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
        int index = DS.items.FindIndex(x => x?.ID == id&& x?.IsDeleted!=true);
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
        int index = DS.items.FindIndex(x => x?.ID == ord.ID);
        if (index == -1)
        {
            ord.ID = DataSource.Config.NextOrderNumberOrderItem;
            DS.items.Add(ord);
            return ord.ID;
        }
        else//הפריט בהזמנה נמצא ברשימה
        {
            if (DS.items[index]?.IsDeleted == false)
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
        int index = DS.items.FindIndex(x => x?.ID == id && x?.IsDeleted != true);
        if (index == -1)
        {
            throw new DoesntExistException("the Order Item for Delete is not exist in list of items!!!");
        }
        else//האיבר קיים ברשימה
        {
            if (DS.items[index]?.IsDeleted != true)
            {
                OrderItem p =(OrderItem) DS.items[index];
                p.IsDeleted = true;
                DS.items[index] = (OrderItem?)p;
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
        OrderItem? OrderItemById = DS.items.Find(x => x?.ID == id && x?.IsDeleted != true);
        if (OrderItemById==null)
        {
            throw new DoesntExistException("the Order Item is not exist in list of OrderItems!!!");
        }
        else
        {
            return (OrderItem)OrderItemById;
        }
    }
    // את ?לבדוקקקקקקקקקק
    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)//החזרת רשימת פריטים בהזמנה
    {      
        if (DS.items.Count==0)
        {
            throw new notExistElementInList("The List of items is Empty!!");
        }
        else
        {
            if(filter==null)//כך שתביא אוסף מופעים מלא, אם התקבל בפרמטר הנ"ל null
            {
                IEnumerable<OrderItem?> items = (from OrderItem? orderitem in DS.items where orderitem?.IsDeleted == false select orderitem).ToList();
                return items;
            }
            else//אחרת היא תחזיר אוסף "מסונן" ע"י המתודה שבפרמטר הנ"ל
            {
                IEnumerable<OrderItem?> items = (from OrderItem? orderitem in DS.items where orderitem?.IsDeleted == false && filter(orderitem) select orderitem).ToList();
                return items;
            }    
        }
    }
    public OrderItem getbyfilter(Func<OrderItem?, bool> filter)//מתודת בקשה של אובייקט בודד
    {
        OrderItem? OrderItemById = DS.items.Find(x => filter(x)&& x?.IsDeleted == false);
        if (OrderItemById == null)
        {
            throw new DoesntExistException("the Order Item is not exist in list of OrderItems!!!");
        }
        else
        {
            return (OrderItem)OrderItemById;
        }
    }


}




