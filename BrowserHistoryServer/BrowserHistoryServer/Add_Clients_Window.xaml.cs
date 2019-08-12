using BrowserHistory_Server.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Add_Clients_Window.xaml
    /// </summary>
    public partial class Add_Clients_Window : Window
    {
        int hash;
        bool discardChanges;
        string databaseName = (System.IO.Directory.GetCurrentDirectory() + "\\DB.db");

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

            Regex regexIP = new Regex(@"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$");

            if (regexIP.Match(TexBoxIP.Text.ToString()).Success)
            {
                string myIP = TexBoxIP.Text.ToString();
                DbManager db = new DbManager(databaseName);

                db.AddUserAsync(TexBoxName.Text.ToString(), myIP, (ListRegion.SelectedIndex + 1).ToString());
                this.Close();
            }
            else
            {
                TexBoxIP.Text = string.Empty;
                TexBoxIP.BorderBrush = Brushes.Red;
            }

        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = false;
            discardChanges = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DbManager db = new DbManager(databaseName);

            ListRegion.ItemsSource = db.getRegionsAsync().GetAwaiter().GetResult().Values;
        }

       
    }
}
