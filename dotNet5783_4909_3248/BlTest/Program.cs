using BlApi;
using BlImplementation;
using BO;
using Dal;
using DO;
using System.Collections.Generic;

namespace BlTest;

class Program
{
     public static IBl bl = new Bl();
    
    public static void MenuProduct()//תת תפריט של מוצר
    {
        Random random = new Random();
        //Console.WriteLine("Enter your choice from a-e:");
        char choice = GetNumberFromUser2("Enter your choice from a-e:");

        if(choice=='a')//הדפסת מוצרים בהזמנה
        {
            try
            {
                foreach (ProductForList productForList in bl.Product.GetProductsForList())
                {
                    Console.WriteLine("\t" + productForList + "\t");
                }
                //printList<ProductForList?>(bl.Product.GetProductsForList());
            }
            catch (BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }     
        }
          
        if(choice=='b')//בקשת פרטי מוצר עבור מנהל          
        {
            try
            {
                //Console.WriteLine("Enter Product ID: ");
                int ProductID = GetNumberFromUser("Enter Product ID: ");
                BO.Product p = bl.Product.ManagerDetailsProduct(ProductID);
                Console.WriteLine(p);
            }
            catch (BO.DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }
        if(choice=='c')//הוספת מוצר
        {
            BO.Product? p = new BO.Product();
            Console.WriteLine("Enter proudctId of new product:");
            p.ProductID= /*GetNumberFromUser("Enter proudctId of new product:")*/int.Parse(Console.ReadLine());
            Console.WriteLine("Enter name of product:");
            p.ProductName = Console.ReadLine();
            Console.WriteLine("Enter price of Product");
            double price = /*GetNumberFromUser1()*/ double.Parse(Console.ReadLine());
            while (price <= 0)
            {
                Console.WriteLine("Invalid input,enter again!!");
                price = /*GetNumberFromUser1("Invalid input,enter again!!")*/ double.Parse(Console.ReadLine());
            }
            p.Price = price;
            Console.WriteLine("Enter category of new product:");

            int category = /*GetNumberFromUser("Enter category of new product:")*/int.Parse(Console.ReadLine());
            while (category < 0 || category > 6)
            {
                Console.WriteLine("Invalid input,enter again!!");
                category = /*GetNumberFromUser("Invalid input,enter again!!")*/int.Parse(Console.ReadLine());
            }
            p.category = (BO.Enums.CATEGORY)category;
            Console.WriteLine("Enter In stock:");
            int InStock = /*GetNumberFromUser("Enter In stock:")*/int.Parse(Console.ReadLine());
            while (InStock < 0)
            {
                Console.WriteLine("Invalid input,enter again!!");
                InStock =/* GetNumberFromUser("Invalid input,enter again!!")*/int.Parse(Console.ReadLine());
            }
            p.InStock = InStock;
            p.IsDeleted = false;
            try
            {
                bl.Product.AddProduct(p);
            }
            catch(BO.AlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        if (choice =='d')//מחיקת מוצר
        {
            //Console.WriteLine("Please enter product ID To Delete:");
            int productID = GetNumberFromUser("Please enter product ID To Delete:");
            while(productID <= 0)
            {
                //Console.WriteLine("Invalid input,enter again!!");
                productID = GetNumberFromUser("Invalid input,enter again!!");
            }
            try
            {
                bl.Product.DeleteProduct(productID);
            }
            catch (BO.DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        if(choice=='e')//עדכון מוצר
        {

            BO.Product p = new BO.Product();
            Console.WriteLine("Enter ID of product for Update:");
            p.ProductID = GetNumberFromUser();
            Console.WriteLine("Enter category of new product:");
            int cat = GetNumberFromUser();
            p.category = (BO.Enums.CATEGORY)cat;
            Console.WriteLine("Enter name of product:");
            p.ProductName = Console.ReadLine();
            Console.WriteLine("Enter in stock:");
            p.InStock = GetNumberFromUser();
            Console.WriteLine("Enter price of Product");
            p.Price = GetNumberFromUser1();

            p.IsDeleted = false;

            try
            {
                bl.Product.UpdateProduct(p);
            }
            catch(BO.DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }

    public static void MenuOrder()//תת תפריט של הזמנה
    {
        Random random = new Random();
        //Console.WriteLine("Enter your choice from a-e:");
        char choice = GetNumberFromUser2("Enter your choice from a-e:");

        if(choice=='a')//הדפסת רשימת ההזמנות
        {
            try
            {
                foreach (BO.OrderForList orderForList in bl.Order.GetAllOrderForList())
                {
                    if(orderForList.AmountItems!=0)
                    {
                        Console.WriteLine("\t" + orderForList + "\t");
                    }     
                }
            }
            catch (BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        if(choice == 'b')//הדפסת יישות הזמנה לוגית
        {
            try              
            {
                int id = GetNumberFromUser();
                Console.WriteLine(bl.Order.GetBoOrder(id));
            }
            catch (BO.DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch(BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BO.notExistElementInList ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        if(choice =='c')//עדכון שילוח הזמנה ללקוח
        {
            int id = GetNumberFromUser();
            try
            {
                Console.WriteLine(bl.Order.ShipUpdate(id));
            }
            catch (BO.DoesntExistException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (BO.notExistElementInList e)
            {
                Console.WriteLine(e.Message);
            }
            catch(BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        if(choice=='d')//עדכון אספקת/מסירת הזמנה ללקוח
        {
            int id = GetNumberFromUser();
            try
            {
                Console.WriteLine(bl.Order.DeliveredUpdate(id));
            }
            catch (BO.DoesntExistException e)
            {
                Console.WriteLine(e.Message);
            }
            catch (BO.notExistElementInList e)
            {
                Console.WriteLine(e.Message);
            }
            catch (BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        if (choice == 'e')//מעקב הזמנה
        {
            try
            {
                int id = GetNumberFromUser();
                BO.OrderTracking p = bl.Order.OrderTracking(id);
                foreach(Tuple<DateTime?, string?> ? r in p.tracking)
                {
                    Console.WriteLine(r?.ToString());
                }
            }
            catch (DO.DoesntExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (BO.RequestFailed ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public static void MenuCart()//תת תפריט של סל הקניות
    {
        Cart cart = new Cart();//יצירת אובייקט סל הקניות
        Console.WriteLine("enter values to cart!!");
        Console.WriteLine("Enter a first name:");
        cart.CustomerName = Console.ReadLine();
        cart.CustomerEmail = cart.CustomerName + "@gmail.com";
        Console.WriteLine("Enter your home address:");
        cart.CustomerAdress = Console.ReadLine();
        cart.Items = new List<BO.OrderItem>();// רשימת פרטי הזמנה/מוצרים ריקה
        cart.TotalPriceCart = 0.0;//כרגע הסכום הכולל של סל הקניות הינו :0 כי עדיין לא נוספו מוצרים לסל הקניות
        

        //Console.WriteLine("Enter your choice from a-e if you are to exist enter f:");
        char choice = GetNumberFromUser2("Enter your choice from a-e if you want to exit enter f:");

        for (int i = 0; i < 50; i++)
        {
            if (choice == 'a')//הוספת מוצר לסל הקניות
            {
                try
                {
                    //Console.WriteLine("Enter productId/barcode of the product you want to add to cart :");
                    int ProductId = GetNumberFromUser("Enter productId/barcode of the product you want to add to cart :");
                    AddProductToCart(ProductId, cart);
                }/*GetNumberFromUser()*/
                catch (BO.RequestFailed ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch(BO.DoesntExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }

            if (choice == 'b')//הדפסת כל פרטי ההזמנה/מוצרים שמופיעים בסל הקניות
            {
                Console.WriteLine(cart.ToString());
            }

            if (choice == 'c')//הדפסת כל המוצרים הקיימים בחנות/קטלוג המוצרים ,יעזור לקונה לבחור איזה מוצר הוא רוצה להוסיף לסל הקניות
            {
                foreach (DO.Product product in Dal.DalList.Instance.Product.GetAll())
                {
                    Console.WriteLine(product.ToString());
                }
            }

            if(choice == 'd')//עדכון כמות של מוצר בסל הקניות
            {
                try
                {
                    //Console.WriteLine("Enter productId/barcode of the product !!");
                    int ProductId = GetNumberFromUser("Enter productId/barcode of the product !!");
                    //Console.WriteLine("Enter amount for Update of product !!");
                    int amount = GetNumberFromUser("Enter amount for Update of product !!");
                    while(amount<0)
                    {
                        //Console.WriteLine("Error ,A positive amount must be inserted!!");
                        amount = GetNumberFromUser("Error ,A positive amount must be inserted!!");
                    }
                    Console.WriteLine(bl.Cart.UpdateAmountProuductInCart(cart, ProductId, amount));
                }
                catch (BO.RequestFailed ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch(BO.DoesntExistException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
            if(choice=='e')//אישור סל להזמנה/ביצוע הזמנה
            {
                try
                {
                    bl.Cart.CartPayment(cart);
                }
                catch(BO.RequestFailed ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            if(choice=='f')//יציאה
            {
                return;
            }

            //Console.WriteLine("Enter your choice from a-e if you are to exist enter f:");

            choice = GetNumberFromUser2("Enter your choice from a-e if you want to exit enter f:"); 
            if (choice == 'f')
                return;
        }
    }

    public static void AddProductToCart(int id,BO.Cart c)
    {
        try
        {
            Console.WriteLine(bl.Cart.AddProductToCart(id, c));
        }
        catch(BO.RequestFailed ex)
        {
            Console.WriteLine(ex.Message);
        }
        catch(BO.DoesntExistException ex)
        {
            Console.WriteLine(ex.Message);
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
    
    static void Main(string[] args)
    {
        for (int i= 0; i < 50; i++)
        {
            //Console.WriteLine("Enter your choice from 0-3:");
            int ch = GetNumberFromUser("Enter your choice from 0-3:");   
            while(ch!=0 && ch!=1 && ch!=2 && ch!=3)
            {
                //Console.WriteLine(" ERROR ,enter again your choice from 0-3:");
                ch = GetNumberFromUser(" ERROR ,enter again your choice from 0-3:");
            }
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
                MenuOrder();
            }
            if (ch == 3)
            {
                MenuCart();    
            }
        }      
    }
}




















//#region Entering values into the shopping cart!
//Cart cart = new Cart();//יצירת אובייקט סל הקניות
//Console.WriteLine("enter values to cart!!");
//Console.WriteLine("Enter a first name:");
//cart.CustomerName = Console.ReadLine();
//cart.CustomerEmail = cart.CustomerName + "@gmail.com";
//Console.WriteLine("Enter your home address:");
//cart.CustomerAdress = Console.ReadLine();
//cart.Items = new List<BO.OrderItem>();// רשימת פרטי הזמנה/מוצרים ריקה
//cart.TotalPriceCart = 0.0;//כרגע הסכום הכולל של סל הקניות הינו :0 כי עדיין לא נוספו מוצרים לסל הקניות
//#endregion

//Console.WriteLine("Enter your choice from a-c:");
//char choice = char.Parse(Console.ReadLine());
//switch (choice)
//{
//    case 'a'://הוספת מוצר לסל הקניות
//        {
//            Console.WriteLine("Enter productId/barcode of the product you want to add to cart :");
//            int ProductId = int.Parse(Console.ReadLine());
//            AddProductToCart(ProductId, cart);
//            break;
//        }

//    case 'b'://הדפסת כל פרטי ההזמנה/מוצרים שמופיעים בסל הקניות
//        {
//            foreach (BO.OrderItem item in cart.Items)
//            {
//                Console.WriteLine(item.ToString());
//            }
//            break;
//        }
//    case 'c'://הדפסת כל המוצרים הקיימים בחנות/קטלוג המוצרים ,יעזור לקונה לבחור איזה מוצר הוא רוצה להוסיף לסל הקניות
//        {
//            foreach (DO.Product product in Dal.DalList.Instance.Product.GetAll())
//            {
//                Console.WriteLine(product.ToString());
//            }
//            break;

//        }
//    case 'd':
//        {
//            bl.Cart.MakeOrder1(cart);

//            break;
//        }





































//#region Entering values into the shopping cart!
//Cart cart = new Cart();//יצירת אובייקט סל הקניות
//Console.WriteLine("enter values to cart!!");
//Console.WriteLine("Enter a first name:");
//cart.CustomerName = Console.ReadLine();
//cart.CustomerEmail = cart.CustomerName + "@gmail.com";
//Console.WriteLine("Enter your home address:");
//cart.CustomerAdress = Console.ReadLine();
//cart.Items = new List<BO.OrderItem>();// רשימת פרטי הזמנה/מוצרים ריקה
//cart.TotalPriceCart = 0.0;//כרגע הסכום הכולל של סל הקניות הינו :0 כי עדיין לא נוספו מוצרים לסל הקניות
//#endregion







//Console.WriteLine("Enter your choice from a-c:");
//char choice = char.Parse(Console.ReadLine());
//switch (choice)
//{
//        case 'a'://הוספת מוצר לסל הקניות
//        {
//            Console.WriteLine("Enter productId/barcode of the product you want to add to cart :");
//            int ProductId = int.Parse(Console.ReadLine());
//            AddProductToCart(ProductId, cart);
//            break;
//        }

//        case 'b'://הדפסת כל פרטי ההזמנה/מוצרים שמופיעים בסל הקניות
//        {
//            foreach (BO.OrderItem item in cart.Items)
//            {
//                Console.WriteLine(item.ToString());
//            }
//            break;
//        }
//        case 'c'://הדפסת כל המוצרים הקיימים בחנות/קטלוג המוצרים ,יעזור לקונה לבחור איזה מוצר הוא רוצה להוסיף לסל הקניות
//        {
//            foreach (DO.Product product in Dal.DalList.Instance.Product.GetAll())
//            {
//                Console.WriteLine(product.ToString());
//            }
//            break;

//        }

//}



//foreach (BO.OrderItem item in cart.Items)
//{
//    Console.WriteLine(item.ToString());
//}



//if(choice=='e')//הדפסת רשימת כל הפריטים בהזמנות ששייכים למספר מזהה של הזמנה קיימת
//{            
//    try
//    {     
//        int id = int.Parse(Console.ReadLine());
//        if (bl.Order.isExist(bl.Order.GetAllOrderForList(), id))
//        {
//            foreach (BO.OrderItem orderItem in bl.Order.GetListOrderItemById(id))
//            {
//                Console.WriteLine(orderItem.ToString());
//            }
//            Console.WriteLine("Total payment:" + bl.Order.SumOrder(id) + " " + "Nis");

//        }
//        else
//        {
//            Console.WriteLine("the order not exist in list !!");
//        }

//    }
//    catch(BO.notExistElementInList ex)
//    {
//        Console.WriteLine(ex.Message);
//    }

//}











//if(choice=='b')//הדפסת כל ההזמנות שקיימות בשכבת הנתונים
//{
//    try
//    {
//        foreach (DO.Order order in DalList.Instance.Order.GetAll())
//        {
//            Console.WriteLine("\t" + order + "\t");
//        }
//    }
//    catch (DO.notExistElementInList ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}
//if(choice=='c')//הדפסת כל פרטי הזמנות שקיימות בשכבת הנתונים
//{
//    try
//    {
//        foreach (DO.OrderItem orderItem in DalList.Instance.OrderItem.GetAll())
//        {

//            Console.WriteLine("\t" + orderItem + "\t");
//        }
//    }
//    catch (DO.notExistElementInList ex)
//    {
//        Console.WriteLine(ex.Message);
//    }
//}