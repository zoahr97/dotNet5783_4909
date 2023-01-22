using BlApi;
using BlImplementation;
using BO;
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
    /// Interaction logic for OrderList.xaml
    /// </summary>
    public partial class OrderList : Window
    {
        private IBl bl = BlApi.Factory.Get();
        ObservableCollection<BO.OrderForList?> orderForLists=new ObservableCollection<BO.OrderForList?>();
        public OrderList()
        {
            InitializeComponent();
            try
            {
                orderForLists = Castings.convertIenumerableToObservable(bl.Order.GetAllOrderForList());
                //DataGridForOrder.ItemsSource = bl.Order.GetAllOrderForList();
                DataGridForOrder.DataContext = orderForLists;
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox.ItemsSource = Enum.GetValues(typeof(BO.Enums.OrderStatus));
        }
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                Func<BO.OrderForList?, bool>? mydelegate = SelectorCategory;//ע"י ביטוי למבדה
                DataGridForOrder.ItemsSource = bl.Order.GetAllOrderForList(mydelegate); 
        }
        private bool SelectorCategory(BO.OrderForList? p)
        {
            BO.Enums.OrderStatus c = (BO.Enums.OrderStatus)comboBox.SelectedItem;
            if (p?.OrderStatus== c)
                return true;
            else
                return false;
        }
        private void DataGridForOrder_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid? listBox = sender as DataGrid;
            BO.OrderForList? orderForList = new BO.OrderForList();
            orderForList = listBox?.SelectedItem as BO.OrderForList;
            new OrderDetails(orderForList.OrderID).ShowDialog();
            try
            {
              orderForLists = Castings.convertIenumerableToObservable(bl.Order.GetAllOrderForList());     
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataGridForOrder.DataContext = orderForLists;
            //Close();
        }

        //private void button1_Click(object sender, RoutedEventArgs e)
        //{
        //    if (comboBox.SelectedIndex != -1)
        //    {
        //        comboBox.SelectedIndex = 3;
        //        DataGridForOrder.ItemsSource = bl.Order.GetAllOrderForList();
        //    }
        //}
    }
}
