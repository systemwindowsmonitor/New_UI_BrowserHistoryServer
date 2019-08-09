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
        public Edit_Window()
        {
            InitializeComponent();
            TextBoxSerch.Focus();
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

            }
            listView.UnselectAll();
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
    }
}
