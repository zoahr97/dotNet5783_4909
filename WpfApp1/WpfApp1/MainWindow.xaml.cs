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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
            for (int i = 0; i < 5; ++i)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = "Item " + i + " from Code";
                cbFromCode.Items.Add(newItem);
            }
            for (int i = 0; i < 5; ++i)
            {
                ComboBox newItem = new ComboBox();
                //newItem.Loaded= "Item " + i + " from Code";
                comboBox.Items.Add(newItem);
            }
        }
       
        private void cbFromCode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(cbFromCode.SelectedItem.ToString());
        }

        private void Addbutton_MouseEnter(object sender, MouseEventArgs e)
        {
            Random r = new Random();

            double Xsize = r.Next((int)this.Width / 2);
            double Ysize = r.Next((int)this.Height / 2);

            Canvas.SetLeft((UIElement)sender, Xsize);
            Canvas.SetTop((UIElement)sender, Ysize);
        }

       
    }
}
