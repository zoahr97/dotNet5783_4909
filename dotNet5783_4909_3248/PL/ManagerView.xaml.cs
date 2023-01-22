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
    /// Interaction logic for ManagerView.xaml
    /// </summary>
    public partial class ManagerView : Window
    {
        public ManagerView()
        {
            InitializeComponent();
        }

        private void ListOrderbutton_Click(object sender, RoutedEventArgs e)
        {
            new OrderList().Show();
        }

        private void ListProductbutton_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().Show();
        }

        private void ListOrderbutton_Copy_Click(object sender, RoutedEventArgs e)
        {
            new OrderTrackingforManeger ().Show();
        }

        private void ListOrderbutton_Copy_Click_1(object sender, RoutedEventArgs e)
        {
            new Disconts().Show(); 
        }
    }
}
