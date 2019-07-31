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
                db.Connect();
                db.AddUser(TexBoxName.Text.ToString(), myIP, TexBoxRegion.Text.ToString());
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
    }
}
