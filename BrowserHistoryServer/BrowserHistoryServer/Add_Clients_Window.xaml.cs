using System.Windows;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Add_Clients_Window.xaml
    /// </summary>
    public partial class Add_Clients_Window : Window
    {
        int hash;
        bool discardChanges;

        public Add_Clients_Window()
        {
            InitializeComponent();
            discardChanges = false;
           
        }

        private void Closed_Click(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = false;
            discardChanges = true;
            this.Close();
        }
    }
}
