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
        
        public static bool IsNumber(string num)
        {
            string pattern = @"\b[1-9-0\s]+$";
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
            string pattern = @"\b[a-z-A-Z\s ]+$";
            Regex reg = new Regex(pattern);
            return reg.IsMatch(word);
        }
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
                    if(IsNumber(Tid.Text )&& (IsHebrew(Tname.Text)|| IsEnglish(Tname.Text))&& IsNumber(Tprice.Text)&& IsNumber(Tinstock.Text)&& CategoryBox.SelectedIndex!=-1)
                    {
                        bl.Product.AddProduct(p);
                        this.Close();
                    }
                    else
                    {
                        Tid.Text = "";
                        Tname.Text = "";
                        Tprice.Text = "";
                        Tinstock.Text = "";
                        CategoryBox.SelectedIndex = -1;
                        MessageBox.Show("חובה להכניס פרטי מוצר תקינים!!");

                    }
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
                if ( (IsHebrew(Tname.Text) || IsEnglish(Tname.Text)) && IsNumber(Tprice.Text) && IsNumber(Tinstock.Text) && CategoryBox.SelectedIndex != -1)
                {
                    bl!.Product.UpdateProduct(p);
                    this.Close();
                }
                else
                {
                    Tname.Text = "";
                    Tprice.Text = "";
                    Tinstock.Text = "";
                    CategoryBox.SelectedIndex = -1;
                    MessageBox.Show("חובה להכניס פרטי מוצר תקינים!!");

                }
                clean();
                
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
