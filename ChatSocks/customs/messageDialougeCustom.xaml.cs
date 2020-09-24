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
    /// Interaction logic for messageDialougeCustom.xaml
    /// </summary>
    public partial class messageDialougeCustom : UserControl
    {
        public messageDialougeCustom(string message, Thickness side)
        {
            InitializeComponent();
            MessageLabel.Content = message;
            if (message != null)
            {
                //filter * ; ' . 
                int filterSubtract = 0;
                for (int i=0; i != message.Length; i++)
                {
                    if (message[i] == char.Parse(",") || message[i] == char.Parse(".") || message[i] == char.Parse("'") || message[i] == char.Parse("/") || message[i] == char.Parse(" ")) {
                        filterSubtract++;
                    }
                }
    

                this.Width = (message.Length - filterSubtract) * 11;
            }
            //slide up
            introduction(side);
        }


        public void introduction(Thickness final)
        {
            ThicknessAnimation slideUp = new ThicknessAnimation(); ;
            slideUp.From = new Thickness(0, 630, 0, 0);
            slideUp.To =  final;
            slideUp.Duration = TimeSpan.FromSeconds(0.5);
            slideUp.EasingFunction = new QuarticEase();
            this.BeginAnimation(MarginProperty, slideUp);
        }

    }
}
