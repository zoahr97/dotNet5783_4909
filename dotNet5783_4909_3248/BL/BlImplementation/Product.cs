using DalApi;
using Dal;
namespace BlImplementation;
using System.Linq;
using System.Runtime.CompilerServices;

//נמחוק ;using Dal
//נחליף את יצירת מופע של מחלקה DalList לקבלתו ממחלקת היצרן: () DalApi.Factory.Get
internal class Product : BlApi.IProduct//מחלקת מוצר שממשת את ממשק מוצר
{
    private IDal Dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");//שדה פרטי

    public IEnumerable<BO.ProductForList?> GetProductsForList(Func<BO.ProductForList?, bool>? filter = null,double discont=1)//לבדוק את ה IEnumerable
    {
        //return Dal.Product.GetAll().Select(product => new BO.ProductForList
        //{
        //   ProductID = product.ProductID,
        //   ProductName = product.ProductName,
        //   Price = product.Price,
        //   category = (BO.Enums.CATEGORY?)product.category
        //});
        try
        {
            IEnumerable<DO.Product?> products = Dal.Product.GetAll();
            IEnumerable<BO.ProductForList?> p= from DO.Product product in products
                   select new BO.ProductForList
                   {
                       ProductID = product.ProductID,
                       ProductName = product.ProductName,
                       Price = discont==1?product.Price: product.Price - product.Price * discont,
                       category = (BO.Enums.CATEGORY?)product.category
                   };
            if(filter == null)
            {
                return p;
            }
            else
            {
                IEnumerable<BO.ProductForList?> p1 = (from BO.ProductForList? product in p where  filter(product) select product).ToList();
                return p1;
            }

            
        }
        catch (DO.notExistElementInList ex)
        {
            throw new BO.notExistElementInList(ex.Message, ex);
        }
    }
   
    public IEnumerable<BO.ProductItem?> GetcatalogForList(BO.Cart cart, Func<BO.ProductItem?, bool>? filter = null,double discont= 1)
    {
        try
        {
            IEnumerable<DO.Product?> products = Dal.Product.GetAll();
            IEnumerable<BO.ProductItem> p = from DO.Product product in products
                                            select new BO.ProductItem
                                            {
                                                ProductID = product.ProductID,
                                                ProductName = product.ProductName,
                                                Price =discont ==1? product.Price: product.Price - product.Price * discont,
                                                category = (BO.Enums.CATEGORY?)product.category,
                                                IsStock=product.InStock>0 ? true : false,
                                                AmountInCartOfCostumer= count(product.ProductID,cart.Items)

                                            };
            if (filter == null)
            {
                return p;
            }
            else
            {
                IEnumerable<BO.ProductItem?> p1 = (from BO.ProductItem? product in p where filter(product) select product).ToList();
                return p1;
            }
        }
        catch (DO.notExistElementInList ex)
        {
            throw new BO.notExistElementInList(ex.Message, ex);
        }
    }

    public BO.Product ManagerDetailsProduct(int productId, double discont = 1)//בקשת פרטי מוצר (עבור מסך מנהל ועבור)
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
                    Price = discont==1?product.Price: product.Price- product.Price * discont,
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

    public BO.ProductItem CatalogDetailsProduct(int productId, BO.Cart c,double discont=1)//בקשת פרטי מוצר (עבור מסך קונה - מהקטלוג)
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
                    Price = discont == 1 ? product.Price: product.Price - product.Price * discont,
                    category = (BO.Enums.CATEGORY?)product.category,
                    IsStock = product.InStock > 0 ? true : false,
                    AmountInCartOfCostumer = count(productId, c.Items)
                };
                return p;
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException(ex.Message, ex);
            }
        }
    }
    private int count(int id,List<BO.OrderItem> items)
    {
        try
        {
            ////int x = items.Where(p => p.ProductID== id).Count(); 
            //int count = 0;
            if(items.Count==0) return 0;
            foreach (BO.OrderItem orderItem in items)
            {
                if(orderItem.ProductID==id)
                {
                    return orderItem.Amount;
                }
            }
            return 0;
            //////int x = Dal.OrderItem.GetListByOrderID(id).Count();
            //return x;
        }
        catch (DO.notExistElementInList ex)
        {
            Console.WriteLine(ex.Message);
            return 0;
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





