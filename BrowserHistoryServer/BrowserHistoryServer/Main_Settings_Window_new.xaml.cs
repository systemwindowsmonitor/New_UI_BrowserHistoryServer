using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для Main_Settings_Window.xaml
    /// </summary>
    public partial class Main_Settings_Window : UserControl
    {
        public Main_Settings_Window()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress(From_email.Text, "Tom");
            
            // кому отправляем
            MailAddress to = new MailAddress(To_email.Text);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to); 
            // тема письма            
            m.Subject = "Тест";
            // текст письма
            m.Body = "<h2>Письмо-тест работы smtp-клиента</h2>";
            // письмо представляет код html
            m.IsBodyHtml = true;
            // адрес smtp-сервера и порт, с которого будем отправлять письмо
            SmtpClient smtp = new SmtpClient("mail2.dietcenter.com.ua", 587);
            m.Attachments.Add(new Attachment(@"C:\Users\Vlalin\Desktop\15.09.2019_16_32.csv"));
            smtp.EnableSsl = false;
            // логин и пароль
            smtp.Credentials = new NetworkCredential(From_email.Text, From_email_password.Password);
          
            smtp.Send(m);

        }
    }
}
