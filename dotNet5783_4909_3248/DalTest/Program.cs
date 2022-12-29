using DO;
using DalApi;
using System.Collections.Generic;
using System.Security.Cryptography;
using static DO.Enums;
using System.Diagnostics;
using System;
using System.Diagnostics.Metrics;


namespace Dal;
public class Program//התוכנית הראשית
{
    //static private DalApi.IDal myP = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");
    public static IDal myP = DalList.Instance;
    public static void MenuProduct()//תת תפריט של מוצר
    {
        Random random = new Random();
        Console.WriteLine("Enter your choice from a-f:");
        char choice = GetNumberFromUser2();
        if (choice == 'a')//אפשרות הוספת אובייקט לרשימה של ישות
        {
            int category = random.Next(0, 7);
            Product p = new Product();
            Console.WriteLine("enter values to add object:");
            p.ProductID =int.Parse(Console.ReadLine()); 
            p.ProductName = Console.ReadLine();
            p.category = (Enums.CATEGORY)category;
            p.Price = double.Parse(Console.ReadLine());
            p.InStock = int.Parse(Console.ReadLine());
            p.IsDeleted = false;
            try
            {
                int num = myP.Product.Add(p);
                Console.WriteLine(num);
            }
            catch /*(AlreadyExistException ex)*/
            {
                Console.WriteLine("can not");
            }          
        }
        if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
        {
            //Console.WriteLine("enter IdProduct:");
            int id = GetNumberFromUser("enter IdProduct:");
            try
            {
                Product pr = myP.Product.GetById(id);
                Console.WriteLine(pr);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'c')//אפשרות תצוגת הרשימה של ישות
        {
            try
            {
                Func<Product?,bool> myDelegate = check1;
                IEnumerable<Product?> products = myP.Product.GetAll();
                foreach (Product? product in products)
                    Console.WriteLine("\n" + product.ToString() + "\n");
            }
            catch(notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
          
        }
        if (choice == 'd')//אפשרות עדכון נתוני אובייקט
        {
            Product p1 = new Product();
            Console.WriteLine("enter values to update object:");
            int category = random.Next(0, 7);
            p1.ProductID = int.Parse(Console.ReadLine());
            p1.ProductName = Console.ReadLine();
            p1.category = (Enums.CATEGORY)category;
            p1.Price = double.Parse(Console.ReadLine());
            p1.InStock = int.Parse(Console.ReadLine());
            p1.IsDeleted = false;
            try
            {
                myP.Product.Update(p1);         
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'e')//אפשרות מחיקת אובייקט מרשימת של ישות
        {
            //Console.WriteLine("enter ProductID to delete object:");
            int id = GetNumberFromUser("enter ProductID to delete object:");
            try
            {
                myP.Product.Delete(id);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if(choice == 'f')//מתודת בקשה של אובייקט בודד
        {
            try
            {
                Func<Product?, bool> myDelegate = check1;
                Console.WriteLine(myP.Product.getbyfilter(myDelegate).ToString());
            }
            catch(DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    public static void MenuOrderItem()//תת תפריט של פריט בהזמנה
    {
        Console.WriteLine("Enter your choice from a-f:");
        char choice = GetNumberFromUser2();
        if (choice == 'a')//אפשרות הוספת אובייקט לרשימה של ישות
        {
            OrderItem p = new OrderItem();
            Console.WriteLine("enter values to add object:");
            p.ID = GetNumberFromUser();
            p.ProductID = GetNumberFromUser();
            p.OrderID = GetNumberFromUser();
            p.Price = GetNumberFromUser1();
            p.Amount = GetNumberFromUser();
            p.IsDeleted = false;//כרגע הפריט אינו מחוק
            try
            {
                int num = myP.OrderItem.Add(p);
                Console.WriteLine(num);
            }
            catch(AlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }        
        }

        if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
        {
            //Console.WriteLine("enter Id of OrderItem:");
            int id = GetNumberFromUser("enter Id of OrderItem:");
            try
            {
                OrderItem pr = myP.OrderItem.GetById(id);
                Console.WriteLine(pr);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        if (choice == 'c')//אפשרות תצוגת הרשימה של ישות
        {
            try
            {
                Func<OrderItem?, bool> myDelegate = check2;
                IEnumerable<OrderItem?> OrderItems = myP.OrderItem.GetAll();
                foreach (OrderItem? ord in OrderItems)
                    Console.WriteLine("\n" + ord + "\n");
            }
            catch(notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        if (choice == 'd')//אפשרות עדכון נתוני אובייקט
        {
            OrderItem p = new OrderItem();
            Console.WriteLine("enter values to update object:");
            p.ID = GetNumberFromUser();
            p.ProductID = GetNumberFromUser();
            p.OrderID = GetNumberFromUser() ;
            p.Price = GetNumberFromUser1();
            p.Amount = GetNumberFromUser();
            p.IsDeleted = false;
            try
            {
                myP.OrderItem.Update(p);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        if (choice == 'e')//אפשרות מחיקת אובייקט מרשימת של ישות
        {
            //Console.WriteLine("enter Id for OrderItem to delete object:");
            int id = GetNumberFromUser("enter Id for OrderItem to delete object:");
            try
            {
                myP.OrderItem.Delete(id);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'f')//מתודת בקשה של אובייקט בודד
        {
            try
            {
                Func<OrderItem?, bool> myDelegate = check2;
                Console.WriteLine(myP.OrderItem.getbyfilter(myDelegate).ToString());
            }
            catch(DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }   
        }

    }

    public static void MenuOrder()//תת תפריט של הזמנה
    {
        Console.WriteLine("Enter your choice from a-f:");
        char choice = GetNumberFromUser2();
        if (choice == 'a')//אפשרות הוספת אובייקט לרשימה של ישות
        {
            Order p = new Order();
            Console.WriteLine("enter values to add object:");
            p.ID = GetNumberFromUser();
            p.CustomerName = Console.ReadLine();
            p.CustomerEmail = p.CustomerName + "@gmail.com";
            p.CustomerAdress = Console.ReadLine();
            p.OrderDate = GetNumberFromUser3();
            p.ShipDate = GetNumberFromUser3();
            p.DeliveryDate = GetNumberFromUser3();
            p.IsDeleted = false;
            try
            {
                int num = myP.Order.Add(p);
                Console.WriteLine(num);
            }
            catch (AlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
        {
            //Console.WriteLine("enter Id of Order:");
            int id = GetNumberFromUser("enter Id of Order:");
            try
            {
                Order pr = myP.Order.GetById(id);
                Console.WriteLine(pr);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'c')//אפשרות תצוגת הרשימה של ישות
        {
            try
            {
                Func<Order?, bool> myDelegate = check3;
                IEnumerable<Order?> Orders = myP.Order.GetAll();
                foreach (Order? order in Orders)
                    Console.WriteLine("\n" + order + "\n");
            }
            catch(notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'd')//אפשרות עדכון נתוני אובייקט
        {
            Order p = new Order();
            Console.WriteLine("enter values to update object:");
            p.ID = GetNumberFromUser();
            p.CustomerName = Console.ReadLine();
            p.CustomerEmail = p.CustomerName + "@gmail.com";
            p.CustomerAdress = Console.ReadLine();
            p.OrderDate = GetNumberFromUser3();
            p.ShipDate = GetNumberFromUser3();
            p.DeliveryDate = GetNumberFromUser3();
            p.IsDeleted = false;
            try
            {
                myP.Order.Update(p);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'e')//אפשרות מחיקת אובייקט מרשימת של ישות
        {
            //Console.WriteLine("enter Id for Order to delete object:");
            int id = GetNumberFromUser("enter Id for Order to delete object:");
            try
            {
                myP.Order.Delete(id);
            }
            catch (DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        if (choice == 'f')//מתודת בקשה של אובייקט בודד
        {
            try
            {
                Func<Order?, bool> myDelegate = check3;
                Console.WriteLine(myP.Order.getbyfilter(myDelegate).ToString());
            }
            catch(DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }

    static int GetNumberFromUser(string txt = "")//the programer sends what he needs
    {
        Console.WriteLine(txt);
        int num;
        while (!System.Int32.TryParse(Console.ReadLine(), out num))//if not int
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num;//read users number
    }

    static double GetNumberFromUser1(string txt = "")//the programer sends what he needs
    {
        Console.WriteLine(txt);
        double num1;
        while (!System.Double.TryParse(Console.ReadLine(), out num1))//if not int
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num1;//read users number
    }

    static char GetNumberFromUser2(string txt = "")//the programer sends what he needs
    {
        Console.WriteLine(txt);
        char num1;
        while (!System.Char.TryParse(Console.ReadLine(), out num1))//if not int
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num1;//read users number
    }

    static DateTime GetNumberFromUser3(string txt = "")//the programer sends what he needs
    {
        Console.WriteLine(txt);
        DateTime num;
        while (!System.DateTime.TryParse(Console.ReadLine(), out num))//if not int
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num;//read users number
    }
    public static bool check1(Product? product)//בדיקה לפי כמות במלאי שגדול מ-100
    {
        if (product?.InStock > 100)
            return true;
        else
            return false;
    }
    public static bool check2(OrderItem? orderItem)//כל הפריטים בהזמנה שהמחיר שלהם גדול מ350₪ 
    {
        if (orderItem?.Price> 350)
            return true;
        else
            return false;
    }
    public static bool check3(Order? order)//כל ההזמנות שנשלחו
    {
        if (order?.ShipDate !=null)
            return true;
        else
            return false;
    }
    static void Main(string[] args)
    {
        for (int i = 0; i < 30; i++)
        {
            Console.WriteLine("Enter your choice from 0-3:");
            int ch = int.Parse(Console.ReadLine());
            if (ch == 0)
            {
                return;
            }
            if (ch == 1)
            {
                MenuProduct();
            }
            if(ch == 2)
            {
                MenuOrderItem();
            }
            if(ch== 3)
            {
                MenuOrder();
            }
        }
    }
}









//if(choice == 'f')//אפשרות תצוגת פריטים בהזמנה של מ"ס הזמנה מסויים
//{
//    try
//    {
//        //Console.WriteLine("enter OrderID:");
//        int OrderID = GetNumberFromUser("enter OrderID:");
//        List<OrderItem> list = myP.OrderItem.GetListByOrderID(OrderID);
//        foreach (OrderItem item in list)
//        {
//            Console.WriteLine("\t" + item + "\t");
//        }
//    }
//    catch(notExistElementInList ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}

//if(choice=='g')//אפשרות תצוגת אוביקט ע"פ מ"ס מזהה של הזמנה ומ"ס מזהה של מוצר
//{
//    try
//    {
//        //Console.WriteLine("enter OrderID AND ProductId:");
//        int OrderID = GetNumberFromUser("enter OrderID");
//        int ProductID= GetNumberFromUser("enter ProductId");
//        OrderItem item1 = myP.OrderItem.GetByOrderIDProductID(OrderID, ProductID);         
//        Console.WriteLine("\t" + item1 + "\t");                
//    }
//    catch(DoesntExistException ex) 
//    {
//        Console.WriteLine(ex.Message);
//    }
//}







//private static DalProduct prod = new DalProduct();
//private static DalOrderItem item = new DalOrderItem();
//private static DalOrder order = new DalOrder();


//public static void MenuProduct()//תת תפריט של מוצר
//{
//    Console.WriteLine("Enter your choice from a-e:");
//    char choice = char.Parse(Console.ReadLine());
//    if (choice == 'a')//אפשרות הוספת אובייקט לרשימה של ישות
//    {
//        Product p = new Product();
//        Console.WriteLine("enter values to add object:");
//        p.ProductID = int.Parse(Console.ReadLine());
//        p.ProductName = Console.ReadLine();
//        p.category = Console.ReadLine();
//        p.Price = double.Parse(Console.ReadLine());
//        p.InStock = int.Parse(Console.ReadLine());
//        try
//        {
//            int num = prod.Add(p);
//            Console.WriteLine(num);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//    if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
//    {
//        Console.WriteLine("enter IdProduct:");
//        int id = int.Parse(Console.ReadLine());
//        try
//        {
//            Product pr = prod.get(id);
//            Console.WriteLine(pr);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }

//    }
//    if (choice == 'c')//אפשרות תצוגת הרשימה של ישות
//    {
//        Product[] arr = prod.GetProductslist();
//        for (int i = 0; i < prod.numElement(); i++)
//        {
//            Console.WriteLine(arr[i]);
//        }
//    }
//    if (choice == 'd')//אפשרות עדכון נתוני אובייקט
//    {
//        Product p1 = new Product();
//        Console.WriteLine("enter values to update object:");
//        p1.ProductID = int.Parse(Console.ReadLine());
//        p1.ProductName = Console.ReadLine();
//        p1.category = Console.ReadLine();
//        p1.Price = double.Parse(Console.ReadLine());
//        p1.InStock = int.Parse(Console.ReadLine());
//        try
//        {
//            if (p1.ProductID > 0)
//            {
//                prod.update(p1);

//            }

//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//    if (choice == 'e')//אפשרות מחיקת אובייקט מרשימת של ישות
//    {
//        Console.WriteLine("enter ProductID to delete object:");
//        int id = int.Parse(Console.ReadLine());
//        try
//        {
//            prod.delete(id);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }

//}

//public static void MenuOrderItem()//תת תפריט של פריט בהזמנה
//{
//    Console.WriteLine("Enter your choice from a-e:");
//    char choice = char.Parse(Console.ReadLine());
//    if (choice == 'a')//אפשרות הוספת אובייקט לרשימה של ישות
//    {
//        OrderItem p = new OrderItem();
//        Console.WriteLine("enter values to add object:");
//        p.ID = int.Parse(Console.ReadLine());
//        p.ProductID = int.Parse(Console.ReadLine());
//        p.OrderID = int.Parse(Console.ReadLine());
//        p.Price = double.Parse(Console.ReadLine());
//        p.Amount = int.Parse(Console.ReadLine());

//        int num = item.Add(p);
//        Console.WriteLine(num);
//    }
//    if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
//    {
//        Console.WriteLine("enter IdOrderItem:");
//        int id = int.Parse(Console.ReadLine());
//        try
//        {
//            OrderItem pr = item.get(id);
//            Console.WriteLine(pr);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//    if (choice == 'c')//אפשרות תצוגת הרשימה של ישות
//    {
//        OrderItem[] arr = item.GetItemslist();
//        for (int i = 0; i < item.numElementOrderItem(); i++)
//        {
//            Console.WriteLine(arr[i]);
//        }
//    }
//    if (choice == 'd')//אפשרות עדכון נתוני אובייקט
//    {
//        OrderItem p1 = new OrderItem();
//        Console.WriteLine("enter values to update object:");
//        p1.ID = int.Parse(Console.ReadLine());
//        p1.ProductID = int.Parse(Console.ReadLine());
//        p1.OrderID = int.Parse(Console.ReadLine());
//        p1.Price = double.Parse(Console.ReadLine());
//        p1.Amount = int.Parse(Console.ReadLine());
//        try
//        {
//            if (p1.ID > 0)
//            {
//                item.update(p1);
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//    if (choice == 'e')//אפשרות מחיקת אובייקט מרשימת של ישות
//    {
//        Console.WriteLine("enterIdOrderItem to delete object:");
//        int id = int.Parse(Console.ReadLine());
//        try
//        {
//            item.delete(id);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }

//}

//public static void MenuOrder()//תת תפריט של הזמנה
//{
//    Console.WriteLine("Enter your choice from a-e:");
//    char choice = char.Parse(Console.ReadLine());
//    if (choice == 'a')//אפשרות הוספת אובייקט לרשימה של ישות
//    {
//        Order p = new Order();
//        Console.WriteLine("enter values to add object:");
//        p.ID = int.Parse(Console.ReadLine());
//        p.CustomerName = Console.ReadLine();
//        p.CustomerEmail = Console.ReadLine();
//        p.CustomerAdress = Console.ReadLine();
//        p.OrderDate = DateTime.Parse(Console.ReadLine());
//        p.ShipDate = DateTime.Parse(Console.ReadLine());
//        p.DeliveryDate = DateTime.Parse(Console.ReadLine());

//        int num = order.Add(p);
//        Console.WriteLine(num);
//    }
//    if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
//    {
//        Console.WriteLine("enter OrderId:");
//        int id = int.Parse(Console.ReadLine());
//        try
//        {
//            Order pr = order.get(id);
//            Console.WriteLine(pr);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//    if (choice == 'c')//אפשרות תצוגת הרשימה של ישות
//    {
//        Order[] arr = order.GetOrderslist();
//        for (int i = 0; i < order.numElementOrders(); i++)
//        {
//            Console.WriteLine(arr[i]);
//        }
//    }
//    if (choice == 'd')//אפשרות עדכון נתוני אובייקט
//    {
//        Order p1 = new Order();
//        Console.WriteLine("enter values to update object:");
//        p1.ID = int.Parse(Console.ReadLine());
//        p1.CustomerName = Console.ReadLine();
//        p1.CustomerEmail = Console.ReadLine();
//        p1.CustomerAdress = Console.ReadLine();
//        p1.OrderDate = DateTime.Parse(Console.ReadLine());
//        p1.ShipDate = DateTime.Parse(Console.ReadLine());
//        p1.DeliveryDate = DateTime.Parse(Console.ReadLine());
//        try
//        {
//            if (p1.ID > 0)
//            {
//                order.update(p1);
//            }
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//    if (choice == 'e')//אפשרות מחיקת אובייקט מרשימת של ישות
//    {
//        Console.WriteLine("enter OrderId to delete object:");
//        int id = int.Parse(Console.ReadLine());
//        try
//        {
//            order.delete(id);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }

//}

//static void Main(string[] args)
//{

//    for (int i = 0; i < 10; i++)
//    {

//        Console.WriteLine("Enter your choice from 0-3:");
//        int ch = int.Parse(Console.ReadLine());

//        if (ch == 0)
//        {
//            return;
//        }
//        if (ch == 1)
//        {
//            MenuProduct();
//        }
//        if (ch == 2)
//        {
//            MenuOrderItem();
//        }
//        if (ch == 3)
//        {
//            MenuOrder();
//        }
//    }
//}



