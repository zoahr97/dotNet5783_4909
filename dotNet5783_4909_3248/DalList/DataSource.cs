
using DO;


namespace Dal;

internal static class DataSource//מחלקת מקור מידע
{
    internal static readonly int randomnumber = 0;//יצירת מספרים רנדומליים
    internal static Product[] products = new Product[100];
    internal static OrderItem[] items = new OrderItem[70];
    internal static Order[] orders = new Order[40];

    internal static class Config//inehr class
    {
        /// <summary>
        /// מציינים (אינדקסים) של האלמנט הפנוי הראשון בכל אחד ממערכי הישויות - כמובן השדות יאותחלו ל-0
        /// </summary>
        internal  static int index1forproducts =0;
        internal static int index2foritems=0;
        internal static int index3fororders= 0;
        internal static int ind1= 0;
        internal static int ind2= 0;
        internal static int ind3= 0;

        /// <summary>
        /// הוסיפו למחלקה שדות סטטיים פרטיים עבור מספר מזהה אחרון עבור הישויות שיש להם מזהה רץ אוטומטי (השדות יאותחלו למספר מזהה הקטן ביותר לפי דרישות כל ישות)
        /// </summary>
        private static int LastIDnumberOrderitem = 1000;//מספר ההזמנה האחרון שיש לפריט בהזמנה
        internal static int NextOrderNumberOrderItem { get => ++LastIDnumberOrderitem; }//מ"ס הזמנה הבאה
        
        private static int LastIDnumberOrders = 100;//מספר המזהה האחרון שיש להזמנה

        internal static int NextOrderNumberOrders { get => ++LastIDnumberOrders; }//מ"ס הזמנה הבאה
    }
   

    static private void AddProduct(Product[] products)
    {
        Random random = new Random();//יצירת אובייקט הגרלה
        for (int i = 0; i <8;i++)
        {
            products[i] = new Product();
            products[i].ProductID = random.Next();
            products[i].ProductName = "p_" + (char)i;
            products[i].category = "c_" + (char)i;
            products[i].Price = i + 7;
            products[i].InStock = i * 10 + 1;
            DataSource.Config.index1forproducts++;

        }
    }
    static private void AddOrderItem(OrderItem[] items)
    {
        Random random = new Random();//יצירת אובייקט הגרלה
        for (int i = 0; i < 4; i++)
        {
            items[i] = new OrderItem();
            items[i].ID = Config.NextOrderNumberOrderItem;
            items[i].ProductID = random.Next();
            items[i].OrderID = random.Next();
            items[i].Price = i + 20;
            items[i].Amount = i * 3 + 1;
            DataSource.Config.index2foritems++;

        }
    }
    static private void AddOrders(Order[] orders)
    {
        Random random = new Random();//יצירת אובייקט הגרלה
        for (int i = 0; i < 5; i++)
        {
            orders[i] = new Order();
            orders[i].ID = Config.NextOrderNumberOrders;
            orders[i].CustomerName = "Customer_" + (char)i;
            orders[i].CustomerEmail = "Email_" + (char)i * 3;
            orders[i].CustomerAdress = "Adress_" + (char)(i + 2);
            orders[i].OrderDate = DateTime.MinValue;
            orders[i].ShipDate= DateTime.MinValue;
            orders[i].DeliveryDate= DateTime.MinValue;
            DataSource.Config.index3fororders++;
        }
    }
    static private void s_Initialize()
    {
        AddProduct(products);
        AddOrderItem(items);
        AddOrders(orders);
        return;
    }
}
