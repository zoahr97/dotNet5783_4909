namespace DO;

/// <summary>
/// יישות פריט בהזמנה
/// </summary>

public struct OrderItem
{
    /// <summary>
    /// מספר מזהה ייחודי/מספר רץ אוטומטי
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// מספר מזהה של מוצר/ברקוד
    /// </summary>
    public int? ProductID { get; set; }
    /// <summary>
    /// מספר מזהה של הזמנה
    /// </summary>
    public int? OrderID { get; set; }
    /// <summary>
    /// מחיר ליחידה
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// כמות
    /// </summary>
    public int? Amount { get; set; }

    /***********ToString***************/

    public override string ToString() => $@"
	 ID:{ID}
     Product ID:{ProductID}, 
	 Order ID : {OrderID}
     Price: {Price}
     Amount: {Amount}
	";
}
