using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            comboBoxpayment.Items.Clear();
            comboBoxpayment.ItemsSource = new object[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
            comboBoxpayment_Copy1.Items.Clear();
            comboBoxpayment_Copy1.ItemsSource = new object[] { "01/23", "02/24", "03/25", "04/26", "05/27", "06/28","07/29","08/28","09/29" };
        }
        public static bool IsNumber(string num)
        {
            string pattern = @"\b[1-9-\s]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(num);
        }
        public static bool IsHebrew(string word)
        {
            string pattern = @"\b[א-ת-\s ]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(word);
        }
        public static bool IsEnglish(string word)
        {
            string pattern = @"\b[a-z-\s ]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(word);
        }
        private void MakeOrder_Click(object sender, RoutedEventArgs e)
        {  
            try
            {
                if(Tcredit.Text !=" " && comboBoxpayment.SelectedIndex!=-1 && comboBoxpayment_Copy1.SelectedIndex != -1)
                {
                    if(IsNumber(Tcredit.Text)&& IsNumber(Tcvv.Text))
                    {
                        bl.Cart.CartPayment(cart);
                        MessageBox.Show("ההזמנה בוצעה בהצלחה!!");
                        MessageBox.Show("תודה שקניתם אצלנו!!");
                        int amount = Convert.ToInt16(comboBoxpayment.Text);
                        int AmountItem = (cart.Items.Count());
                        new ReceiptWindow(amount, AmountItem).Show();
                        cart.Items.Clear();
                        cart.TotalPriceCart = 0.0;
                        this.Close();
                    }
                    else
                    {
                        Tcredit.Text = " ";
                        comboBoxpayment.SelectedIndex = -1;
                        comboBoxpayment_Copy1.SelectedIndex = -1;
                        Tcvv.Text = " ";
                        MessageBox.Show (" פרטי תשלום חייבים להיות ערך מספרי!");
                        return;
                    }    
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
