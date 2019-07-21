using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для New_Admin_Window.xaml
    /// </summary>
    public partial class New_Admin_Window : Window
    {
        public New_Admin_Window()
        {
            InitializeComponent();
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            if(ButtonOpenMenu.Content.ToString().ToLower() == "открыть")
            {
                ButtonOpenMenu.Content = "Закрыть";
            }
            else
            {
                ButtonOpenMenu.Content = "Открыть";
            }
        }

        private void ButtonCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void GridMovement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Work_Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
