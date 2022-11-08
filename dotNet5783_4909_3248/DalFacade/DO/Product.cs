namespace DO;

/// <summary>
/// יישות מוצר
/// </summary>

public struct Product
{
    /// <summary>
    /// מספר מזהה של מוצר/הברקוד של המוצר
    /// </summary>
    public int ProductID { get; set; }      
    /// <summary>
    /// שם המוצר
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// קטגוריה
    /// </summary>
    public string? category { get; set; }
    /// <summary>
    /// מחיר המוצר
    /// </summary>
    public double? Price { get; set; }
    /// <summary>
    /// כמות במלאי
    /// </summary>
    public int? InStock { get; set; }

    /***********ToString***************/

    public override string ToString() => $@"
	Product ID={ProductID}
    Product Name:{ProductName}, 
	category - {category}
    Price: {Price}
    Amount in stock: {InStock}
	";

}
