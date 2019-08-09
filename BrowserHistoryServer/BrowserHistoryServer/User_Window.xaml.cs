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
        public User_Window()
        {
            InitializeComponent();
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

        }

        private void PackIcon_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void MainDataGrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Regions_SearchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
