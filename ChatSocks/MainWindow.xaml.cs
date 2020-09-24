using ChatSocks.classes;
using ChatSocks.customs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace ChatSocks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public IPAddress serverIP;
        public string username;
        public int port;
        public Socket serverSocks;
        public Socket serverSocksHandler;
        private accountData[] activeAccounts = new accountData[10] ;
        public Thread threadUI;
        public string buttonFocused;
        public string MyUsername;

        public MainWindow()
        {
            InitializeComponent();
            presets();
        }

        public void presets()
        {
            settingsControl.Visibility = Visibility.Hidden;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            settingsControl.Visibility = Visibility.Visible;
            if (messageComposer.visible == true)
                messageComposer.hide();
            messageBox.Children.Clear();
        }

        public void listen()
        {
            IPEndPoint serverEP = new IPEndPoint(serverIP, port);
            serverSocks = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocks.Bind(serverEP);

            //thread
            threadUI = new Thread(listenSilence);
            threadUI.Start();
        }

        private void listenSilence()
        {
            //listening 
            serverSocks.Listen(10);
            serverSocksHandler = serverSocks.Accept();
            //receiving 
            byte[] buffer = null;
            string msg = null;
            while (true)
            {
                buffer = new byte[1024];
                int bufferLength = serverSocksHandler.Receive(buffer);
                //get client end point
                IPEndPoint clientEndPoint = serverSocksHandler.RemoteEndPoint as IPEndPoint;
                
                
                msg += Encoding.UTF8.GetString(buffer, 0, bufferLength);
                //account creation
                if (msg.IndexOf("sockchat") > -1)
                {
                    //analyz text
                    string account = "";
                    string message = "";
                    int positionCount = 0;
                    //disamble
                    for (int i = 0 ; i != msg.Length; i++)
                    {
                        positionCount = i;
                        if (msg[i] != char.Parse("$"))
                        {
                            account += msg[i];
                        }
                        else break;
                    }
                    for (int i = (positionCount + 1) ; i != msg.Length; i++)
                    {
                        positionCount = i;
                        if (msg[i] != char.Parse("$"))
                        {
                            message += msg[i];
                        }
                        else break;
                    }

                    this.Dispatcher.Invoke(new Action(() => messageReceived(account, message,clientEndPoint)),DispatcherPriority.Normal);
                }

                if (msg.IndexOf("-getUsername") > -1)
                {
                    serverSocksHandler.Send(Encoding.UTF8.GetBytes(this.username));
                }
                msg = "";
            }
        }

        public void messageReceived(string account, string message, IPEndPoint remoteServer)
        {
     
            Boolean accountExists = false;
            int accountDataIndex = 0;

           //check if the account exists
           for (int i=0; i != activeAccounts.Length; i++)
           {
                //check if it is blank (because if blank nothing else is infront)
                if (activeAccounts[i] == null)
                {
                    break;
                }
                else
                {
                    if (activeAccounts[i].username == account)
                    {
                        accountExists = true;
                        accountDataIndex = i;
                        break;
                    }
                }
           }

           //if account doent exist
           if (accountExists == false)
            {
                //add to account stack
                for (int i = 0; i != activeAccounts.Length; i++)
                {
                    if (activeAccounts[i] == null)
                    {
                        //make new active account 
                        activeAccounts[i] = new accountData(account, message);
                        //set data pointer
                        accountDataIndex = i;
                        //make new userAccount
                        userAccountCustom newCustom = new userAccountCustom(account, message);
                        newCustom.Width = 290;



                        //onclick event of this control

                        //set focused
                        buttonFocused = account;
                        
                        //event
                        newCustom.MouseDown += (sender, EventArgs) =>
                        {
                            //set index for composer
                            messageComposer.activeAccountIndex = i;
                            //retract
                            messageComposer.show();
                            //add endpoint
                            messageComposer.clientsEP = remoteServer;
                            messageComposer.clientSocks = "sender";
                            //adding all messages 
                            messageBox.Children.Clear();
                           for (int b = 0; b != activeAccounts[accountDataIndex].messages.Length; b++)
                            {
                                if (activeAccounts[accountDataIndex].messageCount + 1 == b)
                                {
                                    break;
                                }
                                else
                                {
                                    messageDialougeCustom newDialouge = new messageDialougeCustom(activeAccounts[accountDataIndex].messages[b].message, new Thickness(0,20,100,0));
                
                                    newDialouge.HorizontalAlignment = HorizontalAlignment.Left;
                                    if (activeAccounts[accountDataIndex].messages[b].from == "client")
                                    {
                                        newDialouge.HorizontalAlignment = HorizontalAlignment.Right;
                                    }

                                    SolidColorBrush messagecolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF321779"));
                                    newDialouge.messageColor.Background = messagecolor;
                                    messageBox.Children.Add(newDialouge);
                                }

                             
                            }
                        };
                        //set account socks 
                        newCustom.accountSock = "sender";
                        //adding to stack
                        AccountStack.Children.Add(newCustom);
                        //break
                        break;
                    }
                }
            }

           //if account exists 
           if (accountExists == true)
            {
                //adding message to accountData
                activeAccounts[accountDataIndex].addMessage(message,"client");
                
                //check if focused 
                if (buttonFocused == activeAccounts[accountDataIndex].username)
                {
                    //adding message 
                    messageDialougeCustom newDialouge = new messageDialougeCustom(message, new Thickness(0, 20, 100, 0));
                    newDialouge.HorizontalAlignment = HorizontalAlignment.Left;
                    if (activeAccounts[accountDataIndex].messages[accountDataIndex].from == "client")
                    {
                        newDialouge.HorizontalAlignment = HorizontalAlignment.Right;
                    }
                    SolidColorBrush messagecolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF321779"));
                    newDialouge.messageColor.Background = messagecolor;
                    messageBox.Children.Add(newDialouge);
                }    

            }

        }

        private void addAccountCustom_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //clear messages
            messageBox.Children.Clear();
            //add newAccountSetUp
            newAccountSetUp newAcc = new newAccountSetUp();
            newAcc.Width = 250;
            newAcc.Height = 100;
            newAcc.HorizontalAlignment = HorizontalAlignment.Left;
            newAcc.Margin = new Thickness(20, 20, 0, 0);
            messageBox.Children.Add(newAcc);
        }

        public int addActiveAccount(string name, string message)
        {
            int i;
            for ( i = 0; i != activeAccounts.Length; i++)
            {
                if (activeAccounts[i] == null)
                {
                    activeAccounts[i] = new accountData(name, message);
                    break;
                }
            }
            return i;
        }

        public void addReceiverAccount(string name, IPEndPoint epClient)
        {
            //adding active account
            int accountDataIndex = addActiveAccount(name, "new account");
            //make new userAccount
            userAccountCustom newCustom = new userAccountCustom(name, "new account");
            newCustom.Width = 290;
            newCustom.accountSock = "receiver";
            //onclick event of this control

            //set focused
            buttonFocused = name;

            //event
            newCustom.MouseDown += (sender, EventArgs) =>
            {
                //retract
                messageComposer.show();
                messageComposer.clientSocks = "receiver";
                //add endpoint
                messageComposer.clientsEP = epClient;
                //adding all messages 
                messageBox.Children.Clear();
                for (int b = 0; b != activeAccounts[accountDataIndex].messages.Length; b++)
                {
                    if (activeAccounts[accountDataIndex].messageCount + 1 == b)
                    {
                        break;
                    }
                    else
                    {
                        messageDialougeCustom newDialouge = new messageDialougeCustom(activeAccounts[accountDataIndex].messages[b].message, new Thickness(0, 20, 100, 0));
                        newDialouge.HorizontalAlignment = HorizontalAlignment.Left;
                        if (activeAccounts[accountDataIndex].messages[b].from == "client")
                        {
                            newDialouge.HorizontalAlignment = HorizontalAlignment.Right;
                        }
                        SolidColorBrush messagecolor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF321779"));
                        newDialouge.messageColor.Background = messagecolor;
                        messageBox.Children.Add(newDialouge);
                    }


                }
            };

            //add item 
            AccountStack.Children.Add(newCustom);
        }

        public void addMessageToActiveAccount(int accountIndex, string message, string from)
        {
            activeAccounts[accountIndex].addMessage(message, from);
        }
    }

}
