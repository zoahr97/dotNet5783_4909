using BlApi;
using BlImplementation;
using PL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for OrderTrackingforManeger.xaml
    /// </summary>
    public partial class OrderTrackingforManeger : Window
    {
       
        private Stopwatch stopWatch;
        private bool isTimerRun;
        BackgroundWorker timerworker;
       
        public OrderTrackingforManeger()
        {
            InitializeComponent();
            try
            {
                InitializeComponent();
                stopWatch = new Stopwatch();

                timerworker = new BackgroundWorker();
                timerworker.DoWork += Worker_DoWork;
                timerworker.ProgressChanged += Worker_ProgressChanged;
                timerworker.WorkerReportsProgress = true;
               
            }
            catch (BO.notExistElementInList ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void setTextInvok(string text)
        {
            this.timerTextBlock.Text = text;
        }

        private void startTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isTimerRun)
            {
                stopWatch.Restart();
                isTimerRun = true;

                timerworker.RunWorkerAsync();//בשונה מתהליכון רגיל שלא ניתן להפעיל יותר מפעם אחת
                                             //אבל ב BackgroundWorker הפעלה של RunWorkerAsync מייצרת Thread חדש בכל פעם ולכן ניתן להפעילה שוב על אותו אובייקט BackgroundWorker.
            }
        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
            string timerText = stopWatch.Elapsed.ToString();
            timerText = timerText.Substring(0, 8);
            this.timerTextBlock.Text = timerText;
        }



        private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        {
            if (isTimerRun)
            {
               
                stopWatch.Stop();
                isTimerRun = false;
            }
        }

        private void runTimer()
        {

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }
        
        //public void ReportProgress()
        //{
        //    foreach (BO.OrderForList? item in bl.Order.GetAllOrderForList())
        //    {
        //        if(item?.OrderStatus== BO.Enums.OrderStatus.ConfirmOrder)
        //        {
        //            //if (DateTime.Now.CompareTo(bl.Order.OrderTracking(item.OrderID).tracking[0])>0)
        //            //{
        //                item.OrderStatus = BO.Enums.OrderStatus.SentOrder;
        //            orderForLists = Castings.convertIenumerableToObservable(bl.Order.GetAllOrderForList());
        //            DataGridForOrder.DataContext = orderForLists;
        //                //Thread.Sleep(1000);
        //            //}
        //        }
        //        else if(item?.OrderStatus == BO.Enums.OrderStatus.SentOrder )
        //        {
        //            //if (DateTime.Now.CompareTo(bl.Order.OrderTracking(item.OrderID).tracking[1]) > 0)
        //            //{
        //                item.OrderStatus = BO.Enums.OrderStatus.ProvidedOrder;
        //                DataGridForOrder.DataContext = orderForLists;
        //                //Thread.Sleep(2000);
        //            //}
        //        }
        //    }
        //}

    }
}

