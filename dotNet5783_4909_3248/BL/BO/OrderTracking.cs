
using BlImplementation;
using DO;
using System.Diagnostics;

namespace BO;

public class OrderTracking//ישות עזר של מעקב הזמנה
{
    /// <summary>
    /// מספר מזהה של הזמנה
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// (מצב הזמנה (הזמנה מאושרת, נשלחה, סופקה ללקוח
    /// </summary>
    public Enums.OrderStatus OrderStatus { get; set; }
    /// <summary>
    ///  רשימה של צמדים(תאריך,התקדמות החבילה)1
    /// </summary>
    public List<Tuple<DateTime?, string?>?> tracking { get; set; }

    /***********ToString***************/

    public override string ToString()
    {
        string s = " ";
        s += "OrderID:" + OrderID + "\n" + "OrderStatus:" + OrderStatus + "\n";
         foreach(Tuple<DateTime?, string?>  t in tracking)
        {
            s += "\n" + t + "\n";
        }
         return s;
    }
}

