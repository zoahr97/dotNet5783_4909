
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














//int Add(T item);
//T GetById(int id);
//void Update(T item);
//void Delete(int id);
//IEnumerable<T> GetAll();

//if (IsExist(order.ID))
//{
//    int index = DS.orders.FindIndex(x => x.ID == order.ID);
//    DS.orders[index] = order;
//}
//else//כאשר ההזמנה לעדכון לא קיימת ברשימת ההזמנות
//{
//    throw new DoesntExistException("the order for Update is not exist in list of orders!!!");
//}

//if (IsExist(id))
//{
//    int index = DS.orders.FindIndex(x => x.ID == id);
//    DS.orders.RemoveAt(index);
//}
//else
//{
//    throw new DoesntExistException("the order for Delete is not exist in list of orders!!!");
//}

//if (IsExist(id))
//{
//    int index = DS.orders.FindIndex(x => x.ID == id);
//    return DS.orders[index];
//}
//else
//{
//    throw new DoesntExistException("the order is not exist in list of orders!!!");
//}

//if (IsExist(order.ID))
//{
//    throw new AlreadyExistException("Order for Adding already exists in the list of orders");
//}
//else
//{
//    order.ID = DataSource.Config.NextOrderNumberOrders;
//    DS.orders.Add(order);
//    return order.ID;
//}
//public int Add(Order ord)
//{
//    ord.ID = DataSource.Config.NextOrderNumberOrders;
//    int x = DataSource.Config.index3fororders;//המקום הפנוי הראשון במערך
//    DataSource.orders[x] = ord;
//    DataSource.Config.index3fororders++;
//    return ord.ID;
//}

//public Order get(int ID)
//{
//    for (int i = 0; i < DataSource.Config.index3fororders; i++)
//    {
//        if (DataSource.orders[i].ID == ID)
//        {
//            return DataSource.orders[i];
//        }
//    }

//    throw new Exception(" the object not exist in array!");
//}
//public int numElementOrders()
//{
//    return DataSource.Config.index3fororders;
//}

//public Order[] GetOrderslist()//קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים
//{
//    return DataSource.orders;
//}

//public void delete(int ID)
//{
//    if (Exist(ID))
//    {
//        Order[] neworders = new Order[DataSource.orders.Length - 1];
//        for (int i = 0; i < DataSource.Config.index3fororders; i++)
//        {
//            if (DataSource.orders[i].ID == ID)
//            {
//                for (int j = 0; j < i; j++)
//                {
//                    neworders[DataSource.Config.ind3] = DataSource.orders[j];
//                    DataSource.Config.ind3++;
//                }
//                for (int k = i + 1; k < DataSource.Config.index3fororders; k++)
//                {
//                    neworders[DataSource.Config.ind3] = DataSource.orders[k];
//                    DataSource.Config.ind3++;
//                }
//            }
//        }
//        DataSource.orders = neworders;
//        DataSource.Config.index3fororders--;
//    }
//    else//אם הערך למחיקה לא קיים במערך
//    {
//        throw new Exception("the value is not exist in array");
//    }
//}

//public bool Exist(int ID)
//{
//    for (int i = 0; i < DataSource.Config.index3fororders; ++i)
//    {
//        if (DataSource.orders[i].ID == ID)
//        {
//            return true;
//        }
//    }
//    return false;
//}
////**************לבדוק
//public void update(Order p)//מתודת עדכון
//{
//    if (Exist(p.ID))
//    {
//        for (int i = 0; i < DataSource.Config.index3fororders; i++)
//        {
//            if (DataSource.orders[i].ID == p.ID)
//            {
//                DataSource.orders[i] = p;
//            }
//        }
//    }
//    else//כאשר האיבר לא נמצא
//        throw new Exception("the value is not exist in array");
//}