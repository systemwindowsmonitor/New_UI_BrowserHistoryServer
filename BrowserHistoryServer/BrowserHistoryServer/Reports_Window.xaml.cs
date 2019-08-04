using BrowserHistory_Server.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Логика взаимодействия для Reports_Window.xaml
    /// </summary>
    public partial class Reports_Window : Window
    {
        DbManager db = new DbManager(System.IO.Directory.GetCurrentDirectory() + "\\DB.db");
        public Reports_Window()
        {
            InitializeComponent();

            db.Connect();
            MainDataGridExel.ItemsSource = db.getUsers();
            MainDataGridHtml.ItemsSource = db.getUsers();

            TextBoxSerchExel.Focus();
            TextBoxSerchHtml.Focus();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MainDataGridExel_Loaded(object sender, RoutedEventArgs e)
        {
            MainDataGridExel.Columns[0].Header = "ID";
            MainDataGridExel.Columns[1].Header = "Имя";
            MainDataGridExel.Columns[2].Header = "IP";
            MainDataGridExel.Columns[3].Header = "Регион";

            Regions_SearchCombo.ItemsSource = db.getRegions().Values;
        }

        private void MainDataGridHtml_Loaded(object sender, RoutedEventArgs e)
        {
            Regions_SearchCombo_Html.ItemsSource = db.getRegions().Values;
        }

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo.SelectedValue.ToString());

            //now we add our Filter
            Itemlist.Filter = yourCostumFilter;
            MainDataGridExel.ItemsSource = Itemlist;
        }

        private void Regions_SearchCombo_Html_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo_Html.SelectedValue.ToString());

            //now we add our Filter
            Itemlist.Filter = yourCostumFilter;
            MainDataGridHtml.ItemsSource = Itemlist;
        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(ComplexFilterExel);
            Itemlist.Filter = yourCostumFilter;
            MainDataGridExel.ItemsSource = Itemlist;
        }

        private void PackIcon_MouseLeftButtonDown_Html(object sender, MouseButtonEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(ComplexFilterHtml);
            Itemlist.Filter = yourCostumFilter;
            MainDataGridHtml.ItemsSource = Itemlist;
        }

        private bool ComplexFilterExel(object _object)
        {
            var obj = _object as User;
            if (obj != null)
            {
                if (obj.account_name.Contains(TextBoxSerchExel.Text) && obj.Region.Equals(Regions_SearchCombo.SelectedValue.ToString()))
                    return true;
            }
            return false;
        }

        private bool ComplexFilterHtml(object _object)
        {
            var obj = _object as User;
            if (obj != null)
            {
                if (obj.account_name.Contains(TextBoxSerchHtml.Text) && obj.Region.Equals(Regions_SearchCombo_Html.SelectedValue.ToString()))
                    return true;
            }
            return false;
        }
    }
}
