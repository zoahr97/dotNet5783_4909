using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using System.Xml.Linq;


namespace PL
{
    public partial class OrderTrackingforManeger : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        ObservableCollection<OrderForList?> ordersForList = new();
        IEnumerable<Order> orders;
        BackgroundWorker worker;
        bool isWork = false;
        DateTime nowTime = DateTime.Now; 
        bool inAddingProcess = false;
        public OrderTrackingforManeger()
        {
            InitializeComponent();
            orders = bl!.Order.GetAllOrderForList().Select(x => bl.Order.GetBoOrder((int)x?.OrderID!));
            try
            {
                ordersForList = Castings.convertIenumerableToObservable(bl.Order.GetAllOrderForList().OrderBy(x => x?.OrderID));
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
            DataContext = ordersForList; 
            worker = new() { WorkerReportsProgress = true, WorkerSupportsCancellation = true };
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var Worker = sender as BackgroundWorker;
            foreach (Order? Item in orders)
            {
                orders = bl!.Order.GetAllOrderForList().Select(x => bl.Order.GetBoOrder((int)x?.OrderID!)).OrderBy(x => x.OrderDate);
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }
                switch (Item.OrderStatus)
                {
                    case BO.Enums.OrderStatus.ConfirmOrder:
                        if (Item.OrderDate?.AddDays(15) >= nowTime)
                        {
                            bl.Order.ShipUpdate(Item.OrderID);
                            System.Threading.Thread.Sleep(400);
                        }
                        break;

                    case BO.Enums.OrderStatus.SentOrder:
                        if (Item.ShipDate?.AddDays(12) >= nowTime)
                        {
                            bl.Order.DeliveredUpdate(Item.OrderID);
                            System.Threading.Thread.Sleep(400);   
                        }
                        break;
                }
                
                System.Threading.Thread.Sleep(2000);
            }
           
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            nowTime.AddHours(3);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StartTracking.IsEnabled = true;
            StopTracking.IsEnabled = true;
            if (!inAddingProcess)
                MessageBox.Show("Simulator stopped");
        }
        private void StartTracking_Click(object sender, RoutedEventArgs e)
        {
            if (isWork == false)
            {
                isWork = true;
                StartTracking.IsEnabled = false;
                StopTracking.IsEnabled = true;
                worker.RunWorkerAsync("Test");//מתחיל את התהליכון
            }
        }
        private void StopTracking_Click(object sender, RoutedEventArgs e)
        {
            if (isWork == true)
            {
                isWork = false;
                StartTracking.IsEnabled = true;
                worker.CancelAsync();//מפסיק את התהליכון
            }
        }
        private void DataGridForOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataGridForOrders.SelectedItem is OrderForList orderForList)
            {
                //new OrderDetails(orderForList.OrderID).ShowDialog();
                OrderDetails orderDetails = new OrderDetails(orderForList.OrderID);
                orderDetails.updateshipbutton.Visibility = Visibility.Hidden;
                orderDetails.deliverybutton.Visibility = Visibility.Hidden;
                orderDetails.ShowDialog();
                ordersForList = Castings.convertIenumerableToObservable(bl.Order.GetAllOrderForList().OrderBy(x => x?.OrderID));
                DataContext = ordersForList;
            }
        }
        private void cart_Click(object sender, RoutedEventArgs e)
        {
            new Catalog().ShowDialog();
            ordersForList = Castings.convertIenumerableToObservable(bl.Order.GetAllOrderForList().OrderBy(x => x?.OrderID));
            DataContext = ordersForList;
        }
    }
}
