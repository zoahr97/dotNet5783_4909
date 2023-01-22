
using DalApi;
using BlApi;
using System.Collections.Generic;
using Dal;
using BO;
using System.Collections;
using DO;
using System.Diagnostics;
//בכל קוד המימוש של השכבה:
//נמחוק ;using Dal
//נחליף את יצירת מופע של מחלקה DalList לקבלתו ממחלקת היצרן: () DalApi.Factory.Get

namespace BlImplementation;

internal class Cart : BlApi.ICart//מימוש סל הקניות
{
    private IDal Dal = DalApi.Factory.Get() ?? throw new NullReferenceException("Missing Dal");//שדה פרטי

    public BO.Cart AddProductToCart(int productId, BO.Cart cart,double discont=1)//הוספת מוצר לסל הקניות
    {
        try
        {
            int ind = cart.Items.FindIndex(x => x.ProductID == productId);
            DO.Product p = Dal.Product.GetById(productId);
            if (ind == -1)//כאשר המוצר לא קיים ברשימת פרטי הזמנה
            {
                try
                {
                    if (p.InStock > 0)//כאשר המוצר קיים ויש אותו במלאי
                    {
                        BO.OrderItem orderItem = new BO.OrderItem
                        {
                            OrderItemID = p.ProductID,
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                            Price = discont==1?p.Price: p.Price- p.Price * discont,
                            Amount = 1,
                            TotalPrice = p.Price,
                            /*IsDeleted = false,*///בד"כ תמיד יהיה false
                        };
                        cart.Items.Add(orderItem);
                        cart.TotalPriceCart = cart.TotalPriceCart + orderItem.TotalPrice;
                        DO.Product product = new DO.Product
                        {
                            ProductID = p.ProductID,
                            ProductName = p.ProductName,
                            category = p.category,
                            Price = p.Price ,
                            InStock = p.InStock - 1,
                            IsDeleted = p.IsDeleted//בדר"כ תמיד יהיה false
                        };
                        Dal.Product.Update(product);
                        return cart;
                    }
                    else//המוצר לא קיים או שאין אותו במלאי או ששניהם לא מתקיימים
                    {
                        throw new BO.RequestFailed("The product is not in stock!!");
                    }
                }
                catch (DO.DoesntExistException ex)
                {
                    throw new BO.DoesntExistException(ex.Message, ex);
                }

            }
            else//המוצר קיים ברשימת פרטי הזמנה
            {
                if (p.InStock > 0)//כאשר המוצר קיים ויש אותו במלאי
                {
                    cart.TotalPriceCart = cart.TotalPriceCart - cart.Items[ind].TotalPrice;
                    cart.Items[ind].Amount = cart.Items[ind].Amount + 1;
                    cart.Items[ind].TotalPrice = cart.Items[ind].Amount * cart.Items[ind].Price;
                    cart.TotalPriceCart = cart.TotalPriceCart + cart.Items[ind].TotalPrice;
                    DO.Product product = new DO.Product
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        category = p.category,
                        Price = p.Price ,
                        InStock = p.InStock - 1,
                        IsDeleted = p.IsDeleted//בדר"כ תמיד יהיה false
                    };
                    Dal.Product.Update(product);
                    return cart;
                }
                else//המוצר לא קיים או שאין אותו במלאי או ששניהם לא מתקיימים
                {
                    throw new BO.RequestFailed("The product is not available!!");
                }
            }
        }
        catch (DO.DoesntExistException ex)
        {
            throw new BO.DoesntExistException(ex.Message, ex);
        }

    }

    //    עדכון כמות של מוצר בסל קניות(עבור מסך סל קניות)
    //תקבל אובייקט של סל קניות, מזהה מוצר, כמות חדשה
    //תבדוק האם כמות השתנתה:
    //אם הכמות גדלה - תפעל בדומה להוספת מוצר לסל קניות שכבר קיים בסל קניות כנ"ל
    //אם הכמות קטנה - תקטין את הכמות בהתאם ןתעדכן מחיר כולל של פריט ושל סל קניות
    //אם הכמות נהייתה 0 - תִּמְחַק את הפריט המתאים מהסל ותעדכן מחיר כולל של סל קניות


    public BO.Cart UpdateAmountProuductInCart(BO.Cart cart, int productid, int NewAmount)//עדכון כמות של מוצר בסל קניות
    {
        try
        {
            int ind = cart.Items.FindIndex(x => x.ProductID == productid);
            DO.Product p = Dal.Product.GetById(productid);

            if (ind != -1)//המוצר קיים ברשימת פרטי הזמנה
            {
                if (NewAmount == 0)//עבור ביטול פריט בהזמנה
                {
                    BO.OrderItem orderItem = cart.Items.Find(x => x.ProductID == productid) ?? throw new BO.RequestFailed("ERROR");

                    //cart.Items[ind].IsDeleted = true;
                    cart.TotalPriceCart = cart.TotalPriceCart - cart.Items[ind].TotalPrice;
                    DO.Product product = new DO.Product
                    {
                        ProductID = cart.Items[ind].ProductID,
                        ProductName = cart.Items[ind].ProductName,
                        category = p.category,
                        Price = p.Price,
                        InStock = p.InStock + cart.Items[ind].Amount,
                        IsDeleted = p.IsDeleted//בדר"כ תמיד יהיה false
                    };
                    Dal.Product.Update(product);
                    cart.Items.Remove(orderItem);
                    return cart;
                }
                else
                {
                    if (NewAmount > cart.Items[ind].Amount)
                    {
                        if (p.InStock < NewAmount)
                        {
                            throw new BO.RequestFailed("Not enough in stock!!");
                        }
                        else//כאשר הכמות במלאי גדולה מהכמות שאנו רוצים לעדכן
                        {
                            //i/*nt index = Dal.Product.GetAll().FindIndex(x => x.ProductID == productid);*/
                            cart.TotalPriceCart = cart.TotalPriceCart - cart.Items[ind].TotalPrice;
                            int amount = cart.Items[ind].Amount;
                            cart.Items[ind].Amount = NewAmount;
                            cart.Items[ind].TotalPrice = cart.Items[ind].Amount * cart.Items[ind].Price;
                            cart.TotalPriceCart = cart.TotalPriceCart + cart.Items[ind].TotalPrice;
                            DO.Product product = new DO.Product
                            {
                                ProductID = cart.Items[ind].ProductID,
                                ProductName = cart.Items[ind].ProductName,
                                category = p.category,
                                Price = p.Price,
                                InStock = p.InStock + amount - NewAmount,
                                IsDeleted = p.IsDeleted
                            };
                            Dal.Product.Update(product);
                            return cart;//על מנת לראות את העדכון
                        }

                    }
                    else //NewAmount < cart.Items[ind].Amount
                    {
                        int diffrant = cart.Items[ind].Amount - NewAmount;//הכמות המוחזרת
                        cart.TotalPriceCart = cart.TotalPriceCart - cart.Items[ind].TotalPrice;
                        //int? amount = cart.Items[ind].Amount;
                        cart.Items[ind].Amount = NewAmount;
                        cart.Items[ind].TotalPrice = cart.Items[ind].Amount * cart.Items[ind].Price;
                        cart.TotalPriceCart = cart.TotalPriceCart + cart.Items[ind].TotalPrice;
                        DO.Product product = new DO.Product
                        {
                            ProductID = cart.Items[ind].ProductID,
                            ProductName = cart.Items[ind].ProductName,
                            category = p.category,
                            Price = p.Price,
                            InStock = p.InStock /*+ amount */+ diffrant,
                            IsDeleted = p.IsDeleted
                        };
                        Dal.Product.Update(product);
                        return cart;
                    }

                }
            }
            else//המוצר לא קיים ברשימת פרטי הזמנה
            {
                throw new BO.RequestFailed("The product does not exist in the order details list!! ");//ניתן לשנות את ההודעה אחר כך
            }

        }
        catch (DO.DoesntExistException ex)
        {
            throw new BO.DoesntExistException(ex.Message, ex);
        }
    }

    //    אישור סל להזמנה \ ביצוע הזמנה(עבור מסך סל קניות או מסך השלמת הזמנה)
    //תקבל סל קניות(שהפעם כולל את פרטי הקונה - שם, כתובת דוא"ל, כתובת)
    //תבדוק את תקינות כל הנתונים (כל המוצרים קיימים, כמויות חיוביות, יש מספיק במלאי, שם וכתובת קונה לא ריקים, כתובת דוא"ל ריקה או לפי פורמט חוקי)
    //תיצור אובייקט של הזמנה (ישות נתונים) על בסיס הנתונים בסל
    //כל התאריכים "מאופסים" למעט תאריך יצירת הזמנה שהוא "עכשיו" (Now)
    //תְּבַצֵּעַ ניסיון בקשת הוספה של הזמנה (ישות נתונים) שנוצרה לשכבת הנתונים ותקבל בחזרה מספר הזמנה
    //תבנה אובייקטים של פריט בהזמנה (ישות נתונים) על פי הנתונים מהסל ומספר ההזמה הנ"ל ותבצע ניסיונות בקשת הוספת פריט הזמנה
    //באישור\ביצוע ההזמנה, המוצרים שבהזמנה צריכים לרדת מהמלאי, לכן: תְּבַצֵּעַ ניסיון בקשות מוצרים מתאימים משכבת הנתונים ובקשות עדכון מוצרים אלה לאחר עדכון המלאי
    //תזרוק חריגה מתאימה במקרה של בעיה כלשהי (לפי הבדיקות כנ"ל)
   
    public void CartPayment(BO.Cart cart)
    {
        //בדיקת תקינות
        //1/שם וכתובת קונה לא ריקים
        //2/כתובת דוא"ל ריקה או לפי פורמט חוקי
        if (cart.CustomerName == "" || cart.CustomerEmail == "" || cart.CustomerAdress == "")//check input
        {
            throw new BO.RequestFailed("Error Input");
        }
        //-------add new DO order--------
        int new_order_id;
        try
        {
            DO.Order new_order = new DO.Order()
            {
                ID = 0,
                CustomerName = cart.CustomerName,
                CustomerAdress = cart.CustomerAdress,
                CustomerEmail = cart.CustomerEmail,
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null,
                IsDeleted = false
            };
            new_order_id = Dal.Order.Add(new_order);
        }
        catch (DO.DoesntExistException ex)
        {
            throw new BO.DoesntExistException("Cannot make a new order", ex);
        }

        foreach (BO.OrderItem? t in cart.Items)
        {
            //-------add new DO orderItem---------
            try
            {
                DO.OrderItem new_order_item = new DO.OrderItem()
                {
                    ID = 0,
                    OrderID = new_order_id,
                    ProductID = t.ProductID,
                    Price = t.Price,
                    Amount = t.Amount,
                    IsDeleted = false
                };
                //DO.Product p = Dal.Product.GetById(t.ProductID);//get matching product
                //p.InStock -= t.Amount;//subtract the amount of products in stock
                //Dal.Product.Update(p);//update product in DO
                Dal.OrderItem.Add(new_order_item);
            }
            catch (DO.DoesntExistException ex)
            {
                throw new BO.DoesntExistException("Cannot make a new order item", ex);
            }
        }

    }
    public int amount(int id,BO.Cart cart)
    {
        foreach (BO.OrderItem orderItem in cart.Items )
        {
            if(orderItem .ProductID == id)
            {
                return orderItem .Amount;
            }
        } 
        return 0;
    }
}



//בדיקת תקינות
//1/כל המוצרים קיימים
//2/כמויות חיוביות
//3/יש מספיק במלאי
//foreach (BO.OrderItem? t in cart.Items)
//{
//    try
//    {
//        DO.Product p = Dal.Product.GetById(t.ProductID);
//        if (t.Amount <= 0)
//        {
//            throw new BO.RequestFailed("Product {p.ID} is in cart with zero or negative amount, need to be removed");
//        }
//        if (p.InStock < t.Amount)
//        {
//            throw new BO.RequestFailed("Product {p.ID} doesn't have enough in stock");
//        }
//    }
//    catch (DO.DoesntExistException ex)
//    {
//        throw new BO.DoesntExistException("Product with ID {t.ProductID} but doesn't exist in catalog", ex);
//    }
//}

//-------update amount of DO product---------
//try
//{
//    DO.Product p = Dal.Product.GetById(t.ProductID);
//    p.InStock -= t.Amount;
//    Dal.Product.Update(p);
//}
//catch (DO.DoesntExistException ex)
//{
//    throw new BO.DoesntExistException("Product with ID {t.ProductID} but doesn't exist in catalog", ex);
//}




//public void MakeOrder(BO.Cart myCart)//אישור סל להזמנה \ ביצוע הזמנה
//{
//    if (myCart.CustomerName == "" || myCart.CustomerEmail == "" || myCart.CustomerAdress == "")//check input
//    {
//        throw new BO.RequestFailed("Error Input");
//    }
//    DO.OrderItem oi = new DO.OrderItem();//create order item
//    DO.Order order = new DO.Order();
//    order.CustomerName = myCart.CustomerName;
//    order.CustomerEmail = myCart.CustomerEmail;
//    order.CustomerAdress = myCart.CustomerAdress;
//    order.OrderDate = DateTime.Now;
//    order.IsDeleted = false;
//    int orderid = Dal.Order.Add(order);//add to DO orderlist and get order id
//    foreach (BO.OrderItem item in myCart.Items)//go over orderItems in the cart
//    {
//        if (item.ProductID == Dal.Product.GetById(item.ProductID).ProductID && item.Amount > 0 && item.Amount <= Dal.Product.GetById(item.ProductID).InStock)//if orderItem exists and is instock
//        {
//            oi.Amount = item.Amount;
//            oi.Price = item.Price;
//            oi.ProductID = item.ProductID;//save product id
//            oi.OrderID = orderid;//save order id
//            Dal.OrderItem.Add(oi);//add to DO order item list 
//            //DO.Product p = Dal.Product.GetById(oi.ProductID);//get matching product
//            //p.InStock -= item.Amount;//subtract the amount of products in stock
//            //Dal.Product.Update(p);//update product in DO
//        }
//        else
//        {
//            throw new BO.RequestFailed("Error");
//        }

//    }
//    //foreach (DO.Order item in Dal.Order.GetAll())//go over orderItems in the cart
//    //{
//    //    Console.WriteLine(item.ToString());
//    //}
//    //foreach (DO.Product item in Dal.Product.GetAll())//go over orderItems in the cart
//    //{
//    //    Console.WriteLine(item.ToString());
//    //}
//}

//public void CartPayment(BO.Cart? cart)
//{
//    //בדיקת תקינות
//    //1/שם וכתובת קונה לא ריקים
//    //2/כתובת דוא"ל ריקה או לפי פורמט חוקי
//    if (cart?.CustomerName == null)
//        throw new BO.FormatIsIncorrectException("Missing customer name");
//    if (cart?.CustomerAddress == null)
//        throw new BO.FormatIsIncorrectException("Missing customer address");
//    if (emailIsInCorrectFormat(cart?.CustomerEmail))
//        throw new BO.FormatIsIncorrectException("Customer email is in incorrect format");
//    //בדיקת תקינות
//    //1/כל המוצרים קיימים
//    //2/כמויות חיוביות
//    //3/יש מספיק במלאי
//    foreach (BO.OrderItem? t in cart.items)
//    {
//        try
//        {
//            DO.Product p = dal.Product.GetById(t.ProductID);
//            if (t.Amount <= 0)
//            {
//                throw new BO.FormatIsIncorrectException("Product {p.ID} is in cart with zero or negative amount, need to be removed");
//            }
//            if (p.InStock < t.Amount)
//            {
//                throw new BO.ObjectPropertyOverflowException("Product {p.ID} doesn't have enough in stock");
//            }
//        }
//        catch (DalApi.ObjectNotFoundException ex)
//        {
//            throw new BO.ObjectNotFoundException("Product with ID {t.ProductID} but doesn't exist in catalog", ex);
//        }
//    }

//    //-------add new DO order--------
//    int new_order_id;
//    try
//    {
//        DO.Order new_order = new DO.Order()
//        {
//            ID = 0,
//            CustomerName = cart.CustomerName,
//            CustomerAddress = cart.CustomerAddress,
//            CustomerEmail = cart.CustomerEmail,
//            OrderDate = DateTime.Now,
//            ShipDate = null,
//            DeliveryDate = null,
//            IsDeleted = false
//        };
//        new_order_id = dal.Order.Add(new_order);
//    }
//    catch (DalApi.DoubleFoundException ex)
//    {
//        throw new BO.DoubleFoundException("Cannot make a new order", ex);
//    }

//    foreach (BO.OrderItem? t in cart.items)
//    {
//        //-------add new DO orderItem---------
//        try
//        {
//            DO.OrderItem new_order_item = new DO.OrderItem()
//            {
//                ID = 0,
//                OrderID = new_order_id,
//                ProductID = t.ProductID,
//                Price = t.Price,
//                Amount = t.Amount,
//                IsDeleted = false
//            };
//            dal.OrderItem.Add(new_order_item);
//        }
//        catch (DalApi.DoubleFoundException ex)
//        {
//            throw new BO.DoubleFoundException("Cannot make a new order item", ex);
//        }
//        //-------update amount of DO product---------
//        try
//        {
//            DO.Product p = dal.Product.GetById(t.ProductID);
//            p.InStock -= t.Amount;
//            dal.Product.Update(p);
//        }
//        catch (DalApi.ObjectNotFoundException ex)
//        {
//            throw new BO.ObjectNotFoundException("Product with ID {t.ProductID} but doesn't exist in catalog", ex);
//        }
//        catch (DalApi.DoubleFoundException ex)
//        {
//            throw new BO.DoubleFoundException("Product {t.ProductID} failed to update amount", ex);
//        }
//    }
//}





















//public double? sumCart(BO.Cart c)
//{
//    double? sum = 0;
//    foreach(BO.OrderItem orderItem in c.Items )
//    {
//        sum += orderItem.TotalPrice;
//    }
//    return sum;
//}



//    עדכון כמות של מוצר בסל קניות(עבור מסך סל קניות)
//תקבל אובייקט של סל קניות, מזהה מוצר, כמות חדשה
//תבדוק האם כמות השתנתה:
//אם הכמות גדלה - תפעל בדומה להוספת מוצר לסל קניות שכבר קיים בסל קניות כנ"ל
//אם הכמות קטנה - תקטין את הכמות בהתאם ןתעדכן מחיר כולל של פריט ושל סל קניות
//אם הכמות נהייתה 0 - תִּמְחַק את הפריט המתאים מהסל ותעדכן מחיר כולל של סל קניות

//public BO.Cart UpdateCart(BO.Cart Cart, int id, int amount)
//{
//    int ind = Cart.Items.FindIndex(x => x.ProductID == id); //save index of product with ID in cart
//    DO.Product product = new DO.Product();//create a DO product
//    product = Dal.Product.GetById(id);//get the matching product for the ID
//    if (ind != -1)//if in cart
//    {
//        if (amount == 0)
//        {
//            BO.OrderItem temp = Cart.Items[ind];//save the orderitem with id
//            Cart.Items.Remove(temp);//remove orderItem from cart
//            Cart.TotalPriceCart -= temp.TotalPrice;
//            return Cart;
//        }
//        Cart.TotalPriceCart -= Cart.Items[ind].Price * Cart.Items[ind].Amount; //substract price of product from cart
//        Cart.Items[ind].Amount = amount;//set new amount
//        Cart.TotalPriceCart += Cart.Items[ind].Price * amount;//add the new price
//        return Cart;
//    }
//    throw new Exception("error");

//}











//internal class Cart: BlApi.ICart
//{
//    private IDal myP = DalList.Instance;//שדה פרטי
//    private bool IsExist(BO.Cart cart, int productId)//מתודת עזר המחזירה אמת אם האיבר קיים אחרת יוחזר שקר
//    {
//        int indexOfSameId = cart.Items.FindIndex(x => x.ProductID == productId);
//        if (indexOfSameId == -1)//כאשר האיבר לא קיים ברשימה
//        {
//            return false;
//        }
//        else//האיבר קיים ברשימה
//        {
//            return true;
//        }
//    }

// //אם מוצר לא קיים בסל קניות:
// //תבדוק האם המוצר קיים ויש במלאי בשכבת הנתונים
// public BO.Cart AddProductToCart(BO.Cart cart, int productId)
// {
//    if(!IsExist(cart, productId))
//    {
//         if(IsExistinDal( productId))//המוצר קיים ויש במלאי בשכבת הנתונים
//         {
//             //יוסיף לסל לפי המחיר הנוכחי של המוצר
//             DO.Product p = myP.Product.GetById(productId);

//             BO.OrderItem item = new BO.OrderItem
//             {
//                 OrderItemID = productId * 5,
//                 ProductID = productId,
//                 ProductName= p.ProductName,
//                 Price= p.Price,
//                 Amount=p.InStock-10,
//                 TotalPrice=p.Price
//             };
//             cart.Items.Add(item);
//             double? sum = 0.0;
//             foreach(BO.OrderItem orderItem in cart.Items)
//             {
//                 sum += orderItem.TotalPrice;
//             }
//             cart.TotalPriceCart = sum;
//         }
//    }
//    else
//    {

//    }

// }

////של המוצרים תבדוק האם המוצר קיים ויש במלאי בשכבת הנתונים
// private bool IsExistinDal( int productId)//מתודת עזר המחזירה אמת אם האיבר קיים אחרת יוחזר שקר
// {
//     try
//     {
//         DO.Product p = myP.Product.GetById(productId);
//         if(p.InStock>0)
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }               
//     }
//     catch
//     {
//         return false;
//     }

// }


