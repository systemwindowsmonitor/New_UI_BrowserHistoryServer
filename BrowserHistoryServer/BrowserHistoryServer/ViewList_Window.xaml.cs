using BrowserHistory_Server.Data;
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
        DbManager db = new DbManager(System.IO.Directory.GetCurrentDirectory() + "\\DB.db");
        public ViewList_Window()
        {
            InitializeComponent();

            db.Connect();
            MainDataGridViewList.ItemsSource = db.getUsers();

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

        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            MainDataGridViewList.Columns[0].Header = "ID";
            MainDataGridViewList.Columns[1].Header = "Имя";
            MainDataGridViewList.Columns[2].Header = "IP";
            MainDataGridViewList.Columns[3].Header = "Регион";

            Regions_SearchComboViewList.ItemsSource = db.getRegions().Values;
        }

        private void Search_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {

            }
            listView.UnselectAll();
        }

        private void Serch_Button_Click_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ObColl = new ObservableCollection<User>(db.getUsers());
            var _itemSourceList = new CollectionViewSource() { Source = ObColl };
            ICollectionView Itemlist = _itemSourceList.View;
            var yourCostumFilter = new Predicate<object>(ComplexFilterViewList);
            Itemlist.Filter = yourCostumFilter;
            MainDataGridViewList.ItemsSource = Itemlist;
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

            }
            listView.UnselectAll();
        }

        
    }
}
