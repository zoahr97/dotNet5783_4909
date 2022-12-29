
using BO;

namespace BlApi;
//לבדוק את/להוסיף nullable?
public interface IProduct//הצהרת פעולות על מוצר
{
    public IEnumerable<BO.ProductForList?> GetProductsForList(Func<BO.ProductForList?, bool>? filter= null);  //1.בקשת רשימת מוצרים
    public BO.Product ManagerDetailsProduct(int productId);///2.בקשת פרטי מוצר עבור מסך מנהל
    public BO.ProductItem CatalogDetailsProduct(int productId, BO.Cart c);////3.בקשת פרטי מוצר עבור מסך הקטלוג המוצרים
    public void AddProduct(BO.Product product);//4.הוספת מוצר
    public void DeleteProduct(int productId);//5.מחיקת מוצר
    public void UpdateProduct(BO.Product product);//6.עדכון נתוני מוצר   
}
//public BO.Product RequestProductDetails1(int productId);//2.בקשת פרטי מוצר
//public BO.ProductItem RequestProductDetails2(int productId,BO.Cart c);//3.
//סוג ערך מוחזר במתודות של BlApi המחזירות אוספים (רק טיפוסי אלמנטים) - כנ"ל לגבי מתודות של DalApi.ICrud
