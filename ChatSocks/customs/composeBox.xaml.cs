using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for composeBox.xaml
    /// </summary>
    public partial class composeBox : UserControl
    {
        public composeBox()
        {
            InitializeComponent();
            Presets();
        }

        public Boolean visible = false;
        public IPEndPoint clientsEP;
        public string clientSocks;
        public int activeAccountIndex;

        public void Presets()
        {

        }

        private void message_MouseDown(object sender, MouseButtonEventArgs e)
        {
            message.Text = "";
        }

        public void show()
        {
            ThicknessAnimation slideup = new ThicknessAnimation();
            slideup.From = new Thickness(60, 0, 0, -40);
            slideup.To = new Thickness(60, 0, 0, 0);
            slideup.Duration = TimeSpan.FromSeconds(0.6);
            this.BeginAnimation(MarginProperty, slideup);
            this.visible = true;
        }

        public void hide()
        {           
            ThicknessAnimation slideup = new ThicknessAnimation();
            slideup.From = new Thickness(60, 0, 0, 0);
            slideup.To = new Thickness(60, 0, 0, -40);
            slideup.Duration = TimeSpan.FromSeconds(0.6);
            this.BeginAnimation(MarginProperty, slideup);
            this.visible = false;
        }

        private void message_GotFocus(object sender, RoutedEventArgs e)
        {
            message.Text = "";
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //main window 
                var window = (MainWindow)Application.Current.MainWindow;

                //send message
                if (this.clientSocks == "sender")
                {
                    window.serverSocksHandler.Send(Encoding.UTF8.GetBytes(window.username + "$" + message.Text + "$" + " sockchat"));
                }
                else
                {
                    Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //connecting to remote server
                    clientSocket.Connect(clientsEP);
                    //send greeting message
                    clientSocket.Send(Encoding.UTF8.GetBytes(window.username + "$" + message.Text + "$" + " sockchat"));
                  
                }

                //add message to account data
                window.addMessageToActiveAccount(activeAccountIndex, message.Text, "server");
                messageDialougeCustom newMesg = new messageDialougeCustom(message.Text, new Thickness(20, 20, 0, 0));
                newMesg.HorizontalAlignment = HorizontalAlignment.Left;
                window.messageBox.Children.Add(newMesg);
                //clear text
                message.Text = "";
               
            }
        }
    }
}
