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
    /// Interaction logic for ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private IBl bl = Bl.Instance;
        public ProductListWindow()
        {
            InitializeComponent();
            try
            {
                ProductsListView.ItemsSource = bl.Product.GetProductsForList();
            }
            catch(BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
            
        }
        private void Addbutton_Click(object sender, RoutedEventArgs e)
        { 
            new ProductWindow().Show();
        }
        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Func<BO.ProductForList?, bool>? mydelegate = SelectorCategory;//ע"י ביטוי למבדה
            ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate);
        }
        private bool SelectorCategory(BO.ProductForList? p)
        {
                BO.Enums.CATEGORY c = (BO.Enums.CATEGORY)AttributeSelector.SelectedItem;
                if (p?.category == c)
                    return true;
                else
                    return false;     
        }

        private void Returnbutton_Click(object sender, RoutedEventArgs e)
        {
            if (AttributeSelector.SelectedIndex != -1)
            {
                AttributeSelector.SelectedIndex = 7;
                ProductsListView.ItemsSource = bl.Product.GetProductsForList();
            }
        }

        private void ProductsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = sender as ListBox;
            BO.ProductForList product=new ProductForList();
            product=listBox.SelectedItem as BO.ProductForList;
            new ProductWindow(product.ProductID).Show();
            Close();
        }

        
    }
}
