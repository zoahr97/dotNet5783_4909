using Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Dal;

internal class DalOrderItem:IOrderItem
{
    /*מתודת הוספת אובייקט שתקבל אובייקט של ישות ותחזיר את המספר המזהה של האובייקט שנוסף
*/
    DataSource DS = DataSource.GetInstance();

    public bool IsExist(int id)
    {
        int index = DS.items.FindIndex(x => x.ID == id&& x.IsDeleted!=true);
        if (index == -1)//כאשר האיבר לא קיים ברשימה
        {
            return false;
        }
        else//האיבר קיים ברשימה
        {
            return true;
        }
    }
    public int Add(OrderItem ord)
    {  
        int index = DS.items.FindIndex(x => x.ID == ord.ID);
        if (index == -1)
        {
            ord.ID = DataSource.Config.NextOrderNumberOrderItem;
            DS.items.Add(ord);
            return ord.ID;
        }
        else//הפריט בהזמנה נמצא ברשימה
        {
            if (DS.items[index].IsDeleted == false)
            {
                throw new AlreadyExistException("Order Item for Adding already exists in the list of items");
            }
            else//המוצר קיים ברשימה אבל הוא בגדר מחוק?
            {
                DS.items.Add(ord);
                return ord.ID;
            }
        }
    }
   public void Delete(int id)
   {
        int index = DS.items.FindIndex(x => x.ID == id && x.IsDeleted != true);
        if (index == -1)
        {
            throw new DoesntExistException("the Order Item for Delete is not exist in list of items!!!");
        }
        else//האיבר קיים ברשימה
        {
            if (DS.items[index].IsDeleted != true)
            {
                OrderItem p = DS.items[index];
                p.IsDeleted = true;
                DS.items[index] = p;
            }
            else//המוצר כבר מחוק
            {
                throw new DoesntExistException("the Order Item for Delete already deleted!!!");
            }
        }
       
   }
    public void Update(OrderItem item)
    {     
        try
        {
            GetById(item.ID);
        }
        catch
        {
            throw new DoesntExistException("the  Order Item for Update is not exist in list of items!!!");
        }
        Delete(item.ID);
        Add(item);
    }

    public OrderItem GetById(int id)
    {
        OrderItem? OrderItemById = DS.items.Find(x => x.ID == id && x.IsDeleted != true);
        if (OrderItemById.Value.ID == 0)
        {
            throw new DoesntExistException("the Order Item is not exist in list of OrderItems!!!");
        }
        else
        {
            return (OrderItem)OrderItemById;
        }
    }
    // את ?לבדוקקקקקקקקקק
    public IEnumerable<OrderItem> GetAll()//החזרת רשימת פריטים בהזמנה
    {      
        if (DS.items.Count==0)
        {
            throw new notExistElementInList("The List of items is Empty!!");
        }
        else
        {
            IEnumerable<OrderItem> items = (from OrderItem orderitem in DS.items where orderitem.IsDeleted == false select orderitem).ToList();
            return items;

            //List<OrderItem>? items = DS.items.FindAll(x => x.IsDeleted != true);
            //return items;
        }
    }

    public List<OrderItem> GetListByOrderID(int OrderID)
    {
        List<OrderItem>? List = new List<OrderItem>();
        foreach (OrderItem item in DS.items)
        {
            if (item.OrderID == OrderID && item.IsDeleted == false)
                List.Add(item);
        }
        if (List.Count == 0)
        {
            throw new notExistElementInList("There is no item/s with this order ID number!!");
        }
        else//כאשר הרשימה אינה ריקה
        {
            return List;
        }
    }
    public OrderItem GetByOrderIDProductID(int OrderID, int ProductID)
    {
        OrderItem item = DS.items.Find(x => x.OrderID == OrderID && x.ProductID == ProductID && x.IsDeleted == false);
        if (item.ID == 0)
        {
            throw new DoesntExistException("The order item for OrderID and ProductID is not exist in List of items!!!");
        }
        else//מצאנו
        {
            return (OrderItem)item;
        }
    }
}







//int Add(T item);
//T GetById(int id);
//void Update(T item);
//void Delete(int id);
//IEnumerable<T> GetAll();

//if (IsExist(item.ID))
//{
//    int index = DS.items.FindIndex(x => x.ID == item.ID);
//    DS.items[index] = item;
//}
//else
//{
//    throw new DoesntExistException("the item for Update is not exist in list of items!!!");
//}

//if (IsExist(id))
//{
//    int index = DS.items.FindIndex(x => x.ID == id);
//    DS.items.RemoveAt(index);
//}
//else
//{
//    throw new DoesntExistException("the OrderItem for Delete is not exist in list of items!!!");
//}

//if(IsExist(order.ID))
//        {
//            throw new AlreadyExistException("Order for Adding already exists in the list of orders");
//        }
//        else
//{
//    order.ID = DataSource.Config.NextOrderNumberOrders;
//    DS.orders.Add(order);
//    return order.ID;
//}

//Product? ProductById = DS.products.Find(x => x.ProductID == id && x.IsDeleted != true);
//if (ProductById.Value.ProductID == 0)
//{
//    throw new DoesntExistException("the product is not exist in list of products!!!");
//}
//else
//{
//    return (Product)ProductById;
//}

//public class DalOrderItem//נתון פריט בהזמנה
//{
//    /*מתודת הוספת אובייקט שתקבל אובייקט של ישות ותחזיר את המספר המזהה של האובייקט שנוסף
//*/
//    public int Add(OrderItem ord)
//    {
//        ord.ID = DataSource.Config.NextOrderNumberOrderItem;
//        int x = DataSource.Config.index2foritems;//האינדקס הפנוי הראשון שיש במערך
//        DataSource.items[x] = ord;
//        DataSource.Config.index2foritems++;
//        return ord.ID;
//    }
//    public OrderItem get(int id)
//    {
//        for (int i = 0; i < DataSource.Config.index2foritems; i++)
//        {
//            if (DataSource.items[i].ID == id)
//            {
//                return DataSource.items[i];
//            }
//        }
//        throw new Exception(" the object not exist in array!");
//    }
//    public int numElementOrderItem()
//    {
//        return DataSource.Config.index2foritems;
//    }
//    public OrderItem[] GetItemslist()
//    {
//        return DataSource.items;
//    }
//    public void delete(int id)
//    {
//        if (Exist(id))
//        {

//            OrderItem[] newitems = new OrderItem[DataSource.items.Length - 1];
//            for (int i = 0; i < DataSource.Config.index2foritems; i++)
//            {
//                if (DataSource.items[i].ID == id)
//                {
//                    for (int j = 0; j < i; j++)
//                    {
//                        newitems[DataSource.Config.ind2] = DataSource.items[j];
//                        DataSource.Config.ind2++;
//                    }
//                    for (int k = i + 1; k < DataSource.Config.index2foritems; k++)
//                    {
//                        newitems[DataSource.Config.ind2] = DataSource.items[k];
//                        DataSource.Config.ind2++;
//                    }
//                }
//            }
//            DataSource.items = newitems;
//            DataSource.Config.index2foritems--;
//        }
//        else//אם הערך למחיקה לא קיים במערך
//        {
//            throw new Exception("the value is not exist in array");
//        }
//    }
//    public bool Exist(int id)
//    {
//        for (int i = 0; i < DataSource.Config.index2foritems; ++i)
//        {
//            if (DataSource.items[i].ID == id)
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//    public void update(OrderItem p)//מתודת עדכון
//    {
//        if (Exist(p.ID))
//        {
//            for (int i = 0; i < DataSource.Config.index2foritems; i++)
//            {
//                if (DataSource.items[i].ID == p.ID)
//                {
//                    DataSource.items[i] = p;//דריסה של האובייקט הישן על ידי החדש
//                }
//            }
//        }
//        else//כאשר האיבר לא נמצא
//            throw new Exception("the value is not exist in array");
//    }

//}