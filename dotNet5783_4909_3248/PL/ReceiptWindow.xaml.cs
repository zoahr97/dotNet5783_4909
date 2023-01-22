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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing.Printing;
using System.Printing;
using System.Windows.Documents;

namespace PL
{
    /// <summary>
    /// Interaction logic for ReceiptWindow.xaml
    /// </summary>
    public partial class ReceiptWindow : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public Cart cart = Catalog.cart;

        public ReceiptWindow(int amount)
        {
            InitializeComponent();
            Tcustomername.Text = cart?.CustomerName?.ToString();
            TEmail.Text = cart?.CustomerEmail?.ToString();
            Tcustomeradress.Text = cart?.CustomerAdress?.ToString();
            Tprice.Text =cart?.TotalPriceCart.ToString();
            Tamountofpayment.Text =amount.ToString();
            dataGridItems.ItemsSource = cart?.Items;
            datePicker.Text = DateTime.Now.ToLongDateString();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {   
            //PrintDialog printDlg = new PrintDialog();
            //printDlg.ShowDialog();
            FlowDocument doc = new FlowDocument(new Paragraph(new Run("Some text goes here")));
            doc.Name = "FlowDoc";
            /// Create IDocumentPaginatorSource from FlowDocument
            IDocumentPaginatorSource p= (IDocumentPaginatorSource)doc;
            IDocumentPaginatorSource idpSource = doc;
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(this, "Window Printing.");
            MessageBox.Show("הקבלה הופקה בהצלחה!");
        }

        
    }
}
