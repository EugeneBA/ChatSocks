using ChatSocks.classes;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatSocks.customs
{
    /// <summary>
    /// Interaction logic for userAccountCustom.xaml
    /// </summary>
    public partial class userAccountCustom : UserControl
    {
        public userAccountCustom(string name, string introMessage)
        {
            InitializeComponent();
            //setting properties
            labelIcon.Content = name[0];
            AccountName.Content = name;
            lastMessage.Content = introMessage;
            //introduce self
            introduction();
        }

        public void introduction()
        {
            ThicknessAnimation slideUP = new ThicknessAnimation();
            slideUP.From = new Thickness(0, 630, 0, 0);
            slideUP.To = new Thickness(0, 5, 0, 0);
            slideUP.Duration = TimeSpan.FromSeconds(0.5);
            this.BeginAnimation(MarginProperty, slideUP);
        }

        public string accountSock = "";
    }
}
