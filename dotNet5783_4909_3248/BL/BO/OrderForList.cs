

using DO;
using static DO.Enums;
using System.Diagnostics;

namespace BO;

public class OrderForList//ישות עזר של הזמנה ברשימה OrderForList
{
    /// <summary>
    /// מספר מזהה של הזמנה
    /// </summary>
    public int OrderID { get; set; }
    /// <summary>
    /// שם הלקוח המזמין
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// מצב הזמנה
    /// </summary>
    public Enums.OrderStatus OrderStatus { get; set; }
    /// <summary>
    ///  כמות פריטים בהזמנה
    /// </summary>
    public int AmountItems { get; set; }
    /// <summary>
    /// סה"כ לתשלום/ מחיר כולל של הזמנה
    /// </summary>
    public double? TotalOrder { get; set; }

    /**********ToString********************/
    public override string ToString() => $@"
     OrderID: {OrderID}, 
	 CustomerName : {CustomerName}
     OrderStatus: {OrderStatus}
     AmountItems:  {AmountItems}
     TotalOrder:{TotalOrder}
	";
}
