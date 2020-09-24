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
    /// Interaction logic for textboxForLoginCustom.xaml
    /// </summary>
    public partial class textboxForLoginCustom : UserControl
    {
        public textboxForLoginCustom()
        {
            InitializeComponent();
        }

        private void textLabel_GotFocus(object sender, RoutedEventArgs e)
        {
            DoubleAnimation shrink = new DoubleAnimation(this.Width, 200, new Duration(TimeSpan.FromSeconds(0.05)));
            this.BeginAnimation(WidthProperty, shrink);
            //clear the text
            textLabel.Text = "";
        }
    }
}
