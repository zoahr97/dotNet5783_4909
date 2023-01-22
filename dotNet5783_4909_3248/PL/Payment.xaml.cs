using BlApi;
using BlImplementation;
using BO;
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
    /// Interaction logic for Payment.xaml
    /// </summary>
    public partial class Payment : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public Cart cart = Catalog.cart;
        public Payment()
        {
            InitializeComponent();
            comboBoxpayment.ItemsSource = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, };
        }

        private void MakeOrder_Click(object sender, RoutedEventArgs e)
        {  
            try
            {
                if(Tcredit.Text !=" " && Tvalidity.Text!=" " && Tcvv.Text!= " " && comboBoxpayment.SelectedIndex!=-1)
                {
                    bl.Cart.CartPayment(cart);
                    MessageBox.Show("ההזמנה בוצעה בהצלחה!!");
                    MessageBox.Show("תודה שקניתם אצלנו!!");
                    int amount = Convert.ToInt16(comboBoxpayment.Text);
                    new ReceiptWindow(amount).Show();
                    cart.Items.Clear();
                    cart.TotalPriceCart = 0.0;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("ההזמנה לא בוצעה!!,נא למלא פרטי חשבון");
                }
               
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

        
    }
}
