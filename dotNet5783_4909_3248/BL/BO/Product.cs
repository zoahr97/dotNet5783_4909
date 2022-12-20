
using DO;

namespace BO;

public class Product//ישות לוגית ראשית של מוצר
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
    /// מחיר המוצר
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    ///קטגוריה של המוצר
    /// </summary>
    public Enums.CATEGORY? category { get; set; }
    /// <summary>
    /// כמות במלאי
    /// </summary>
    public int? InStock { get; set; }
    /// <summary>
    /// האם האיבר נמחק או לא
    /// </summary>
    public bool? IsDeleted { get; set; }
    /**********ToString********************/

    public override string ToString() => $@"
     ProductID: {ProductID}, 
	 ProductName : {ProductName}
     ProductCategory: {category}
     Price:  {Price}    
     InStock: {InStock}
	";
}

