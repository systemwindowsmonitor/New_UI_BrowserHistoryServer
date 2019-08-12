using BrowserHistory_Server.Data;
using BrowserHistoryServer.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Edit_Window.xaml
    /// </summary>
    public partial class Edit_Window : Window
    {
        DataGridManager dataGrid;
        public Edit_Window()
        {
            InitializeComponent();
            dataGrid = DataGridManager.Init();
            TextBoxSerch.Focus();
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Delet_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {

            }
            listView.UnselectAll();
        }

        private void Copy_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {

            }
            listView.UnselectAll();
        }

        private void Save_Click_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Не сносить т.к кнопка не будет работать. Писать логику в ифе
            var listView = (ListView)sender;
            if (listView.SelectedItems.Count != 0)
            {

            }
            listView.UnselectAll();
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

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainDataGrid.ItemsSource = dataGrid.GetTable(new Predicate<object>(item => ((User)item).Region == Regions_SearchCombo.SelectedValue.ToString()));
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
    }
}
