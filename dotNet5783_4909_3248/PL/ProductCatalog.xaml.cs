using BlApi;
using BlImplementation;
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
    /// Interaction logic for ProductCatalog.xaml
    /// </summary>
    public partial class ProductCatalog : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public ProductCatalog()
        {
            InitializeComponent();
            try
            {
                if (Disconts.flag == true)
                {
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList(null,0.3).OrderBy(x => x?.Price);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.5).OrderBy(x => x?.Price);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.7).OrderBy(x => x?.Price);
                        }
                        else
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList().OrderBy(x=>x?.Price);
                        }
                    }
                    
                }
               
                    
            }
            catch (BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
            AttributeSelector.ItemsSource = Enum.GetValues(typeof(BO.Enums.CATEGORY));
           
        }
        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AttributeSelector.SelectedIndex == 7)
            {
                if (Disconts.flag == true)
                {
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList(null,0.3).OrderBy(x => x?.Price);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.5).OrderBy(x => x?.Price);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(null, 0.7).OrderBy(x => x?.Price);
                        }
                        else
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList().OrderBy(x => x?.Price);
                        }
                    }
                    
                }
                  
            }
            else
            {
                Func<BO.ProductForList?, bool>? mydelegate = SelectorCategory;//ע"י ביטוי למבדה
                if (Disconts.flag == true)
                {
                    ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate,0.3).OrderBy(x => x?.Price);
                }
                else
                {
                    if (Disconts.flag1 == true)
                    {
                        ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate, 0.5).OrderBy(x => x?.Price);
                    }
                    else
                    {
                        if (Disconts.flag2 == true)
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate, 0.7).OrderBy(x => x?.Price);
                        }
                        else
                        {
                            ProductsListView.ItemsSource = bl.Product.GetProductsForList(mydelegate).OrderBy(x => x?.Price);
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

        private void PrintButton1_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = new FlowDocument(new Paragraph(new Run("Some text goes here")));
            doc.Name = "FlowDoc";
            /// Create IDocumentPaginatorSource from FlowDocument
            IDocumentPaginatorSource p = (IDocumentPaginatorSource)doc;
            IDocumentPaginatorSource idpSource = doc;
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(this, "Window Printing.");
            MessageBox.Show("הקבלה הופקה בהצלחה!");
        }
    }
}
