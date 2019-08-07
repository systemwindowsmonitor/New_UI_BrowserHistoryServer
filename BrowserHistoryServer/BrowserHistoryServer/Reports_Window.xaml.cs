using BrowserHistory_Server.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Reports_Window.xaml
    /// </summary>
    public partial class Reports_Window : System.Windows.Window
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
        public static DataTable DataGridtoDataTable(DataGrid dg)
        {


            dg.SelectAllCells();
            dg.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dg);
            dg.UnselectAllCells();
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] Fields;
            Fields = Lines[0].Split(new char[] { ',' });
            int Cols = Fields.GetLength(0);

            DataTable dt = new DataTable();
            for (int i = 0; i < Cols; i++)
                dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            DataRow Row;
            for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            {
                Fields = Lines[i].Split(new char[] { ',' });
                Row = dt.NewRow();
                for (int f = 0; f < Cols; f++)
                {
                    Row[f] = Fields[f];
                }
                dt.Rows.Add(Row);
            }
            return dt;

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

        private void ListView_Export_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                try
                {
                    DataTable dt = new DataTable();
                    dt = DataGridtoDataTable(MainDataGridExel);
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + DateTime.Now.ToShortDateString() + "_" + DateTime.Now.ToShortTimeString().Replace(':', '_') + ".csv";
                    CSV.ExportToCSV(path, dt);
                }
                catch (Exception rx)
                {
                    MessageBox.Show(rx.Message);
                }
                MessageBox.Show("Файл сохранен на рабочий стол!");
            }
            listView.UnselectAll();
        }

        private void ListView_PrintExel_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                try
                {
                    this.IsEnabled = false;

                    PrintDialog printDialog = new PrintDialog();
                    if (printDialog.ShowDialog() == true)
                    {
                        printDialog.PrintVisual(MainDataGridExel, "Invoice");
                    }
                }
                finally
                {
                    this.IsEnabled = true;
                }
            }
            listView.UnselectAll();
        }
    }
}
