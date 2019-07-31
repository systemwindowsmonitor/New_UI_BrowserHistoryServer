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
        int hash;
        bool discardChanges;

        public Add_User_Window()
        {
            InitializeComponent();
            discardChanges = false;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            SnackbarUnsavedChanges.IsActive = true;
        }

        string databaseName = (System.IO.Directory.GetCurrentDirectory() + "\\DB.db");
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            DbManager db = new DbManager(databaseName);
            db.Connect();
            db.AddAdmin(TexBoxName, TexBoxSurname, TexBoxMiddleName, TexBoxLogin, TexBoxPassword, ComboBoxRole);
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
