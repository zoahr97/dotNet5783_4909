namespace DO;

/// <summary>
/// יישות מוצר
/// </summary>
//הן בישויות הנתונים(DO) והן בישויות לוגיות(BO) נהפוך לטיפוסי מתאפסים(בעזרת הוספת "?" - Nullable Value/Reference types) את טיפוסי כל התכונות למעט מזהים ותכונות מהטיפוסים הבאים: int, double, bool.
public struct Product
{
    //int, double, bool.
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
    public Enums.CATEGORY? category { get; set; }
    /// <summary>
    /// מחיר המוצר ליחידה
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// כמות במלאי
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// האם האיבר מחוק או לא
    /// </summary>
    public bool IsDeleted { get; set; }

    /***********ToString***************/

    public override string ToString() => $@"
	Product ID={ProductID}
    Product Name:{ProductName}, 
	category - {category}
    Price: {Price}
    Amount in stock: {InStock}
    IsDeleted:{IsDeleted}
	";

}
