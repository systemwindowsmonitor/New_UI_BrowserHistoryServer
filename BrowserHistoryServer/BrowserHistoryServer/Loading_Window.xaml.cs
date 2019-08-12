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
using System.Windows.Threading;

namespace BrowserHistoryServer
{
    /// <summary>
    /// Логика взаимодействия для Loading_Window.xaml
    /// </summary>
    public partial class Loading_Window : UserControl
    {
        public Loading_Window()
        {
            InitializeComponent();
            Media.Source = new Uri(Environment.CurrentDirectory + @"\loading-1.gif");
            Loadiing();
        }

        DispatcherTimer timer = new DispatcherTimer();

        private void Media_MediaEnded(object sender, RoutedEventArgs e)
        {
            Media.Position = new TimeSpan(0, 0, 1);
            Media.Play();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            //Hide();
            new Admin_Window().Show();
            //Close();
        }

        void Loadiing()
        {
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0, 0, 8);
            timer.Start();
        }
    }
}
