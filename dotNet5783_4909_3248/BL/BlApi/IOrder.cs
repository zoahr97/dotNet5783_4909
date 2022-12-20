

using BO;

namespace BlApi;

public interface IOrder
{
    public List<BO.OrderForList> GetAllOrderForList();
    public BO.Order GetBoOrder(int id);
    public BO.Order ShipUpdate(int orderId);
    public BO.Order DeliveredUpdate(int orderId);
    public BO.OrderTracking OrderTracking(int orderId);
}


//תוספות של מתודות שאני הוספתי
    //public List<BO.OrderItem> GetListOrderItemById(int id);//מתודה המחזירה את רשימת פרטי הזמנה ששייכים להזמנה של הלקוח
    //public double SumOrder(int orderid);//  מתודה המחזירה את הסכום הכולל לתשלום של ההזמנה
    //public bool isExist(List<BO.OrderForList> list, int id);//מתודה הבודקת האם ההזמנה קיימת ברשימת ההזמנות
