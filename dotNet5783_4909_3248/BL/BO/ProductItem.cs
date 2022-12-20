

using DO;

namespace BO;

public class ProductItem// (ישות עזר לוגי של פריט מוצר (המייצג מוצר עבור הקטלוג 
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
    public double? Price { get; set; }  
    /// <summary>
    /// קטגוריה
    /// </summary>
    public Enums.CATEGORY? category { get; set; }
    
    /// <summary>
    /// זמין/ האם המוצר קיים במלאי?
    /// </summary>
    public bool? IsStock { get; set; }
    /// <summary>
    /// כמות בסל הקניות של הלקוח/הקונה
    /// </summary>
    public int? AmountInCartOfCostumer { get; set; }

    public override string ToString() => $@"
     ProductID: {ProductID}, 
	 ProductName : {ProductName}
     Price:  {Price}
     category:  {category}
     IsStock: {IsStock}
     AmountInCartOfCostumer:{AmountInCartOfCostumer}
	";

}
