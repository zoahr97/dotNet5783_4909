namespace DO;

/// <summary>
/// יישות הזמנה
/// </summary>

public struct Order//יישות הזמנה
{
    /// <summary>
    /// מספר מזהה ייחודי/מספר רץ אוטומטי
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// שם הלקוח המזמין
    /// </summary>
    public String? CustomerName { get; set; }
    /// <summary>
    /// כתובת דוא"ל
    /// </summary>
    public String? CustomerEmail { get; set; }
    /// <summary>
    /// כתובת למשלוח
    /// </summary>
    public String? CustomerAdress { get; set; }
    /// <summary>
    /// תאריך יצירת הזמנה
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// תאריך משלוח
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// תאריך מסירה
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// ?האם האיבר מחוק או לא
    /// </summary>
    public bool IsDeleted { get; set; }

    /***************ToString*****************************/

    public override string ToString() => $@"
	
     ID:{ID}
     Customer Name:{CustomerName}, 
     Customer Email:{CustomerEmail}
     Customer Adress:{CustomerAdress}
     Order Date:{OrderDate}
     Ship Date: {ShipDate}
     Delivery Date: {DeliveryDate}
	";  

}
