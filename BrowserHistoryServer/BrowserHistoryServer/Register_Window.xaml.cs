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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Register_Window.xaml
    /// </summary>
    public partial class Register_Window : UserControl
    {
        string databaseName = (System.IO.Directory.GetCurrentDirectory() + "\\DB.db");
        public Register_Window()
        {
            InitializeComponent();
        }

        private void Buttom_Save_Click(object sender, RoutedEventArgs e)
        {
            DbManager db = new DbManager(databaseName);
            db.AddAdminAsync(TexBoxName.Text.ToString(), TexBoxSurname.Text.ToString(), TexBoxMiddleName.Text.ToString(), TexBoxLogin.Text.ToString(), TexBoxPassword.GetHashCode().ToString(), ComboBoxRole.Text.ToString());
            TexBoxName.Clear();
            TexBoxSurname.Clear();
            TexBoxMiddleName.Clear();
            TexBoxLogin.Clear();
            TexBoxPassword.Clear();
            ComboBoxRole.SelectedValue = -1;
        }

        
    }
}
