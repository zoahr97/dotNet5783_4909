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

namespace PL
{
    /// <summary>
    /// Interaction logic for Disconts.xaml
    /// </summary>
    public partial class Disconts : Window
    {
        public static bool flag = false;
        public static bool flag1 = false;
        public static bool flag2 = false;

        public Disconts()
        {
            InitializeComponent();
        }

        private void Addbutton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            flag = false;
            flag1 = false;
            flag2 = false;
        }

        private void discont1_Click(object sender, RoutedEventArgs e)
        {
            flag = true;
        }

        private void discont2_Click(object sender, RoutedEventArgs e)
        {
            flag1 = true ;
        }

        private void discont3_Click(object sender, RoutedEventArgs e)
        {
            flag2 = true;
        }
    }
}
