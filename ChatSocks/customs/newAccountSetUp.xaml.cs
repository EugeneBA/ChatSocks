using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatSocks.customs
{
    /// <summary>
    /// Interaction logic for newAccountSetUp.xaml
    /// </summary>
    public partial class newAccountSetUp : UserControl
    {
        public newAccountSetUp()
        {
            InitializeComponent();
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
            try
            {
                //get ip and port and bind to an endPoint
                IPEndPoint clientEP = new IPEndPoint(IPAddress.Parse(ipadress.Text), Int32.Parse(port.Text));
                //creating socket
                Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //connecting to remote server
                clientSocket.Connect(clientEP);
                //send greeting message
                clientSocket.Send(Encoding.UTF8.GetBytes("-getUsername"));
                //waiting response
                byte[] buffer = new byte[1024];
                string msg = null;
                //listen 
                int bufferLength = clientSocket.Receive(buffer);
                msg += Encoding.UTF8.GetString(buffer, 0, bufferLength);
                //make account
                var window = (MainWindow)Application.Current.MainWindow;
                //make new account
                window.addReceiverAccount(msg, clientEP);
                //hide 
                this.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show("could not find person");
            }
            
            
            
        }
    }
}
