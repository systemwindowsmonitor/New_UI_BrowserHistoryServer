using System;
using System.Windows;
using System.Windows.Input;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void GridMovement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
