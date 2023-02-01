using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL
{
    public class statusToProgressBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Random random = new Random();
            if (!(value is BO.Enums.OrderStatus))
            {
                throw new Exception("Error");
            }
            var status = (BO.Enums.OrderStatus)value;
            if (status.ToString() == "ConfirmOrder")
            {
                return random.Next(1,33);
            }
            else if (status.ToString() == "SentOrder")
            {

                return random.Next(33, 66);
            }
            else return 100;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class Castings
    {
        public static ObservableCollection<BO.OrderForList?>convertIenumerableToObservable(IEnumerable<BO.OrderForList?> list)        
        {
            ObservableCollection<BO.OrderForList?> lists = new ObservableCollection<BO.OrderForList?>();
            foreach (BO.OrderForList?item in list)
            {
                lists.Add(item);
            }
            return lists;
        }
       
        public static ObservableCollection<BO.ProductForList?> convertIenumerableToObservable(IEnumerable<BO.ProductForList?> list)
        {
            ObservableCollection<BO.ProductForList?> lists = new ObservableCollection<BO.ProductForList?>();
            foreach (BO.ProductForList? item in list)
            {
                lists.Add(item);
            }
            return lists;
        }
        public static ObservableCollection<BO.OrderItem> convertListToObservable(List<BO.OrderItem>list,double discont=1)
        {
            ObservableCollection<BO.OrderItem> lists = new ObservableCollection<BO.OrderItem>();
            foreach (BO.OrderItem item in list)
            {
                item.Price = discont ==1? item.Price : item.Price-item.Price *discont ;
                item.TotalPrice = item.Price*item.Amount;
                lists.Add(item);
            }
            return lists;
        }
    }
}
