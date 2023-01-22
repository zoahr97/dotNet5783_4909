using BlApi;
using BlImplementation;
using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
   
    public partial class ProductListWindow : Window
    {
        
        private IBl bl = BlApi.Factory.Get();
        ObservableCollection<BO.ProductForList?> productForLists = new ObservableCollection<BO.ProductForList?>();
        public ProductListWindow()
        {
            InitializeComponent();
            try
            {

                if (Disconts.flag == true)
                {
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList(null,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.7);
                        }
                        else
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList();
                        }
                    }
                    
                }
                
            }
            catch(BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
            
        }
        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(AttributeSelector.SelectedIndex==7)
            {
                if (Disconts.flag == true)
                {
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList(null,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.7);
                        }
                        else
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList();
                        }
                    }
                    
                }
                
                   
            }
            else
            {
                Func<BO.ProductForList?, bool>? mydelegate = SelectorCategory;//
                if (Disconts.flag == true)
                {                                                            //ע"י ביטוי למבדה
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate,0.3);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate, 0.5);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate, 0.7);
                        }
                        else
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate);
                        }
                    }
                   
                }
                
            }   
        }
        private bool SelectorCategory(BO.ProductForList? p)
        {
                BO.Enums.CATEGORY c = (BO.Enums.CATEGORY)AttributeSelector.SelectedItem;
                if (p?.category == c)
                    return true;
                else
                    return false;     
        }

        private void Addbutton_Click(object sender, RoutedEventArgs e)
        {
            new ProductWindow().ShowDialog();
            try
            {
                if (Disconts.flag == true)
                {
                    productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList(null, 0.3)!);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList(null, 0.5)!);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList(null, 0.7)!);
                        }
                        else
                        {
                            productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList()!);
                        }
                    }
                    
                }
               
                    
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            ProductsListView.ItemsSource = productForLists;
            //this.Close();
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox? listBox = sender as ListBox;
            BO.ProductForList? productForList = new BO.ProductForList();
            productForList = listBox?.SelectedItem as BO.ProductForList;
            new ProductWindow(productForList.ProductID).ShowDialog();
            try
            {
                if (Disconts.flag == true)
                {
                    productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList(null, 0.3)!);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList(null, 0.5)!);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList(null, 0.7)!);
                        }
                        else
                        {
                            productForLists = Castings.convertIenumerableToObservable(bl?.Product.GetProductsForList()!);
                        }
                    }
                    
                }
                

            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            ProductsListView.ItemsSource = productForLists;
            //Close();
        }

        //private void Addbutton_Copy_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        flag = true;
        //        ProductsListView.ItemsSource = bl.Product.GetProductsForList(null,0.3);
        //    }
        //    catch (BO.notExistElementInList ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        //private void Addbutton_Copy1_Click(object sender, RoutedEventArgs e)
        //{
        //    Disconts.flag  = false;
        //}
    }
}
