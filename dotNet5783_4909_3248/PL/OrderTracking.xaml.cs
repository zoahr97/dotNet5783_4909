using BlApi;
using BlImplementation;
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
using static BO.Enums;

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public OrderTracking()
        {
            InitializeComponent();
            int?[] arr = orderid();
            CategoryBoxid.ItemsSource = arr.OrderBy(x => x); 
            CategoryBox1.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
        }
        public int?[] orderid()
        {
            int i = 0;
            int?[] arr=new int?[bl.Order.GetAllOrderForList().Count()];
            foreach (BO.OrderForList? order in bl.Order.GetAllOrderForList ())
            {
                arr[i] = order?.OrderID;
                i++;
            }
            return arr;
        }

        private void CategoryBoxid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id=Convert.ToInt16 (CategoryBoxid.SelectedValue.ToString());
            BO .OrderTracking orderTracking = new BO.OrderTracking ();
            orderTracking = bl.Order.OrderTracking(id);
            CategoryBox1.SelectedItem  = (BO.Enums.OrderStatus?)orderTracking.OrderStatus;
            ordertrackingListView.ItemsSource = orderTracking.tracking;
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if(CategoryBoxid.SelectedIndex != -1)
            {
                int id = Convert.ToInt16(CategoryBoxid.SelectedValue.ToString());
                OrderDetails orderDetails = new OrderDetails(id);
                orderDetails.updateshipbutton.Visibility = Visibility.Hidden;
                orderDetails.deliverybutton.Visibility = Visibility.Hidden;
                orderDetails.Show();
            }
            else
            {
                MessageBox.Show("יש לבחור מספר מזהה של הזמנה!!");
            }
            
        }
    }
}
