using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatSocks.customs
{
    /// <summary>
    /// Interaction logic for configCustom.xaml
    /// </summary>
    public partial class configCustom : UserControl
    {
        public configCustom()
        {
            InitializeComponent();
            presets();
        }

        private void presets()
        {
            //setting ip adress
            string hostname = Dns.GetHostName();
            //getting ipadress list
            IPHostEntry adresses = Dns.GetHostEntry(hostname);
            //adding ip adress
            foreach (IPAddress ip in adresses.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipadress.Text = ip.ToString();
                }
            }
            //guessing a port value 
            Random randomOBJ = new Random();
            int portGuess = randomOBJ.Next(100, 12000);
            port.Text = portGuess.ToString();
        }

        private void cancelBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }

        private void username_GotFocus(object sender, RoutedEventArgs e)
        {
            username.Text = "";
        }

        private void ipadress_GotFocus(object sender, RoutedEventArgs e)
        {
            ipadress.Text = "";
        }

        private void port_GotFocus(object sender, RoutedEventArgs e)
        {
            port.Text = "";
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var window = (MainWindow)Application.Current.MainWindow;
            //setting ip adress
            window.serverIP = IPAddress.Parse(ipadress.Text);
            //setting usernmae
            window.username = username.Text;
            //setting port
            if (port.Text == "")
            {
                Random randomOBJ = new Random();
                int portGuess = randomOBJ.Next(100, 1200);
                MessageBox.Show("We set a port for you " + portGuess.ToString());
                window.port = portGuess;
            }
            else
                window.port = Int32.Parse(port.Text) ;
            //listening 
            window.listen();
            //hiding this
            thisOpacityAnimate(1, 0);
            
        }

        public void thisOpacityAnimate(int start, int finish)
        {
            DoubleAnimation anime = new DoubleAnimation(start, finish, new Duration(TimeSpan.FromSeconds(0.5))); ;
            this.BeginAnimation(OpacityProperty, anime);
        }
    }
}
