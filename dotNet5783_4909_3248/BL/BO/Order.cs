

using DO;
using static DO.Enums;
using System.Diagnostics;

namespace BO;
//לבדוק אנומרציות
public class Order//ישות לוגית ראשית של הזמנה
{
    /// <summary>
    /// מספר מזהה של הזמנה
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// שם הלקוח המזמין
    /// </summary>
    public String? CustomerName { get; set; }
    /// <summary>
    //כתובת דוא"ל של הלקוח המזמין
    /// </summary>
    public String? CustomerEmail { get; set; }
    /// <summary>
    /// כתובת(איזור מגורים) של הלקוח הקונה
    /// </summary>
    public String? CustomerAdress { get; set; }
    /// <summary>
    /// מצב הזמנה
    /// </summary>
    public Enums.OrderStatus OrderStatus { get; set; }
    /// <summary>
    /// תאריך יצירת הזמנה/תאריך אישור/ביצוע הזמנה
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// תאריך משלוח
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// תאריך אספקה/מסירה
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// רשימת פרטי הזמנה
    /// </summary>
    public List<OrderItem>? Items { get; set; }//הוספתי ?
    /// <summary>
    /// מחיר כולל של ההזמנה
    /// </summary>
    public double TotalOrder { get; set; }
    /// <summary>
    /// ?האם האיבר מחוק או לא
    /// </summary>
    public bool IsDeleted { get; set; }


    //*********ToString**********//
    public override string ToString()
    {
        string s = "OrderID:" + OrderID + "\n CustomerName:" + CustomerName + "\n CustomerEmail:" + CustomerEmail +
            "\n CustomerAdress:" + CustomerAdress + "\nOrderStatus: " + OrderStatus
            + "\n OrderDate:" + OrderDate + "\n ShipDate:" + ShipDate + "\n DeliveryDate:"
            + DeliveryDate + "\n " +"\n "+ " Items In Order :"+ "\n";
         
        if (Items != null)
        {
            foreach (OrderItem orderItem in Items)
            {
                s += "\n" + orderItem.ToString();
            }
        }
        
        s += "\n Total payment:" + " "  + TotalOrder+ " " + "NIS" +" ";//סה"כ לתשלום
        return s;
    }
    

}
