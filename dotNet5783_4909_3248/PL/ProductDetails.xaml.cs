using BlApi;
using BlImplementation;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ProductDetails.xaml
    /// </summary>
    public partial class ProductDetails : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public Cart cart = Catalog.cart;
        public ProductDetails()
        {
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
            InitializeComponent();
        }
        public ProductDetails (int id ,Cart cart)
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
            try
            {
                BO.ProductItem product = new BO.ProductItem() { category = BO.Enums.CATEGORY.None };
                if (Disconts.flag == true)
                {
                    product = bl.Product.CatalogDetailsProduct(id, cart,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        product = bl.Product.CatalogDetailsProduct(id, cart, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            product = bl.Product.CatalogDetailsProduct(id, cart, 0.7);
                        }
                        else
                        {
                            product = bl.Product.CatalogDetailsProduct(id, cart);
                        }
                    }
                   
                }
               
                DataContext = product;
                if(product.IsStock==true)
                {
                    checkBox.IsChecked= true;
                }
                else
                {
                    checkBox.IsChecked= false;
                }
                //Tid.Text =product.ProductID .ToString ();
                //Tname.Text = product.ProductName;
                //Tprice.Text = product.Price.ToString() + "₪";
                //TIsStock.Text = product.IsStock .ToString();
                //TamountIncart.Text =product.AmountInCartOfCostumer.ToString();
                //CategoryBox.SelectedItem = (BO.Enums.CATEGORY?)product.category;     
            }
            catch (BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }     
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(Tid.Text);
                //int amount=Convert.ToInt16(Tamount.Text);
                if (Disconts.flag == true)
                {
                    bl.Cart.AddProductToCart(id, cart,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        bl.Cart.AddProductToCart(id, cart, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            bl.Cart.AddProductToCart(id, cart, 0.7);
                        }
                        else
                        {
                            bl.Cart.AddProductToCart(id, cart);
                        }
                    }
                    
                }       
                int amount=bl.Cart.amount (id,cart);
                TamountIncart.Text =amount.ToString();        
            }
            catch( BO.RequestFailed ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.DoesntExistException ex)
            {
                MessageBox.Show (ex.Message);
            }
          
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int amount = 0;
                int id = Convert.ToInt16(Tid.Text);
                bl.Cart.UpdateAmountProuductInCart(cart, id, amount);
                this.Close();
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

        private void AddButton_Copy_Click(object sender, RoutedEventArgs e)
        {
            int id = Convert.ToInt16(Tid.Text);
            int amount = bl.Cart.amount(id, cart);
            try
            {
                bl.Cart.UpdateAmountProuductInCart(cart, id, amount - 1);
                int amount1 = bl.Cart.amount(id, cart);
                TamountIncart.Text = amount1.ToString();
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

        private void AddButton_Copy1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
