﻿
using BrowserHistory_Server.Data;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            MainDataGrid.ItemsSource = db.getUsers();
            //MessageBox.Show(MainDataGrid..ToString());

            //MainDataGrid.Columns.Add(new DataGridTextColumn() { Header = "DSA" });
            TextBoxSerch.Focus();
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

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Process progress = Process.GetCurrentProcess();
            progress.Kill();
        }


        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MainDataGrid.Columns[0].Header = "ID";
            MainDataGrid.Columns[1].Header = "Имя";
            MainDataGrid.Columns[2].Header = "IP";
            MainDataGrid.Columns[3].Header = "Регион";

            Regions_SearchCombo.ItemsSource = db.getRegions().Values;
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

                    break;
                default:
                    break;
            }
            listView.UnselectAll();
            MainDataGrid.ItemsSource = db.getUsers();
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
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(ComplexFilter);
            Itemlist.Filter = yourCostumFilter;
            MainDataGrid.ItemsSource = Itemlist;
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
    }
}
