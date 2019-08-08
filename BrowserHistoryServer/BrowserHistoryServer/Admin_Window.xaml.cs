
using BrowserHistory_Server.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        DbManager db = new DbManager(System.IO.Directory.GetCurrentDirectory() + "\\DB.db");
        public object SnackbarUnsavedChenges { get; private set; }

        public Admin_Window()
        {
            InitializeComponent();
            db.Connect();

            
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
            Regions_SearchCombo.ItemsSource = db.getRegions().Values;
            UpdateDataGrid();
        }

        private void UpdateDataGrid()
        {
            MainDataGrid.ItemsSource = db.getUsers();
            TextBoxSerch.Focus();
            if (MainDataGrid.Columns.Count > 0)
                UpdateDataGridNames();
        }
        private void UpdateDataGridNames()
        {
            MainDataGrid.Columns[0].Header = "ID";
            MainDataGrid.Columns[1].Header = "Имя";
            MainDataGrid.Columns[2].Header = "IP";
            MainDataGrid.Columns[3].Header = "Регион";
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
            db.Disconnect();
            GC.Collect();
        }

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo.SelectedValue.ToString());

            //now we add our Filter
            Itemlist.Filter = yourCostumFilter;
            MainDataGrid.ItemsSource = Itemlist;
            UpdateDataGridNames();
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            Predicate<object> yourCostumFilter;
            if (Regions_SearchCombo.SelectedValue != null)
                yourCostumFilter = new Predicate<object>(ComplexFilter);
            else
                yourCostumFilter = new Predicate<object>(item => ((User)item).account_name.Contains(TextBoxSerch.Text));

            Itemlist.Filter = yourCostumFilter;
            MainDataGrid.ItemsSource = Itemlist;

            UpdateDataGridNames();
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
            UpdateDataGrid();
        }
        
    }
}
