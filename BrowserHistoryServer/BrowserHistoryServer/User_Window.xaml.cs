using BrowserHistory_Server.Data;
using BrowserHistoryServer.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для User_Window.xaml
    /// </summary>
    public partial class User_Window : Window
    {
        DataGridManager dataGrid;
        public User_Window()
        {
            InitializeComponent();
            dataGrid = DataGridManager.Init();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            string selectedName = string.Empty;
            if (listView.SelectedItems.Count != 0)
            {
                selectedName = ((ListViewItem)listView.SelectedItem).Name;
            }

            switch (selectedName)
            {
                case "report":
                    new Reports_Window().ShowDialog();
                    break;
                case "viewlist":
                    new ViewList_Window().ShowDialog();
                    break;
                default:
                    break;
            }
            listView.UnselectAll();
            UpdateDataGrid();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ButtonPowerOFF_Click(object sender, RoutedEventArgs e)
        {
            Process progress = Process.GetCurrentProcess();
            progress.Kill();
        }

        private void ClearSearch_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                UpdateDataGrid();
                TextBoxSerch.Clear();
            }
            listView.UnselectAll();
        }

        private void Search_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                Predicate<object> yourCostumFilter;
                if (Regions_SearchCombo.SelectedValue != null)
                    yourCostumFilter = new Predicate<object>(ComplexFilter);
                else
                    yourCostumFilter = new Predicate<object>(item => ((User)item).account_name.Contains(TextBoxSerch.Text));
                MainDataGrid.ItemsSource = dataGrid.GetTable(yourCostumFilter);
            }
            listView.UnselectAll();
        }
        private bool ComplexFilter(object _object)
        {
            var obj = _object as User;
            if (obj != null)
            {
                if (obj.account_name.Contains(TextBoxSerch.Text) && obj.Region.Equals(Regions_SearchCombo.SelectedValue.ToString()))
                    return true;
            }
            return false;
        }

        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var a = dataGrid.GetRegions();
            foreach (var item in a)
            {
                Regions_SearchCombo.Items.Add(item);
            }
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            MainDataGrid.ItemsSource = dataGrid.GetTable();
            TextBoxSerch.Focus();
        }

        private void UpdateDataGridNames()
        {

            for (int i = 0; i < MainDataGrid.Columns.Count; i++)
            {
                MainDataGrid.Columns[i].Header = dataGrid.GridColumnsName[i];
            }
        }

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataGrid.ItemsSource = dataGrid.GetTable(new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo.SelectedValue.ToString()));
        }

        
    }
}
