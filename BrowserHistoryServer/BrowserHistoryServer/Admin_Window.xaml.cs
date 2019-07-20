using BrowserHistoryServer.ViewModel;
using MaterialDesignThemes.Wpf;
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
    /// Логика взаимодействия для Admin_Window.xaml
    /// </summary>
    public partial class Admin_Window : Window
    {
        public Admin_Window()
        {
            InitializeComponent();

            var menuRegister = new List<SubItem>();
            menuRegister.Add(new SubItem("Добавить юзера"));
            menuRegister.Add(new SubItem("Добавить пользователя"));
            var item6 = new ItemMenu("Регистрация", menuRegister, PackIconKind.Register);

            var menuSchedule = new List<SubItem>();
            menuSchedule.Add(new SubItem("Удаление"));
            menuSchedule.Add(new SubItem("Пользователи"));
            var item1 = new ItemMenu("Редактирование", menuSchedule, PackIconKind.Edit);

            var menuReports = new List<SubItem>();
            menuReports.Add(new SubItem("Html"));
            menuReports.Add(new SubItem("Excel"));
            menuReports.Add(new SubItem("Word"));
            menuReports.Add(new SubItem("PDF"));
            var item2 = new ItemMenu("Отчеты", menuReports, PackIconKind.FileReport);

            var menuExpenses = new List<SubItem>();
            menuExpenses.Add(new SubItem("Юзеры"));
            menuExpenses.Add(new SubItem("Пользователи"));
            var item3 = new ItemMenu("Списки", menuExpenses, PackIconKind.ViewList);

            var item4 = new ItemMenu("Настройки", new UserControl(), PackIconKind.Settings);

            Menu.Children.Add(new UserControlMenuItem(item6));
            Menu.Children.Add(new UserControlMenuItem(item1));
            Menu.Children.Add(new UserControlMenuItem(item2));
            Menu.Children.Add(new UserControlMenuItem(item3));
            Menu.Children.Add(new UserControlMenuItem(item4));


        }

        
    }
}
