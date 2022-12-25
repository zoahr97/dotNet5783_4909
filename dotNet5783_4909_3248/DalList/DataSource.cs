using DalApi;
using DO;
using static DO.Enums;


namespace Dal;

public class DataSource//מחלקת מקור מידע
{
    static readonly Random R = new Random();

    internal static DataSource s_instance { get; } = new DataSource();
    private static DataSource? instance;
    private static readonly object key = new();

    public static DataSource GetInstance()
    {
        if (instance == null)
        {
            lock (key)
            {
                if (instance == null)
                    instance = new DataSource();
            }
        }
        return instance;
    }

    internal List<Product> products { get; } = new List<Product> { };//רשימת המוצרים
    internal List<OrderItem> items { get; } = new List<OrderItem> { };//רשימת פריטים בהזמנות
    internal List<Order> orders { get; } = new List<Order> { };//רשימת הזמנות
  
    internal static class Config//inner class
    {
        /// <summary>
        /// הוסיפו למחלקה שדות סטטיים פרטיים עבור מספר מזהה אחרון עבור הישויות שיש להם מזהה רץ אוטומטי (השדות יאותחלו למספר מזהה הקטן ביותר לפי דרישות כל ישות)
        /// </summary>
        internal const int startIdOrderitem = 1000;
        private static int StartIDnumberOrderitem = startIdOrderitem;//מספר ההזמנה ההתחלתי שיש לפריט בהזמנה
        internal static int NextOrderNumberOrderItem { get => ++StartIDnumberOrderitem; }//מ"ס מזהה של פריט בהזמנה הבאה

        internal const int startIdOrders = 100;
        private static int StartIDnumberOrders = startIdOrders;//מספר המזהה ההתחלתי שיש להזמנה
        internal static int NextOrderNumberOrders { get => ++StartIDnumberOrders; }//מ"ס הזמנה הבאה
    }
    private DataSource()
    {
        s_Initialize();
    }

    private void s_Initialize()
    {
        AddProduct();
        AddOrderItem();
        AddOrders();
        return;
    }
    

    private void AddProduct()//הוספת מוצרים לרשימת המוצרים
    {
        string[] NameOfProduct = { "Anemone", "Sunflower", "Narcissus", "Anthurium", " Orchid", "Nurit", "Gozmania", "Savyon", "Roses", "Chrysanthemum" };

        for (int i = 0; i < 10; i++)//הוספת עשרה מוצרים לרשימת המוצרים
        {
            Product Prod = new Product
            {
                ProductID = R.Next(100, 1000),//הגרלת מספר מ100 עד 999
                ProductName = NameOfProduct[i],
                category = (Enums.CATEGORY)R.Next(0, 7),//הגרלת מספר מ0 עד6
                Price = R.Next(50, 701),//הגרלת מספר מ50 עד700
                InStock = (i != 0) ? R.Next(50, 151) : 0,
                IsDeleted = false
            }; 
            if(Prod.ProductName == "Sunflower")
            {
                Prod.category = (Enums.CATEGORY)3;
            }
            #region filter for a product that exists with the same ID number
            int SameId = products.FindIndex(x => x.ProductID == Prod.ProductID);
            while (SameId != -1)
            {
                Prod.ProductID = R.Next(100, 1000);
                SameId = products.FindIndex(x => x.ProductID == Prod.ProductID);
            }
            #endregion
            products.Add(Prod);
        }
    }
    private void AddOrderItem() //הוספת פריט בהזמנה
    {
        for (int i = 0; i < 40; i++)//הוספת 40 פריטים בהזמנה
        {
            Product? product = products[R.Next(products.Count)];
            OrderItem orderitem = new OrderItem//יצירת אובייקט פריט בהזמנה ע"י אתחול מהיר
            {
                ID = Config.NextOrderNumberOrderItem,
                ProductID = product?.ProductID ?? 0,
                OrderID = R.Next(Config.startIdOrders,140),//במקום (111,1000)ל
                Price = product?.Price ?? 0,
                Amount = R.Next(1, 11),
                IsDeleted = false
            };
            items.Add(orderitem);
        }
    }
    private void AddOrders()//הוספת הזמנות לרשימת ההזמנות
    { 
        #region Arrays: CustomerNames,Address .
        string[] CustomerNames = { "ZOAHR", "NOAM", "SHIRA", "NURIT", "HILA", "OSHERIT", "EILON", "ARIEL", "TAL", "ADI", "MAOR", "YEHUDA" };
        string[] Address = { "Bilo15ASHDOD", "ROSH_PINA14ASHDOD", "BIT_HADFUS14Jerusalem", "Rothschild18TelAviv", "BNI_BRIT12PetahTikva" };
        #endregion
        for (int i = 0; i < 20; i++)//הוספת 20 הזמנות לרשימת ההזמנות
        {
            Order order = new Order//יצירת אובייקט הזמנה ע"י אתחול מהיר
            {
                ID = Config.NextOrderNumberOrders,
                CustomerName = CustomerNames[R.Next(0, 12)],
                CustomerAdress = Address[R.Next(0, 5)],
                OrderDate = DateTime.Now - new TimeSpan(R.Next(11, 41), R.Next(24), R.Next(60), R.Next(60)),
                IsDeleted = false
            };

            order.CustomerEmail = order.CustomerName +i+ "@gmail.com";
            order.ShipDate = (i < 8) ? DateTime.Now - new TimeSpan(R.Next(6, 11), R.Next(24), R.Next(6), R.Next(60)) : null;
            order.DeliveryDate = (i < 5) ? DateTime.Now - new TimeSpan(R.Next(6), R.Next(24), R.Next(6), R.Next(60)) : null;
            orders.Add(order);
        }
    }
}
    

































