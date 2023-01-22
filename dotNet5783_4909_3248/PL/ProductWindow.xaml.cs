using BlApi;
using BlImplementation;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = BlApi.Factory.Get();
        private BO.Product p = new BO.Product();
        public ProductWindow()
        {
            InitializeComponent();
            CategoryBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
            CategoryBox.SelectedIndex = 7;
            UpDateButton.Visibility = Visibility.Hidden;
        }
        public ProductWindow(int id )
        {
            InitializeComponent();
            CategoryBox.ItemsSource= Enum.GetValues(typeof(BO.Enums.CATEGORY));
            AddButton.Visibility=Visibility.Hidden;
            UpDateButton.Visibility = Visibility.Visible;
            Tid.IsEnabled=false;
            Tname.IsEnabled=false;
            try
            {
                BO.Product product = new BO.Product() { category = BO.Enums.CATEGORY.None};
                if (Disconts.flag == true)
                {
                    product = bl.Product.ManagerDetailsProduct(id, 0.3);
                    CategoryBox.IsEnabled = false;
                    Tinstock.IsEnabled = false;
                    Tprice.IsEnabled = false;
                    UpDateButton.IsEnabled = false;
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        product = bl.Product.ManagerDetailsProduct(id, 0.5);
                        CategoryBox.IsEnabled = false;
                        Tinstock.IsEnabled = false;
                        Tprice.IsEnabled = false;
                        UpDateButton.IsEnabled = false;
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            product = bl.Product.ManagerDetailsProduct(id, 0.7);
                            CategoryBox.IsEnabled = false;
                            Tinstock.IsEnabled = false;
                            Tprice.IsEnabled = false;
                            UpDateButton.IsEnabled = false;
                        }
                        else
                        {
                            product = bl.Product.ManagerDetailsProduct(id);
                            UpDateButton.IsEnabled = true;
                        }
                    }
                   
                }
                
                DataContext = product;
                //Tid.Text = product.ProductID.ToString();
                //Tname.Text = product.ProductName;
                //Tprice.Text = product.Price.ToString();
                //Tinstock.Text=product.InStock.ToString();
                //CategoryBox.SelectedItem = (BO.Enums.CATEGORY?)product.category;
            }
            catch(BO.DoesntExistException ex)
            {
                MessageBox.Show(ex.Message);
                Close();      
            }    
        }

        private void Tid_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( Tid.Text != "")
            {
                if (int.TryParse(Tid.Text, out int value))
                {
                    p.ProductID= value;
                }
            }
        }

        private void Tname_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( Tname.Text != "")
            {
                p.ProductName = Tname.Text;
            }
        }
        private void Tprice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Tprice.Text != "")
            {
                if (double.TryParse(Tprice.Text, out double value))
                {
                    p.Price = value;
                }
            }
        }

        private void Tinstock_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ( Tinstock.Text != "")
            {
                if (int.TryParse(Tinstock.Text, out int value))
                {
                    p.InStock = value;
                }
                
            }
        }
        //private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    p.category = (BO.Enums.CATEGORY)CategoryBox.SelectedItem;//save the category picked
        //}

        private void AddButton_Click(object sender, RoutedEventArgs e)
        { 
            p.IsDeleted = false;
            try
            {
                p.category = CategoryBox.SelectedIndex == -1 ? null :(BO.Enums.CATEGORY)CategoryBox.SelectedItem;
                if(p.category == null)
                {
                    MessageBox.Show("חובה לבחור קטגוריית מוצר");
                }
                else
                {   
                    bl.Product.AddProduct(p);
                    //new ProductListWindow().Show();
                    this.Close();
                    //clean();
                }    
            }
            catch (BO.AlreadyExistException ex)
            {
                MessageBox.Show("Error adding product\n", ex.Message);
                clean();
            }
            catch (BO.RequestFailed ex)
            {
                MessageBox.Show("Error adding product\n", ex.Message);
                clean();
            }

        }
        private void ToProductList_Click(object sender, RoutedEventArgs e) => new ProductListWindow().Show();

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           try
            {
                p.category = CategoryBox.SelectedIndex == -1 ? null : (BO.Enums.CATEGORY)CategoryBox.SelectedItem;
                if (p.category == null)
                {
                    MessageBox.Show("חובה לבחור קטגוריית מוצר");
                }
                bl!.Product.UpdateProduct(p);
                clean();
                //new ProductListWindow().Show();
                //Close();
            }
            catch (BO.DoesntExistException ex)
            {
                MessageBox.Show("Error Update product\n", ex.Message);
                clean();

            }
            catch (BO.RequestFailed ex)
            {
                MessageBox.Show("Error Update product\n", ex.Message);
                clean();
            }
        }
        private void clean()
        {
            Tid.Clear();
            Tname.Clear();
            Tprice.Clear();
            Tinstock.Clear();
           CategoryBox.SelectedIndex = 7;
        }    
    }
}
