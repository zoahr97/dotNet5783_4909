
using DalApi;
using DO;
using System.Security.Cryptography;

namespace Dal;

internal class DalOrder:IOrder
{
    DataSource DS = DataSource.GetInstance();
    public bool IsExist(int id)
    {
        int index = DS.orders.FindIndex(x => x?.ID == id);
        if (index == -1)//כאשר האיבר לא קיים ברשימה
        {
            return false;
        }
        else//האיבר קיים ברשימה
        {
            return true;
        }
    }

    public int Add(Order order )
    {
        int index = DS.orders.FindIndex(x => x?.ID == order.ID);
        if (index == -1)
        {
            order.ID = DataSource.Config.NextOrderNumberOrders;
            DS.orders.Add(order);
            return order.ID;
        }
        else//הפריט בהזמנה נמצא ברשימה
        {
            if (DS.orders[index]?.IsDeleted == false)
            {
                throw new AlreadyExistException("Order for Adding already exists in the list of items");
            }
            else//ההזמנה קיים ברשימה אבל הוא בגדר מחוק
            {
                DS.orders.Add(order);
                return order.ID;
            }
        }      
    }
    public Order GetById(int id)
    {
        Order? OrderById = DS.orders.Find(x => x?.ID == id && x?.IsDeleted != true);
        if (OrderById==null)
        {
            throw new DoesntExistException("the Order is not exist in list of items!!!");
        }
        else
        {
            return (Order)OrderById;
        }
    }
    //לבדוקקקקקקק
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
    {
        if (DS.orders.Count == 0)
        {
            throw new notExistElementInList("The List of orders is Empty!!");
        }
        else
        {  
            if(filter == null)//כך שתביא אוסף מופעים מלא, אם התקבל בפרמטר הנ"ל null
            {
                IEnumerable<Order?> orders = (from Order? order in DS.orders where order?.IsDeleted == false select order).ToList();
                return orders;
            }
            else//אחרת היא תחזיר אוסף "מסונן" ע"י המתודה שבפרמטר הנ"ל
            {
                IEnumerable<Order?> orders = (from Order? order in DS.orders where order?.IsDeleted == false && filter(order) select order).ToList();
                return orders;
            }        
            //List<Order>? orders = DS.orders.FindAll(x => x.IsDeleted != true);
            //return orders;
        }
    }
    public Order getbyfilter(Func<Order?, bool> filter)//מתודת בקשה של אובייקט בודד
    {
        Order? OrderById = DS.orders.Find(x => filter(x) && x?.IsDeleted == false);
        if (OrderById == null)
        {
            throw new DoesntExistException("the Order is not exist in list of items!!!");
        }
        else
        {
            return (Order)OrderById;
        }
    }
    public void Delete(int id)
    {
        int index = DS.orders.FindIndex(x => x?.ID == id && x?.IsDeleted != true);
        if (index == -1)
        {
            throw new DoesntExistException("the Order for Delete is not exist in list of items!!!");
        }
        else//האיבר קיים ברשימה
        {
            if (DS.orders[index]?.IsDeleted != true)
            {
                Order p = (Order)DS.orders[index];
                p.IsDeleted = true;
                DS.orders[index] = (Order?)p;
            }
            else//המוצר כבר מחוק
            {
                throw new DoesntExistException("the Order for Delete already deleted!!!");
            }
        }
    }
    public void Update(Order order)
    {
        try
        {
            GetById(order.ID);
        }
        catch
        {
            throw new DoesntExistException("the  Order  for Update is not exist in list of items!!!");
        }
        Delete(order.ID);
        Add(order);       
    }
}














