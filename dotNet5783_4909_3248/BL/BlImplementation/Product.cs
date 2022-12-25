using DalApi;
using Dal;
namespace BlImplementation;
using System.Linq;
//נמחוק ;using Dal
//נחליף את יצירת מופע של מחלקה DalList לקבלתו ממחלקת היצרן: () DalApi.Factory.Get
internal class Product: BlApi.IProduct//מחלקת מוצר שממשת את ממשק מוצר
{
    private IDal Dal = DalList.Instance;//שדה פרטי

    public IEnumerable<BO.ProductForList> GetProductsForList()//לבדוק את ה IEnumerable
    {
        return Dal.Product.GetAll().Select(product => new BO.ProductForList
        {
           ProductID = product.ProductID,
           ProductName = product.ProductName,
           Price = product.Price,
           category = (BO.Enums.CATEGORY?)product.category
        });     
    }


    public BO.Product ManagerDetailsProduct(int productId)//בקשת פרטי מוצר (עבור מסך מנהל ועבור)
    {
        if (productId <= 0)
        {
            throw new BO.RequestFailed("The Request Of Product is Failed ");
        }
        else//מספר מזהה של מוצר חיובי
        {
            try
            {
                DO.Product product = Dal.Product.GetById(productId);
                BO.Product p = new BO.Product
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    category = (BO.Enums.CATEGORY?)product.category,
                    Price = product.Price,
                    InStock = product.InStock,
                    IsDeleted = product.IsDeleted
                };
                return p;
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);
            }
        }
    }

    public BO.ProductItem CatalogDetailsProduct(int productId, BO.Cart c)//בקשת פרטי מוצר (עבור מסך קונה - מהקטלוג)
    {
        if (productId <= 0)
        {
            throw new BO.RequestFailed("The Request Of Product is Failed ");
        }
        else//מספר מזהה של מוצר חיובי
        {
            try
            {
                DO.Product product = Dal.Product.GetById(productId);
                BO.ProductItem p = new BO.ProductItem
                {
                    ProductID = product.ProductID,
                    ProductName = product.ProductName,
                    Price = product.Price,
                    category = (BO.Enums.CATEGORY?)product.category,
                    IsStock = product.InStock > 0 ? true : false,
                    AmountInCartOfCostumer = c.Items.Count()
                };
                return p;
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);
            }
        }
    }

    public void AddProduct(BO.Product product)//הוספת מוצר לרשימת המוצרים בשכבת הנתונים
    {
        if (product.ProductID > 0 && product.ProductName != " " && product.Price > 0 && product.InStock >= 0)
        {
            DO.Product product1 = new DO.Product();//המרה לישות נתונים
            product1.ProductID = product.ProductID;
            product1.ProductName = product.ProductName;
            product1.category = (DO.Enums.CATEGORY?)product.category;
            product1.Price = product.Price;
            product1.InStock = product.InStock;
            product1.IsDeleted = false;//האם המוצר נמחק?לבדוקקקקקקק
            try
            {
                int id = Dal.Product.Add(product1);
            }
            catch (DO.AlreadyExistException ex)
            {
                throw new BO.AlreadyExistException(ex.Message, ex);
            }
        }
        else//כאשר הנתונים אינם תקינים
        {
            throw new BO.RequestFailed("The Request for Adding a Product Failed!! ");
        }
    }
    public void DeleteProduct(int productId)//מחיקת מוצר
    {
        //תבדוק שהמוצר לא מופיע באף הזמנה
        try
        {
            Dal.Product.Delete(productId);
        }
        catch (DO.DoesntExistException ex)
        {
            throw new BO.DoesntExistException(ex.Message, ex);//זריקת חריגה פנימית
        }
    }

    public void UpdateProduct(BO.Product product)//עדכון מוצר
    {
        if (product.ProductID > 0 && product.ProductName != " " && product.Price > 0.0 && product.InStock >= 0)
        {
            DO.Product product1 = new DO.Product();//המרה לישות נתונים
            product1.ProductID = product.ProductID;
            product1.ProductName = product.ProductName;
            product1.category = (DO.Enums.CATEGORY?)product.category;//שינוי?
            product1.Price = product.Price;
            product1.InStock = product.InStock;
            product1.IsDeleted = false;//המוצר עבור עדכון עדיין לא נמחק
            try
            {
                Dal.Product.Update(product1);
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);//זריקת חריגה פנימית
            }
        }
        else//כאשר הנתונים אינם תקינים
        {
            throw new BO.RequestFailed("The Request for Update of Product Failed!!");
        }
    }
   
}




//הפונקציה GetAll עדיין זורקת חריגה שלא תפסנו
//List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
//try
//{
//    IEnumerable<DO.Product> products = Dal.Product.GetAll();
//    foreach (DO.Product product in products)
//    {
//        BO.ProductForList productForList = new BO.ProductForList
//        {
//            ProductID = product.ProductID,
//            ProductName = product.ProductName,
//            category = (BO.Enums.CATEGORY)product.category,
//            Price = product.Price,
//        };
//        productForLists.Add(productForList);
//    }
//    return productForLists;
//}
//catch (DO.notExistElementInList ex)
//{
//    throw new BO.notExistElementInList(ex.Message, ex);//זריקת חריגה פנימית
//}


//public void UpdateProduct(BO.Product p)
//{
//    if (p.ProductID < 0 || p.ProductName == "" || p.Price <= 0 || p.InStock < 0)
//    {
//        throw new BO.RequestFailed("Incorrect Input");
//    }
//    DO.Product temp = new DO.Product();
//    temp.ProductID = p.ProductID;
//    temp.ProductName = p.ProductName;
//    temp.Price = p.Price;
//    temp.InStock = p.InStock;
//    temp.category = (DO.Enums.CATEGORY)p.category;
//    temp.IsDeleted = false;
//    try
//    {
//        Dal.Product.Update(temp);
//    }
//    catch(DO.DoesntExistException ex)
//    {
//        throw new BO.DoesntExistException(ex.Message,ex);
//    }
//}//get BO product, check if right and updates DO product










//    מחיקת מוצר(עבור מסך מנהל)
//תקבל מזהה מוצר
//תבדוק שהמוצר לא מופיע באף הזמנה
//אם הבדיקה הצליחה - תבצע ניסיון בקשת מחיקה משכבת הנתונים
//תזרוק חריגה מתאימה משלה עם מוצר מופיע בהזמנות או עם לא קיים מוצר כזה כלל

//public void DeleteProduct(int id)
//{
//    ////from DO.Product? item in Dal.Product.GetAll()
//    //var v = from DO.Order? ord in Dal.Order.GetAll()
//    //        where ord != null && ord?.IsDeleted == false
//    //        select from DO.OrderItem? oi in Dal.OrderItem.GetAll()
//    //               where oi != null && oi?.IsDeleted == false && oi?.OrderID == ord?.ID && oi?.ProductID == id
//    //               select oi;
//    //if (v.Any() == false)//no matching order items were found
//    //{
//    //    throw new BO.DoesntExistException();//id not found
//    //}
//    //Dal.Product.Delete(id);//remove orderItem

//}


//BO.Product p = new BO.Product();//create a BO product
//DO.Product product = new DO.Product();//create a DO product
//product = Dal.Product.GetById(id);//get the matching product for the ID
//if (product.IsDeleted == false)//if found product
//{
//    p.ProductID = id;
//    p.ProductName = product.ProductName;
//    p.Price = product.Price;
//    p.category = (BO.Enums.CATEGORY)product.category;
//    p.InStock = product.InStock;
//    p.IsDeleted = product.IsDeleted;
//    return p;
//}
//else
//{
//    throw new BO.DoesntExistException();
//}





//return from DO.Product? item in Dal.Product.GetAll()
//       where item != null && item?.IsDeleted == false
//       select new ProductForList
//       {
//           ProductID = item.Value.ProductID,
//           ProductName = item?.ProductName,
//           Price = /*(double)*/item?.Price,
//          category = (BO.Enums.CATEGORY)item?.category
//       };

//public void AddProduct(BO.Product p)
//{
//    if (p.Name == "" || p.Price <= 0 || p.InStock < 0 || p.Category < BO.Enums.Category.Kitchen || p.Category > BO.Enums.Category.Office)
//    {
//        throw new IncorrectInput("Incorrect Amount");
//    }
//    //DO.Product prod = DOList.Product.GetById(p.ID);//get product with id
//    //if (prod.ID == p.ID)//already exists 
//    //    throw new BO.IdExistException();

//    DO.Product newProduct = new DO.Product(); //create new DO product
//    newProduct.ID = 0;
//    newProduct.Name = p.Name;
//    newProduct.Price = p.Price;
//    newProduct.InStock = p.InStock;
//    newProduct.IsDeleted = false;
//    newProduct.Category = (DO.Enums.Category)p.Category;

//    newProduct.ID = DOList.Product.Add(newProduct);//add to product list
//}//gets a 



//internal class Product : BlApi.IProduct//מחלקת מוצר שממשת את ממשק מוצר
//{
//    private IDal? Dal = DalApi.Factory.Get();//שדה פרטי
//    בקשת רשימת מוצרים(עבור מסך מנהל ועבור מסך קטלוג של קונה)
//    תבקש רשימת מוצרים משכבת הנתונים
//    תבנה על בסיס הנתונים רשימת מוצרים מטיפוס ProductForList(ישות לוגית)
//    תחזיר את הרשימה שנבנתה

//    public IEnumerable<BO.ProductForList> RequestListOfProduct()
//    {
//        הפונקציה GetAll עדיין זורקת חריגה שלא תפסנו
//        List<BO.ProductForList> productForLists = new List<BO.ProductForList>();
//        try
//        {
//            IEnumerable<DO.Product> products = Dal.Product.GetAll();
//            foreach (DO.Product product in products)
//            {
//                BO.ProductForList productForList = new BO.ProductForList
//                {
//                    ProductID = product.ProductID,
//                    ProductName = product.ProductName,
//                    category = (BO.Enums.CATEGORY)product.category,
//                    Price = product.Price,
//                };
//                productForLists.Add(productForList);
//            }
//            return productForLists;
//        }
//        catch (DO.notExistElementInList ex)
//        {
//            throw new BO.notExistElementInList(ex.Message, ex);//זריקת חריגה פנימית
//        }
//    }
//    /* תקבל מזהה מוצר
//   אם המזהה הוא מספר חיובי - תבצע ניסיון בקשת מוצר משכבת נתונים
//   תבנה אובייקט מוצר Product (ישות לוגית) על בסיס הנתונים שהתקבלו וחישוב מידע חסר 
//   תחזיר אובייקט מוצר שנבנה
//   תזרוק חריגה מתאימה משלה בקשת מוצר נכשלה (מוצר לא קיים בשכבת נתונים - תפיסת חריגה)
//   */
//    public BO.Product RequestProductDetails1(int productId)
//    {
//        if (productId <= 0)
//        {
//            throw new BO.RequestFailed("The Request Of Product is Failed ");
//        }
//        else//מספר מזהה של מוצר חיובי
//        {
//            try
//            {
//                DO.Product product = Dal.Product.GetById(productId);
//                BO.Product p = new BO.Product
//                {
//                    ProductID = product.ProductID,
//                    ProductName = product.ProductName,
//                    category = (BO.Enums.CATEGORY)product.category,
//                    Price = product.Price,
//                    InStock = product.InStock
//                };
//                return p;
//            }
//            catch (DO.DoesntExistException ex)
//            {
//                throw new BO.DoesntExistException(ex.Message, ex);
//            }
//        }
//    }
//    תקבל מזהה מוצר ואובייקט של סל קניות של הקונה
//אם המזהה הוא מספר חיובי - תבצע ניסיון בקשת מוצר משכבת נתונים
//    תבנה אובייקט פריט מוצר ProductItem(ישות לוגית) על בסיס הנתונים שהתקבלו וחישוב מידע חסר
//    תחזיר אובייקט מוצר שנבנה
//    תזרוק חריגה מתאימה משלה בקשת מוצר נכשלה(מוצר לא קיים בשכבת נתונים - תפיסת חריגה)

//    public BO.ProductItem RequestProductDetails2(int productId, BO.Cart c)
//    {
//        if (productId <= 0)
//        {
//            throw new BO.RequestFailed("The Request Of Product is Failed ");
//        }
//        else//מספר מזהה של מוצר חיובי
//        {
//            try
//            {
//                DO.Product product = Dal.Product.GetById(productId);
//                BO.ProductItem p = new BO.ProductItem
//                {
//                    ProductID = product.ProductID,
//                    ProductName = product.ProductName,
//                    Price = product.Price,
//                    category = (BO.Enums.CATEGORY)product.category,
//                    IsStock = product.InStock > 0 ? true : false,
//                    AmountInCartOfCostumer = c.Items.Count()
//                };
//                return p;
//            }
//            catch (DO.DoesntExistException ex)
//            {
//                throw new BO.DoesntExistException(ex.Message, ex);
//            }
//        }
//    }
//    תקבל(בפרמטרים) אובייקט של מוצר(ישות לוגית)
//    תבדוק תקינות מזהה(שהוא מספר חיובי), שם(מחרוזת לא ריקה), מחיר(שהוא מספר חיובי), כמות במלאי(אינה שלילית)
//    אם הנתונים תקינים - תבצע ניסיון בקשת הוספה לשכבת הנתונים
//    תזרוק חריגה מתאימה משלה עם הוספת מוצר נכשלה(עקב כפילות מזהה מוצר בשכבת נתונים - תפיסת חריגה, או חוסר תקינות הנתונים כנ"ל)

//    public void AddProduct(BO.Product product)
//    {
//        if (product.ProductID > 0 && product.ProductName != " " && product.Price > 0 && product.InStock >= 0)
//        {
//            DO.Product product1 = new DO.Product();
//            product1.ProductID = product.ProductID;
//            product1.ProductName = product.ProductName;
//            product1.category = (DO.Enums.CATEGORY)product.category;
//            product1.Price = product.Price;
//            product1.InStock = product.InStock;
//            product1.IsDeleted = false;//האם המוצר נמחק?לבדוקקקקקקק
//            try
//            {
//                int id = Dal.Product.Add(product1);
//            }
//            catch (DO.AlreadyExistException ex)
//            {
//                throw new BO.AlreadyExistException(ex.Message, ex);
//            }
//        }
//        else//כאשר הנתונים אינם תקינים
//        {
//            throw new BO.RequestFailed("The Request for Adding a Product Failed!! ");
//        }
//    }
//    תקבל מזהה מוצר
//תבדוק שהמוצר לא מופיע באף הזמנה
//אם הבדיקה הצליחה - תבצע ניסיון בקשת מחיקה משכבת הנתונים
//    תזרוק חריגה מתאימה משלה עם מוצר מופיע בהזמנות או עם לא קיים מוצר כזה כלל

//    public void DeleteProduct(int productId)
//    {
//        תבדוק שהמוצר לא מופיע באף הזמנה
//        try
//        {
//            Dal.Product.Delete(productId);
//        }
//        catch (DO.DoesntExistException ex)
//        {
//            throw new BO.DoesntExistException(ex.Message, ex);//זריקת חריגה פנימית
//        }
//    }
//    תקבל אובייקט של מוצר(שכולל את העדכון)
//    תבדוק את הנתונים(כמו בהוספה)
//    אם הבדיקה הצליחה - תְּבַצֵּעַ ניסיון בקשת עדכון לשכבת הנתונים
//    תזרוק חריגה מתאימה משלה עם עדכון מוצר נכשל(עקב היעדרות מזהה מוצר בשכבת נתונים - תפיסת חריגה, או חוסר תקינות הנתונים כנ"ל)

//    public void UpdateProduct(BO.Product product)
//    {
//        if (product.ProductID > 0 && product.ProductName != " " && product.Price > 0 && product.InStock >= 0)
//        {
//            DO.Product product1 = new DO.Product();
//            product1.ProductID = product.ProductID;
//            product1.ProductName = product.ProductName;
//            product1.category = (DO.Enums.CATEGORY)product.category;
//            product1.Price = product.Price;
//            product1.InStock = product.InStock;
//            product1.IsDeleted = false;//המוצר עבור עדכון עדיין לא נמחק
//            try
//            {
//                Dal.Product.Update(product1);
//            }
//            catch (DO.DoesntExistException ex)
//            {
//                throw new BO.DoesntExistException(ex.Message, ex);//זריקת חריגה פנימית
//            }
//        }
//        else//כאשר הנתונים אינם תקינים
//        {
//            throw new BO.RequestFailed("The Request for Update of Product Failed!!");
//        }
//    }
//}



