using BrowserHistory_Server.Data;
using BrowserHistoryServer.Data;
using System;
using System.Data;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Reports_Window.xaml
    /// </summary>
    public partial class Reports_Window : System.Windows.Window
    {
        DataGridManager dataGrid;
        public Reports_Window()
        {
            InitializeComponent();

            dataGrid = DataGridManager.Init();

            TextBoxSerchExel.Focus();
            TextBoxSerchHtml.Focus();
        }

        private void UpdateDataGrid()
        {
            MainDataGridExel.ItemsSource = dataGrid.GetTable();
            TextBoxSerchExel.Focus();
        }

        private void UpdateDataGridHtml()
        {
            MainDataGridHtml.ItemsSource = dataGrid.GetTable();
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
            var a = dataGrid.GetRegions();
            foreach (var item in a)
            {
                Regions_SearchCombo.Items.Add(item);
            }
            UpdateDataGrid();
        }

        private void MainDataGridHtml_Loaded(object sender, RoutedEventArgs e)
        {
            var a = dataGrid.GetRegions();
            foreach (var item in a)
            {
                Regions_SearchCombo_Html.Items.Add(item);
            }
            UpdateDataGridHtml();
        }
        public static bool CheckForInternetConnection()
        {
            var ping = new Ping();
            String host = "google.com";
            byte[] buffer = new byte[32];
            int timeout = 1000;
            var options = new PingOptions();
            try
            {
                var reply = ping.Send(host, timeout, buffer, options);
                return reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
        }
        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataGridExel.ItemsSource = dataGrid.GetTable(new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo.SelectedValue.ToString()));
        }

        private void Regions_SearchCombo_Html_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataGridHtml.ItemsSource = dataGrid.GetTable(new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo_Html.SelectedValue.ToString()));
        }

        private void Search_Click_Exel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                Predicate<object> yourCostumFilter;
                if (Regions_SearchCombo.SelectedValue != null)
                    yourCostumFilter = new Predicate<object>(ComplexFilterExel);
                else
                    yourCostumFilter = new Predicate<object>(item => ((User)item).account_name.Contains(TextBoxSerchExel.Text));
                MainDataGridExel.ItemsSource = dataGrid.GetTable(yourCostumFilter);
            }
            listView.UnselectAll();
        }

        private void Search_Click_Html_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                Predicate<object> yourCostumFilter;
                if (Regions_SearchCombo_Html.SelectedValue != null)
                    yourCostumFilter = new Predicate<object>(ComplexFilterHtml);
                else
                    yourCostumFilter = new Predicate<object>(item => ((User)item).account_name.Contains(TextBoxSerchHtml.Text));
                MainDataGridHtml.ItemsSource = dataGrid.GetTable(yourCostumFilter);
            }
            listView.UnselectAll();
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

        private void Export_Html_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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

        private void Print_Html_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
                        printDialog.PrintVisual(MainDataGridHtml, "Invoice");
                    }
                }
                finally
                {
                    this.IsEnabled = true;
                }
            }
            listView.UnselectAll();
        }

        private void ListView_ClearExel_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                UpdateDataGrid();
                TextBoxSerchExel.Clear();
            }
            listView.UnselectAll();
        }

        private void Clear_Html_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                UpdateDataGridHtml();
                TextBoxSerchHtml.Clear();
            }
            listView.UnselectAll();
        }

        private void ListView_SendMail_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {
                if (!CheckForInternetConnection())
                {
                    System.Windows.Forms.MessageBox.Show("Нет интернета!");
                    return;
                }

                DataTable dt = new DataTable();
                dt = DataGridtoDataTable(MainDataGridExel);
                if (dt != null)
                {
                    string path = "mailData.csv";
                    //if (File.Exists(path))
                    //    File.Delete(path);

                    using (null)
                    {
                        CSV.ExportToCSV(path, dt);

                        var a = new DataMailSender("browserhistory@dietcenter.com.ua", "falcon.ukr1@gmail.com", path);
                        a.Send();
                        a.Dispose();
                    }

                }
            }
            listView.UnselectAll();
        }
    }
}
