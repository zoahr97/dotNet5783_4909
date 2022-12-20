
using DO;

namespace BO;

public class ProductForList//ישות עזר של מוצר ברשימה
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
    /// קטגוריה של המוצר
    /// </summary>
    public Enums.CATEGORY? category { get; set; }
    /// <summary>
    /// מחיר המוצר
    /// </summary>
    public double? Price { get; set; }

    /**********ToString********************/
    public override string ToString() => $@"
     ProductID: {ProductID}, 
	 ProductName : {ProductName}
     ProductCategory: {category}
     Price:  {Price}    
	";
}
