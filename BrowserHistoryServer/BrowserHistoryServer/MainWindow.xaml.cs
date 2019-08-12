using BrowserHistory_Server.Data;
using BrowserHistoryServer.Data;
using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class ClientObject
        {
            public TcpClient client;
            public ClientObject(TcpClient tcpClient)
            {
                client = tcpClient;
            }

            public void Process()
            {
                NetworkStream stream = null;
                try
                {
                    stream = client.GetStream();
                    byte[] data = new byte[64]; // буфер для получаемых данных
                    while (true)
                    {
                        // получаем сообщение
                        StringBuilder builder = new StringBuilder();
                        int bytes = 0;
                        do
                        {
                            bytes = stream.Read(data, 0, data.Length);
                            builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                        }
                        while (stream.DataAvailable);

                        string message = builder.ToString();

                        Console.WriteLine(message);
                        // отправляем обратно сообщение в верхнем регистре
                        message = message.Substring(message.IndexOf(':') + 1).Trim().ToUpper();
                        data = Encoding.Unicode.GetBytes(message);
                        stream.Write(data, 0, data.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                    if (client != null)
                        client.Close();
                }
            }
        }

        const int port = 8888;
        static TcpListener listener;

        DbManager db = new DbManager((System.IO.Directory.GetCurrentDirectory() + "\\DB.db"));
        Storyboard animation;
        DoubleAnimation a;

        public MainWindow()
        {
            InitializeComponent();

            Login_TextBox.Focus();
        }

        
        private void GridMovement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ButtonSign_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(db.CheckLogin(Login_TextBox.Text).ToString());
            if (db.CheckLogin(Login_TextBox.Text))
                if (db.CheckPassword(Password_PasswordBox.Password))
                {
                    new Loading().Show();
                    this.Close();
                }
        }

        Thread t;
        TcpClient client;
        bool isActive = true;
        ThreadStart ts;
        //Закрытие окна входа

        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            if (client != null) client.Dispose();

            //t.Abort();
            //this.Close();

            Process progress = Process.GetCurrentProcess();
            progress.Kill();
        }

    }
}
