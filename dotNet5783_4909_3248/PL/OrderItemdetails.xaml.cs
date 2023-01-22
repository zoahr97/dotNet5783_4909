using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for OrderItemdetails.xaml
    /// </summary>
    public partial class OrderItemdetails : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public Cart cart = Catalog.cart;
        public OrderItemdetails(BO.OrderItem? orderItem )
        {
            InitializeComponent();
            Torderitemid.Text =orderItem?.OrderItemID .ToString ();
            TproductId.Text =orderItem?.ProductID.ToString ();
            Tname.Text = orderItem?.ProductName.ToString();
            Tprice.Text =orderItem?.Price.ToString();
            Tamount.Text =orderItem?.Amount.ToString();
            TtotalPrice.Text =orderItem?.TotalPrice.ToString();           
        }

        private void updatebutton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TproductId.Text);
                int amount = Convert.ToInt32(Tamount.Text);
                bl.Cart.UpdateAmountProuductInCart(cart, id, amount);
                this.Close();
                new CartWindow().Show();
            }
            catch (BO.RequestFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(TproductId.Text);
                int amount = 0;
                bl.Cart.UpdateAmountProuductInCart(cart, id, amount);
                this.Close();
                new CartWindow().Show();
            }
            catch (BO.RequestFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
            }         
        }
    }
}
