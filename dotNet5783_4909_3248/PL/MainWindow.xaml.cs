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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ToPassWindow_Click(object sender, RoutedEventArgs e)=> new ProductCatalog().Show();

        private void Managerbutton_Click(object sender, RoutedEventArgs e)
        {
            new PasswordManager().Show();
            //new ManagerView().Show();
        }

        private void ToPassWindow1_Click(object sender, RoutedEventArgs e)
        {

            new Catalog().Show();
        }

        private void ToPassWindow2_Click(object sender, RoutedEventArgs e)
        {
            new OrderTracking().Show();
        }
    }
}
