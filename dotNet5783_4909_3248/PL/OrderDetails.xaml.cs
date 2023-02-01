using BlApi;
using BlImplementation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public OrderDetails()
        {
            InitializeComponent();
        }
        public OrderDetails(int orderid)
        {
            InitializeComponent();
            orderstatus.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
            updateshipbutton.Visibility = Visibility.Visible ;
            deliverybutton.Visibility = Visibility.Visible ;
            try
            {
               
                BO.Order order = new BO.Order();
                order=bl.Order.GetBoOrder(orderid);
                DataContext = order;
                listViewitems.ItemsSource = order.Items;
                if (order.ShipDate  != null )
                {
                    datePicker1.Visibility = Visibility.Visible ;                 
                    updateshipbutton.Visibility = Visibility.Hidden ;
                }
                else
                {
                    datePicker1.Visibility = Visibility.Hidden ;
                }

                if (order.DeliveryDate != null)
                {
                    datePicker2.Visibility = Visibility.Visible ;                  
                    deliverybutton.Visibility = Visibility.Hidden ;
                }
                else
                {
                    datePicker2.Visibility =Visibility.Hidden ;
                }   
            }
            catch (BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        private void updateshipbutton_Click(object sender, RoutedEventArgs e)
        {
            int id =Convert.ToInt16(Torderid.Text);
            try
            {
                bl.Order.ShipUpdate(id);
                this.Close();
            }
            catch(BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.RequestFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void deliverybutton_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt16(Torderid.Text);
            try
            {
                bl.Order.DeliveredUpdate(id);
                this.Close();
            }
            catch (BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.RequestFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
        }     
    }
}
