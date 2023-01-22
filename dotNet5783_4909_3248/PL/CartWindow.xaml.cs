using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public Cart cart = Catalog.cart;
        ObservableCollection<BO.OrderItem> orderForLists = new ObservableCollection<BO.OrderItem>();
        public CartWindow()
        {
            InitializeComponent();   
            orderForLists= Castings.convertListToObservable(cart.Items);
            cart.TotalPriceCart =cart.Items.Sum(x=>x.TotalPrice );
            Tprice.Text = cart.TotalPriceCart.ToString() + "₪";
            dataGridItems.DataContext = orderForLists;
        }

        private void dataGridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid? listBox = sender as DataGrid;
            BO.OrderItem? productItem = new BO.OrderItem();
            productItem = listBox?.SelectedItem as BO.OrderItem;
            new OrderItemdetails(productItem).Show();
            this.Close();
        }

        private void EmptyCartButton_Click(object sender, RoutedEventArgs e)
        {
            int amount = 0;
            for (int i = 0; i < cart.Items.Count; i++)
            {
                bl.Cart.UpdateAmountProuductInCart (cart, cart.Items[i].ProductID, amount);
            }
            cart.Items.Clear();
            cart.TotalPriceCart = 0.0;
            this.Close();
        }

        private void MakePayment_Click(object sender, RoutedEventArgs e)
        {
            if(Tprice.Text == "0₪")
            {
                MessageBox.Show("נא להוסיף פריטים לעגלה");
                return;
            }
            if (Tcustomername.Text == "" || TEmail.Text == ""||Tcustomeradress.Text =="")
            {
                MessageBox.Show("נא להזין פרטי לקוח");
                return;
            }
            else
            {
                cart.CustomerName = Tcustomername.Text;
                cart.CustomerEmail = TEmail.Text;
                cart.CustomerAdress = Tcustomeradress.Text;
                new Payment().Show();
                this.Close();
            }    
        }
    }
}
