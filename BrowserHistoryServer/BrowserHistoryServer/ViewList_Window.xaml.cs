using BrowserHistory_Server.Data;
using BrowserHistoryServer.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// Логика взаимодействия для ViewList_Window.xaml
    /// </summary>
    public partial class ViewList_Window : Window
    {
        DataGridManager dataGrid;
        public ViewList_Window()
        {
            InitializeComponent();

            dataGrid = DataGridManager.Init();

            TextBoxSerchViewList.Focus();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var a = dataGrid.GetRegions();
            foreach (var item in a)
            {
                Regions_SearchComboViewList.Items.Add(item);
            }
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            MainDataGridViewList.ItemsSource = dataGrid.GetTable();
            TextBoxSerchViewList.Focus();
        }

        private void Search_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                Predicate<object> yourCostumFilter;
                if (Regions_SearchComboViewList.SelectedValue != null)
                    yourCostumFilter = new Predicate<object>(ComplexFilter);
                else
                    yourCostumFilter = new Predicate<object>(item => ((User)item).account_name.Contains(TextBoxSerchViewList.Text));
                MainDataGridViewList.ItemsSource = dataGrid.GetTable(yourCostumFilter);
            }
            listView.UnselectAll();
        }

        private bool ComplexFilter(object _object)
        {
            var obj = _object as User;
            if (obj != null)
            {
                if (obj.account_name.Contains(TextBoxSerchViewList.Text) && obj.Region.Equals(Regions_SearchComboViewList.SelectedValue.ToString()))
                    return true;
            }
            return false;
        }

        private bool ComplexFilterViewList(object _object)
        {
            var obj = _object as User;
            if (obj != null)
            {
                if (obj.account_name.Contains(TextBoxSerchViewList.Text) && obj.Region.Equals(Regions_SearchComboViewList.SelectedValue.ToString()))
                    return true;
            }
            return false;
        }

        private void ClearSearch_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                UpdateDataGrid();
                TextBoxSerchViewList.Clear();
            }
            listView.UnselectAll();
        }

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataGridViewList.ItemsSource = dataGrid.GetTable(new Predicate<object>(item => ((User)item).Region == Regions_SearchComboViewList.SelectedValue.ToString()));
        }
    }
}
