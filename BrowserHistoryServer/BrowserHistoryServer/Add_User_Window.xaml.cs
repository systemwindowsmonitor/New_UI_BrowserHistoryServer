using BrowserHistory_Server.Data;
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
    /// Логика взаимодействия для Add_User_Window.xaml
    /// </summary>
    public partial class Add_User_Window : Window
    {
        bool discardChanges;
        string databaseName = (System.IO.Directory.GetCurrentDirectory() + "\\DB.db");

        public Add_User_Window()
        {
            InitializeComponent();
            discardChanges = false;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = true;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DbManager db = new DbManager(databaseName);
            db.AddAdminAsync(TexBoxName.Text.ToString(), TexBoxSurname.Text.ToString(), TexBoxMiddleName.Text.ToString(), TexBoxLogin.Text.ToString(), TexBoxPassword.Text.GetHashCode().ToString(), ComboBoxRole.Text.ToString());
            this.Close();
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = false;
            discardChanges = true;
            this.Close();
        }

    
    }
}
