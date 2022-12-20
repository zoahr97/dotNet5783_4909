

namespace BO;

public class OrderItem//(ישות עזר לוגית של פריט בהזמנה (מייצג שורה בהזמנה
{
    /// <summary>
    /// מספר מזהה של פריט בהזמנה
    /// </summary>
    public int OrderItemID { get; set; }
    /// <summary>
    /// מספר מזהה של מוצר/ברקוד
    /// </summary>
    public int ProductID { get; set; } 
    /// <summary>
    /// שם המוצר
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// מחיר  המוצר ליחידה 
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// כמות פריטים של המוצר בהזמנה
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// (מחיר כולל של פריט בהזמנה (לפי מחיר מוצר וכמותו בהזמנה
    /// </summary>
    public double? TotalPrice { get; set; }
    ///// <summary>
    ///// האם האיבר נמחק או לא
    ///// </summary>
    //public bool? IsDeleted { get; set; }
    /***********ToString***************/

    public override string ToString() => $@"
	 OrderItemID: {OrderItemID}
     ProductID: {ProductID}, 
	 ProductName : {ProductName}
     Price:  {Price}
     Amount:  {Amount}
     TotalPrice: {TotalPrice}
	";
}
