using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
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
        public static ObservableCollection<BO.OrderItem> convertListToObservable(List<BO.OrderItem>list)
        {
            ObservableCollection<BO.OrderItem> lists = new ObservableCollection<BO.OrderItem>();
            foreach (BO.OrderItem item in list)
            {
                lists.Add(item);
            }
            return lists;
        }
    }
}


