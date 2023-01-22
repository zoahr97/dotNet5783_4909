using BlApi;
using BlImplementation;
using BO;
using PL;
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
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public static  Cart cart = new Cart
        {
            TotalPriceCart = 0,
            Items = new List<BO.OrderItem>()
        };



        public Catalog()
        {
            InitializeComponent();
            try
            {
                if (Disconts.flag == true)
                {
                    ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart,null,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, null, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, null, 0.7);
                        }
                        else
                        {
                            ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart);
                        }
                    }
                   

                }


            }
            catch (BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
            AttributeSelector1.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
        }
        

        private void ProductsCatalogListView_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            ListBox? listBox = sender as ListBox;
            BO.ProductItem? productItem = new BO.ProductItem();
            productItem = listBox?.SelectedItem as BO.ProductItem;
            new ProductDetails(productItem.ProductID, cart).ShowDialog();
            IEnumerable<ProductItem> productItem1 = new List<ProductItem>();
            try
            {
                if (Disconts.flag == true)
                {
                    productItem1 = bl.Product.GetcatalogForList(cart, null, 0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        productItem1 = bl.Product.GetcatalogForList(cart, null, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            productItem1 = bl.Product.GetcatalogForList(cart, null, 0.7);
                        }
                        else
                        {
                            productItem1 = bl.Product.GetcatalogForList(cart);
                        }
                    }
                    
                }
               
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            ProductsCatalogListView.ItemsSource = productItem1;
            ////Close();
            //this.Close();
            
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            new CartWindow().Show();
            this.Close();
        }

        private void AttributeSelector1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AttributeSelector1.SelectedIndex == 7)
            {
                if (Disconts.flag == true)
                {
                    ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart,null,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, null, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, null, 0.7);
                        }
                        else
                        {
                            ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart);
                        }
                    }
                    
                }
               
            }
            else
            {
                Func<BO.ProductItem?, bool>? mydelegate = SelectorCategory;//ע"י ביטוי למבדה
                if (Disconts.flag == true)
                {
                    ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, mydelegate, 0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, mydelegate, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, mydelegate, 0.7);
                        }
                        else
                        {
                            ProductsCatalogListView.ItemsSource = bl.Product.GetcatalogForList(cart, mydelegate);
                        }
                    }
                   
                }
                
                   
            }
        }
        private bool SelectorCategory(BO.ProductItem? p)
        {
            BO.Enums.CATEGORY c = (BO.Enums.CATEGORY)AttributeSelector1.SelectedItem;
            if (p?.category == c)
                return true;
            else
                return false;
        }
    }
}
