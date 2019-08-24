
using BrowserHistory_Server.Data;
using BrowserHistoryServer.Data;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        DataGridManager dataGrid;
        public object SnackbarUnsavedChenges { get; private set; }
        

        public Admin_Window()
        {
            InitializeComponent();
            dataGrid = DataGridManager.Init();


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

        private void ButtonPowerOFF_Click(object sender, RoutedEventArgs e)
        {
            Process progress = Process.GetCurrentProcess();
            progress.Kill();
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
            for (int i = 0; i < MainDataGrid.Columns.Count; i++)
            {
                MainDataGrid.Columns[i].Header = dataGrid.getHeaders()[i];

            }
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
                case "users":
                    new Add_User_Window().ShowDialog();
                    break;
                case "clients":
                    new Add_Clients_Window().ShowDialog();
                    break;
                case "edit":
                    new Edit_Window().ShowDialog();
                    break;
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

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GC.Collect();
        }

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataGrid.ItemsSource = dataGrid.GetTable(new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo.SelectedValue.ToString()));
            
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

        private void ClearSearch_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                UpdateDataGrid();
                Regions_SearchCombo.SelectedValue = -1;
                TextBoxSerch.Clear();  
            }
            listView.UnselectAll();
        }

       
    }
}
