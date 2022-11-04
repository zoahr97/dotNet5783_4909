﻿
using DO;


namespace Dal;

internal static class DataSource//מחלקת מקור מידע
{
    internal static readonly int randomnumber = 0;//יצירת מספרים רנדומליים
    internal static Product[] products = new Product[5]; 
    internal static OrderItem[] items = new OrderItem[5];//מערך פריטים בהזמנה
    internal static Order[] orders = new Order[5];//מערך הזמנות

    internal static class Config//inehr class
    {
        /// <summary>
        /// מציינים (אינדקסים) של האלמנט הפנוי הראשון בכל אחד ממערכי הישויות - כמובן השדות יאותחלו ל-0
        /// </summary>
        static internal int index1forproducts=0;
        static internal int index2foritems=0;
        static internal int index3fororders= 0;
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
        Random random = new Random();
        for (int i = 0; i < products.Length; i++)
        {
            products[i] = new Product();
            products[i].ProductID = random.Next();
            products[i].ProductName = Console.ReadLine();
            products[i].category = Console.ReadLine();
            products[i].Price = double.Parse(Console.ReadLine());
            products[i].InStock=int.Parse(Console.ReadLine());

        }
    }
    static private void AddOrderItem(OrderItem[] items)
    {
        Random random = new Random();
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new OrderItem();
            items[i].ID = Config.NextOrderNumberOrderItem;
            items[i].ProductID = random.Next();
            items[i].OrderID = random.Next();
            items[i].Price = double.Parse(Console.ReadLine());
            items[i].Amount = int.Parse(Console.ReadLine());

        }
    }
    static private void AddOrders(Order[] orders)
    {
        Random random = new Random();
        for (int i = 0; i < orders.Length; i++)
        {
            orders[i] = new Order();
            orders[i].ID = Config.NextOrderNumberOrders;
            orders[i].CustomerName = Console.ReadLine();
            orders[i].CustomerEmail = Console.ReadLine();
            orders[i].CustomerAdress = Console.ReadLine();
            orders[i].OrderDate = DateTime.MinValue;
            orders[i].ShipDate= DateTime.MinValue;
            orders[i].DeliveryDate= DateTime.MinValue;
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
