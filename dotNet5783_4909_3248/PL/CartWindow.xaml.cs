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
            dataGridItems.DataContext = orderForLists.OrderBy(x => x?.TotalPrice );
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
        public static bool CheackMail(string t) //בדיקה שהטקסט בפורמט של כתובת מייל  
        {
            //דוא"ל
            if (t.Length == 0)
            {
                return true;
            }     
            else
            if ((t.IndexOf("@gmail") == -1) || (t.IndexOf(".com") == -1) || t.IndexOf("@") > t.IndexOf("."))
            { 
                    return false;
            }   
            else //אם הכתובת נכונה
                return true;
        }
        public static bool CheackMail1(string t) //בדיקה שהטקסט בפורמט של כתובת מייל  
        {
            //דוא"ל
            if (t.Length == 0)
            {
                return true;
            }
            else
            if ((t.IndexOf("@walla") == -1) || (t.IndexOf(".com") == -1) || t.IndexOf("@") > t.IndexOf("."))
            {
                return false;
            }
            else //אם הכתובת נכונה
                return true;
        }
        public static bool IsNumber(string num)
        {
            string pattern = @"\b[1-9-\s]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(num);
        }
        public static bool IsHebrew(string word)
        {
            string pattern = @"\b[א-ת-0-9\s ]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(word);
        }
        public static bool IsEnglish(string word)
        {
            string pattern = @"\b[a-z-0-9\s ]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(word);
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
                if(IsHebrew(Tcustomername.Text)|| IsEnglish(Tcustomername.Text))
                {
                    cart.CustomerName = Tcustomername.Text;
                }
                else
                {
                    Tcustomername.Text = "";
                    MessageBox.Show("שם הלקוח חייב להיות בעברית או באנגלית!!");
                    return ;
                }
                
                if(CheackMail(TEmail.Text.ToString())==true|| CheackMail1(TEmail.Text.ToString())==true)
                {
                    cart.CustomerEmail = TEmail.Text;
                }
                else
                {
                    TEmail.Text = "";
                    MessageBox.Show("כתובת מייל אינה תקינה!!");
                    return;
                }
                cart.CustomerAdress = Tcustomeradress.Text;
                new Payment().Show();
                this.Close();
            }    
        }
    }
}
