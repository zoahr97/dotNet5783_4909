
using BO;
using Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Collections;
using BlApi;
using System.Linq;
using System;

namespace BlImplementation;

internal class Order : BlApi.IOrder
{
    private IDal Dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");//שדה פרטי

    //    בקשת רשימת הזמנות(מסך מנהל)
    //תבקש רשימת הזמנות משכבת הנתונים
    //תבנה על בסיס הנתונים רשימת הזמנות מטיפוס OrderForList(ישות לוגית)
    //תחזיר את הרשימה שנבנתה
    public IEnumerable<BO.OrderForList?> GetAllOrderForList(Func<BO.OrderForList?, bool>? filter = null)//בקשת רשימת ההזמנות של הלקוחות
    {
        try
        {
            Func<DO.Order?, bool> myDelegate = check3;
            IEnumerable<DO.Order?> orders = Dal.Order.GetAll(/*myDelegate*/);
            IEnumerable<DO.OrderItem?> orderItems = Dal.OrderItem.GetAll();
            List<BO.OrderForList?> orderForLists = new List<BO.OrderForList?>();
           
            foreach (DO.Order order in orders)
            {
                BO.OrderForList orderForList = new BO.OrderForList
                {
                    OrderID = order.ID,
                    CustomerName = order.CustomerName,
                    OrderStatus = GetStatus(order),
                    AmountItems = count(order.ID),
                    TotalOrder = SumOrder(order.ID)
                };
                //if (orderForList.AmountItems != 0)
                //{
                    orderForLists.Add(orderForList);
                //}
            }
           
            if (filter == null)
            {
                return orderForLists;
            }
            else
            {
                IEnumerable<BO.OrderForList?> p1 = (from BO.OrderForList? product in orderForLists where filter(product) select product).ToList();
                return p1;
            }
        }
        catch (DO.notExistElementInList ex)
        {
            throw new BO.notExistElementInList(ex.Message, ex);
        }
        
    }

    public static bool check3(DO.Order? order)//כל ההזמנות שנשלחו
    {
        if (order?.DeliveryDate!= null)
            return true;
        else
            return false;
    }

    private int count(int id)//כמות פריטי הזמנה של הלקוח
    {
        try
        {
            int x=Dal.OrderItem.GetAll().Where(p => p?.OrderID == id).Count();
            return x;
        }
        catch (DO.notExistElementInList ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }
    }
  
    public double SumOrder(int orderid)//מתודה המחזירה את הסכום הכולל לתשלום של ההזמנה
    {    
        try
        {
            double? x = Dal.OrderItem.GetAll().Where(p => p?.OrderID == orderid).Sum(p => p?.Price * p?.Amount);
            double sum=Convert.ToDouble(x);
            return sum;
        }
        catch (DO.notExistElementInList ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
        }

    }
    private BO.Enums.OrderStatus GetStatus(DO.Order order)//החזרת מצב הזמנה :אושרה/נשלחה/סופקה
    {
        if (order.DeliveryDate != null)
            return BO.Enums.OrderStatus.ProvidedOrder;
        if (order.ShipDate != null)
        {
            return BO.Enums.OrderStatus.SentOrder;
        }
        else//ההזמנה עדיין לא נישלחה ללקוח
        {
            return BO.Enums.OrderStatus.ConfirmOrder;
        }
       
    }
  
    private List<BO.OrderItem> GetListOrderItemById(int id)//מתודה המחזירה את רשימת פרטי הזמנה ששייכים להזמנה של הלקוח
    {
        List<BO.OrderItem> list = new List<BO.OrderItem>();
        foreach (DO.OrderItem o in Dal.OrderItem.GetAll())
        {
            if (o.OrderID == id)
            {
                DO.Product p = Dal.Product.GetById(o.ProductID);
                BO.OrderItem item = new BO.OrderItem
                {
                    OrderItemID = o.ID,
                    ProductID = o.ProductID,
                    ProductName = p.ProductName,
                    Price = o.Price,
                    Amount = o.Amount,
                    TotalPrice = o.Price * o.Amount,
                                 
                };
                list.Add(item);
            }
        }
        return list;
    }

    //    בקשת פרטי הזמנה(עבור מסך מנהל ומסך קונה)
    //תקבל מזהה הזמנה
    //אם המזהה הוא מספר חיובי - תבצע ניסיון בקשת הזמנה משכבת נתונים
    //תְּבַצֵּעַ ניסיון בקשת פריטי הזמנה משכבת נתונים
    //תבנה אובייקט הזמנה(ישות לוגית) על בסיס הנתונים שהתקבלו וחישוב מידע חסר
    //תחזיר אובייקט הזמנה שנבנה
    //תזרוק חריגה מתאימה משלה בקשת מוצר נכשלה(מוצר לא קיים בשכבת נתונים - תפיסת חריגה)

    public BO.Order GetBoOrder(int id)//בקשת פרטי הזמנה קיימת
    {
        if (id <= 0)
        {
            throw new BO.RequestFailed("The Request is Failed");
        }
        else
        {
            try
            {
                DO.Order ord = Dal.Order.GetById(id);//get right DO Order
                double priceTemp = 0;
                foreach (DO.OrderItem o in Dal.OrderItem.GetAll())
                {
                    if (o.OrderID == id)
                    {
                        priceTemp += o.Price * o.Amount;//add up all of prices in the order
                    }
                }
                BO.Order p=new BO.Order
                {
                    OrderID = id,
                    CustomerName = ord.CustomerName,
                    CustomerEmail = ord.CustomerEmail,
                    CustomerAdress = ord.CustomerAdress,
                    OrderStatus = GetStatus(ord),
                    OrderDate = ord.OrderDate,
                    ShipDate = ord.ShipDate,
                    DeliveryDate = ord.DeliveryDate,
                    TotalOrder = SumOrder(id),
                    IsDeleted = ord.IsDeleted,
                    Items = GetListOrderItemById(id)
                    
                };
                if(p.TotalOrder!=0)
                {
                    return p;
                }
                else
                {
                    throw new BO.RequestFailed("The Request is Failed ");
                }
                
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);
            }
            catch (DO.notExistElementInList ex)
            {
                throw new BO.notExistElementInList(ex.Message, ex);
            }
        }
}

//    עדכון שילוח הזמנה(מסך ניהול הזמנה של מנהל)
//תקבל מספר הזמנה
//תבדוק האם הזמנה קיימת(בשכבת נתונים) ועוד לא נשלחה
//תעדכן את תאריך הַשִּלּוּחַ בהזמנה(בישות נתונים וגם בישות לוגית)
//לבצע ניסיון עדכון הזמנה לשכבת נתונים
//תחזיר אובייקט הזמנה מעודכן של ישות לוגית
//תזרוק חריגה מתאימה במקרה של בעיה כלשהי(לפי הבדיקות והניסיונות כנ"ל)

public BO.Order ShipUpdate(int orderId)//עדכון שילוח הזמנה
{
        if(isExist1(orderId)==true)//הזמנה קיימת(בשכבת נתונים) ועוד לא נשלחה
        {
            try
            {
                DO.Order order1 = Dal.Order.GetById(orderId);//get the order from DO of orderId-or catch exception

                DO.Order order = new DO.Order
                {
                    ID = orderId,
                    CustomerAdress = order1.CustomerAdress,
                    CustomerEmail = order1.CustomerEmail,
                    CustomerName = order1.CustomerName,
                    OrderDate = order1.OrderDate,
                    ShipDate = DateTime.Now,
                    DeliveryDate = null,
                    IsDeleted = order1.IsDeleted//בד"כ תמיד יהיה false
                };
               
                Dal.Order.Update(order);//update the order in DO
                double? priceTemp = 0;

                foreach (DO.OrderItem? temp in Dal.OrderItem.GetAll())
                {
                    if ( temp?.OrderID == order.ID)
                    {
                        priceTemp += temp?.Price * temp?.Amount;//add up all of prices in the order
                    }
                }
                double priceTemp1=Convert.ToDouble(priceTemp);
                return new BO.Order
                {
                    OrderID = orderId,
                    CustomerAdress = order1.CustomerAdress,
                    CustomerEmail = order1.CustomerEmail,
                    CustomerName = order1.CustomerName,
                    OrderDate = order1.OrderDate,//  מחקתי את .value בשורה זו לבדוקק עם הערך 
                    ShipDate = DateTime.Now,
                    OrderStatus = GetStatus(order),
                    TotalOrder = priceTemp1,
                    DeliveryDate = null,
                    IsDeleted = order1.IsDeleted
                };//new BO Order   
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);
            }
            catch (DO.notExistElementInList ex)
            {
                throw new BO.notExistElementInList(ex.Message, ex);
            }
        }
        else
        {
            throw new BO.RequestFailed("Request Failed");
        }      
    }
  //  תבדוק האם הזמנה קיימת(בשכבת נתונים) ועוד לא נשלחה
    private bool isExist1(int orderid)
    {
        foreach(DO.Order?order in Dal.Order.GetAll())
        {
            if(order?.ID == orderid && order?.ShipDate == null)
            {
                return true;
            }
        }
        return false;
    }

    //    עדכון אספקת הזמנה(מסך ניהול הזמנה של מנהל)
    //תקבל מספר הזמנה
    //תבדוק האם הזמנה קיימת(בשכבת נתונים), כבר נשלחו אך עוד לא סופקה
    //תעדכן את תאריך האספקה בהזמנה(בישות נתונים וגם בישות לוגית)
    //לבצע ניסיון עדכון הזמנה לשכבת נתונים
    //תחזיר אובייקט הזמנה(ישות לוגית) מעודכן
    //תזרוק חריגה מתאימה במקרה של בעיה כלשהי(לפי הבדיקות והניסיונות כנ"ל)

    public BO.Order DeliveredUpdate(int orderId)//עדכון אספקה/מסירת הזמנה
    {
        if(isExist2(orderId))//הזמנה קיימת(בשכבת נתונים), כבר נשלחו אך עוד לא סופקה
        {
            try
            {
                DO.Order oId = Dal.Order.GetById(orderId);

                DO.Order order = new DO.Order
                {
                    ID = orderId,
                    CustomerAdress = oId.CustomerAdress,
                    CustomerEmail = oId.CustomerEmail,
                    CustomerName = oId.CustomerName,
                    OrderDate = oId.OrderDate,
                    ShipDate = oId.ShipDate,
                    DeliveryDate = DateTime.Now,
                    IsDeleted = oId.IsDeleted
                };
                Dal.Order.Update(order);
                double?priceTemp = 0;
                foreach (DO.OrderItem?temp in Dal.OrderItem.GetAll())
                {
                    if ( temp?.OrderID == order.ID)
                    {
                        priceTemp += temp?.Price * temp?.Amount;
                    }
                }
                double priceTemp1=Convert.ToDouble(priceTemp);
                return new BO.Order
                {
                    OrderID = orderId,
                    CustomerAdress = oId.CustomerAdress,
                    CustomerEmail = oId.CustomerEmail,
                    CustomerName = oId.CustomerName,
                    OrderDate = oId.OrderDate,
                    ShipDate = oId.ShipDate,
                    DeliveryDate = DateTime.Now,
                    OrderStatus = GetStatus(order),
                    TotalOrder = priceTemp1,
                    IsDeleted = oId.IsDeleted
                };//new BO Order       
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);
            }
            catch (DO.notExistElementInList ex)
            {
                throw new BO.notExistElementInList(ex.Message, ex);
            }

        }
        else
        {
            throw new BO.RequestFailed("Request Failed");
        }
     
    }
    private bool isExist2(int orderid)//תבדוק האם הזמנה קיימת(בשכבת נתונים), כבר נשלחו אך עוד לא סופקה
    {
        foreach (DO.Order?order in Dal.Order.GetAll())
        {
            if(order?.ShipDate != null)
            {
                if (order?.ID == orderid && order?.DeliveryDate == null)
                {
                    return true;
                }
            }           
        }
        return false;
    }
    public BO.OrderTracking OrderTracking(int orderId)//מתודת מעקב הזמנה
    {
            DO.Order tempOrder =Dal.Order.GetById(orderId);
            List<Tuple<DateTime?,string?>?> temporderTracking=new List<Tuple<DateTime?, string?>?>();
            if(tempOrder.OrderDate!=null)
            {
                temporderTracking.Add(new Tuple<DateTime?, string?>(tempOrder.OrderDate, "Order Confirmed"));

            };
            if(tempOrder.ShipDate!=null)
            {
            if (tempOrder.ShipDate < tempOrder.OrderDate)
                throw new BO.RequestFailed("ship date previous then order date ");
                temporderTracking.Add(new Tuple<DateTime?, string?>(tempOrder.ShipDate, "Order Sent "));

            }
            if(tempOrder.DeliveryDate!=null)
            {
                if (tempOrder.OrderDate == null)
                    throw new BO.RequestFailed("Missing Order Date ");
                if (tempOrder.ShipDate == null)
                    throw new BO.RequestFailed("Missing Ship Date ");
                if (tempOrder.DeliveryDate < tempOrder.ShipDate)
                    throw new BO.RequestFailed("Delivery Date previous then ship date ");

                temporderTracking.Add(new Tuple<DateTime?, string?>(tempOrder.DeliveryDate, "Order Delivered "));
            }
            BO.OrderTracking resultordertracking = new BO.OrderTracking()
            {
                OrderID= orderId,
                OrderStatus=GetStatus(tempOrder),
                tracking= temporderTracking
            };
          return resultordertracking;             
    }

}


















































