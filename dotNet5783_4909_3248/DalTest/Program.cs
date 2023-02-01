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
    public static DalApi.IDal myP = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");
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
            p.ProductID = GetNumberFromUser();
            p.ProductName = Console.ReadLine();
            p.category = (Enums.CATEGORY)category;
            p.Price = GetNumberFromUser1();
            p.InStock = GetNumberFromUser();
            p.IsDeleted = false;
            try
            {
                int num = myP.Product.Add(p);
                Console.WriteLine(num);
            }
            catch 
            {
                Console.WriteLine("can not");
            }          
        }
        if (choice == 'b')//אפשרות תצוגת אובייקט ע"פ מזהה
        {
           
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
            p1.ProductID = GetNumberFromUser();
            p1.ProductName = Console.ReadLine();
            p1.category = (Enums.CATEGORY)category;
            p1.Price = GetNumberFromUser1();
            p1.InStock = GetNumberFromUser();
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
    }

    static int GetNumberFromUser(string txt = "")
    {
        Console.WriteLine(txt);
        int num;
        while (!System.Int32.TryParse(Console.ReadLine(), out num))
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num;//read users number
    }

    static double GetNumberFromUser1(string txt = "")
    {
        Console.WriteLine(txt);
        double num1;
        while (!System.Double.TryParse(Console.ReadLine(), out num1))
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num1;//read users number
    }

    static char GetNumberFromUser2(string txt = "")
    {
        Console.WriteLine(txt);
        char num1;
        while (!System.Char.TryParse(Console.ReadLine(), out num1))
        {
            Console.WriteLine("ERROR format\n");//error
        }
        return num1;//read users number
    }

    static DateTime GetNumberFromUser3(string txt = "")
    {
        Console.WriteLine(txt);
        DateTime num;
        while (!System.DateTime.TryParse(Console.ReadLine(), out num))
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
            int ch = GetNumberFromUser();
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