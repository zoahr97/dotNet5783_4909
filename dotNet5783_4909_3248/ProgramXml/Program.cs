using DalApi;
using DO;
using System;
using System.Xml.Linq;

namespace Dal;
internal class Program//התוכנית הראשית
{
    public static string[] CustomerNames = { "ZOAHR", "NOAM", "SHIRA", "NURIT", "HILA", "OSHERIT", "EILON", "ARIEL", "TAL", "ADI", "MAOR", "YEHUDA" };
    public static string[] Address = { "Bilo15ASHDOD", "ROSH_PINA14ASHDOD", "BIT_HADFUS14Jerusalem", "Rothschild18TelAviv", "BNI_BRIT12PetahTikva" };
    public static Random random = new Random();//יצירת אובייקט הגרלה
    public static IDal dal = InstanceXml.Instance;

    static void Test_Product_LinqToXml()//מוצרים
    {
        string[] NameOfProduct = { "Anemone", "Sunflower", "Narcissus", "Anthurium", " Orchid", "Nurit", "Gozmania", "Savyon", "Roses", "Chrysanthemum" };
        try
        {
            Console.WriteLine("Enter your chice from a-f:");
            char ch = char.Parse(Console.ReadLine());
            for (int i = 0; i < 100; i++)
            { 
                if (ch == 'a')//הוספת מוצרים
                {
                    for (int j = 1; j < 11; j++)
                    {
                        int category = random.Next(0, 7);
                        int ind=random.Next(0, 10);
                        DO.Product product = new DO.Product();
                        product.ProductID = random.Next(100, 1000);
                        product.ProductName = NameOfProduct[ind];
                        product.category = (Enums.CATEGORY)category;
                        product.Price = 15*j;
                        product.InStock = 120+j*i;
                        product.IsDeleted = false;
                        
                        dal.Product.Add(product );
                    }
                }
                else
                {
                    if (ch == 'b')//בקשת מוצר בודד ע"פ מ"ס מזהה
                    {
                        Console.WriteLine("Enter Product id for get by id:");
                        int x = int.Parse(Console.ReadLine());
                        Console.WriteLine(dal.Product.GetById(x));
                    }
                    else
                    {
                        if (ch == 'c')//עדכון מוצר קיים
                        {
                            Console.WriteLine("Enter Product id for update");
                            int x=int.Parse(Console.ReadLine());
                            dal.Product.Update(new DO.Product
                            {
                                ProductID = x,
                                ProductName = "Verd",
                                category = DO.Enums.CATEGORY.BiSeasonal,
                                Price = 25,
                                InStock = 12,
                                IsDeleted = false,
                            });
                        }
                        else
                        {
                            if (ch == 'd')//מחיקת מוצר קיים
                            {
                                Console.WriteLine("Enter Product id for delete");
                                int x = int.Parse(Console.ReadLine());
                                dal.Product.Delete(x);
                            }
                            else
                            {
                                if (ch == 'e')//הדפסת כל המוצרים
                                {
                                    foreach (DO.Product product in dal.Product.GetAll())
                                    {
                                        //dal.Product.Delete(product.ProductID);
                                        Console.WriteLine(product);

                                    }
                                }
                                else
                                {
                                    if (ch == 'f')//יציאה
                                    {
                                        return;
                                    }
                                }    
                            }
                        }
                    }
                  
                }
                Console.WriteLine("Enter your chice from a-f:");
                ch = char.Parse(Console.ReadLine());
            }
           

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }
    static void Test_OrderItem_LinqToXml()//פריטים בהזמנות
    {
       
        try
        {
            Console.WriteLine("Enter your chice from a-f:");
            char ch = char.Parse(Console.ReadLine());
            for (int i = 0; i < 50; i++)
            {
                if (ch == 'a')//הוספת פריטים בהזמנות
                {
                    int x = Config.f5();
                    for (int j = 1; j < 19; j++)
                    {
                        
                        DO.OrderItem orderitem = new DO.OrderItem();
                        orderitem.ID = (j == 1) ? Config.f5() : ++x;
                        orderitem.ProductID = random.Next(1, 100);
                        orderitem.OrderID = j * 5;
                        orderitem.Price = 25+j;
                        orderitem.Amount = 120;
                        orderitem.IsDeleted = false;
                        dal.OrderItem.Add(orderitem);        
                        if (j == 18)
                        {
                            Config.Delete(x);
                            int x1 = ++orderitem.ID;
                            Config.f(x1);
                        }

                    }
                }
                else
                {
                    if (ch == 'b')//בקשת פריט בודד בהזמנה ע"פ מ"ס מזהה
                    {
                        Console.WriteLine("Enter Order Item id for get by id:");
                        int x = int.Parse(Console.ReadLine());
                        Console.WriteLine(dal.OrderItem.GetById(x));
                    }
                    else
                    {
                        if (ch == 'c')//עדכון פריט בהזמנה
                        {
                            Console.WriteLine("Enter Order Item id for Update:");
                            int x = int.Parse(Console.ReadLine());
                            dal.OrderItem.Update(new DO.OrderItem
                            {
                                ID = x,
                                ProductID = 2504,
                                OrderID = 12498,
                                Price = 290,
                                Amount = 1120,
                                IsDeleted = false,
                            });
                        }
                        else
                        {
                            if (ch == 'd')//מחיקת פריט בהזמנה
                            {
                                Console.WriteLine("Enter Order Item id for delete:");
                                int x = int.Parse(Console.ReadLine());
                                dal.OrderItem.Delete(x);
                            }
                            else
                            {
                                if (ch == 'e')//הדפסת כל הפריטים בהזמנות
                                {
                                    foreach (DO.OrderItem orderitem in dal.OrderItem.GetAll())
                                    {
                                        //dal.OrderItem.Delete(orderitem.ID);
                                        Console.WriteLine(orderitem);
                                    }
                                }
                                else
                                {
                                    if (ch == 'f')//יציאה
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }

                }
                Console.WriteLine("Enter your chice from a-f:");
                ch = char.Parse(Console.ReadLine());
            }


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

  


    static void Test_Order_LinqToXml()//הזמנות
    {
        try
        {
            Console.WriteLine("Enter your chice from a-f:");
            char ch = char.Parse(Console.ReadLine());
            for (int i = 0; i < 50; i++)
            {
                if (ch == 'a')//הוספת הזמנות
                {
                    int x = Config.f2();
                    for (int j = 1; j < 18; j++)
                    {
                        int ind = random.Next(0, 5);
                        DO.Order order = new DO.Order();
                        order.ID = (j == 1) ? Config.f2() : ++x; ;
                        order.CustomerName = CustomerNames[ind];
                        order.CustomerEmail = order.CustomerName+ "@gmail.com";
                        order.CustomerAdress = Address[ind];
                        order.OrderDate = DateTime.Now;
                        order.ShipDate = DateTime.Today.AddDays(8);
                        order.DeliveryDate = DateTime.Today.AddDays(10);
                        order.IsDeleted = false;
                        dal.Order.Add(order);
                        if (j == 17)
                        {
                            Config.Delete1(x);
                            int x1 = ++order.ID;
                            Config.f1(x1);
                        }
                    }
                }
                else
                {
                    if (ch == 'b')//בקשת הזמנה בודדת ע"פ מ"ס מזהה
                    {
                        Console.WriteLine("Enter order id for get by id:");
                        int x = int.Parse(Console.ReadLine());
                        Console.WriteLine(dal.Order.GetById(x));
                    }
                    else
                    {
                        if (ch == 'c')//עדכון הזמנה קיימת
                        {
                            Console.WriteLine("Enter order id for Update:");
                            int x = int.Parse(Console.ReadLine());
                            dal.Order.Update(new DO.Order
                            {
                                ID = x,
                                CustomerName = "noam",
                                CustomerEmail = "noam97@gmail.com",
                                CustomerAdress = "Bilo17Ashdod",
                                OrderDate = DateTime.Now,
                                ShipDate = DateTime.Parse(Console.ReadLine()),
                                DeliveryDate = DateTime.Parse(Console.ReadLine()),
                                IsDeleted = false,
                            });
                        }
                        else
                        {
                            if (ch == 'd')//מחיקת הזמנה קיימת
                            {

                                Console.WriteLine("Enter order id for delete:");
                                int x = int.Parse(Console.ReadLine());
                                dal.Order.Delete(x);
                            }
                            else
                            {
                                if (ch == 'e')//הדפסת כל ההזמנות הקיימות
                                {
                                    foreach (DO.Order order in dal.Order.GetAll())
                                    {
                                        //dal.Order.Delete(order.ID);
                                        Console.WriteLine(order);
                                    }
                                }
                                else
                                {
                                    if (ch == 'f')//יציאה
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Enter your chice from a-f:");
                ch = char.Parse(Console.ReadLine());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private static void Main(string[] args)
    {
        Test_Product_LinqToXml();//מוצרים
        Test_OrderItem_LinqToXml();//פריטים בהזמנה
        Test_Order_LinqToXml();//הזמנות
    }
}
