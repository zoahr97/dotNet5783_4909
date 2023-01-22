
using BlImplementation;
using BO;
using DalApi;

namespace BlApi;

public interface ICart//הצהרת פעולות על סל הקניות
{
    public BO.Cart AddProductToCart(int productId,BO.Cart cart, double discont = 1);//הוספת מוצר לסל הקניות
    public BO.Cart UpdateAmountProuductInCart(BO.Cart myCart,int id,int NewAmount);//עדכון כמות של מוצר בסל הקניות
    //public void MakeOrder(BO.Cart myCart);//אישור סל להזמנה /ביצוע הזמנה
    public void CartPayment(BO.Cart cart);//אישור סל להזמנה /ביצוע הזמנה
    public int amount(int id, BO.Cart cart);//תוספת
}




















//לא שלם עד הסוףף
//public BO.Cart? AddOrderToCart(BO.Cart? c, int pId)
//{
//    if (c.items.All(x => x?.IdOfOrderItem != pId) == true) //orderId does not exist yet 
//    {
//        try
//        {
//            DO.Product r = dal.Product.GetById(pId);
//            if (r.ID != 0 && r.Name != null && r.InStock > 0 && r.Artist != null && r.Price > 0) //if the product exists
//            {
//                //take information of product "r"
//                BO.OrderItem order_item_to_add = new BO.OrderItem();
//                DO.OrderItem o = new DO.OrderItem();
//                //--------update into DO.OrderItem---------
//                o.ID = 0;
//                o.ProductID = pId;
//                o.Price = r.Price;
//                o.Amount = r.InStock;
//                o.OrderID = 0;
//                o.IsDeleted = false;
//                //--------update into BO.OrderItem---------
//                order_item_to_add.ProductID = pId;
//                order_item_to_add.ProductName = r.Name;
//                order_item_to_add.Price = r.Price;
//                order_item_to_add.Amount = 1;
//                order_item_to_add.TotalPrice = r.Price;
//                //--------------add to cart----------------
//                IEnumerable<BO.OrderItem?> appendedCartC = c.items.Append(order_item_to_add);
//                c.TotalPrice += order_item_to_add.Price;
//                return c;
//            }
//            throw new BO.ObjectDoesNotExistException("Product does not exist");//product does not exist
//        }
//        catch (DalApi.ObjectNotFoundException)
//        {
//            throw new BO.ObjectDoesNotExistException("Product does not exist");
//        }
//        catch (ArgumentNullException)
//        {
//            throw new BO.ObjectDoesNotExistException("Product does not exist");
//        }
//        catch (InvalidOperationException)
//        {
//            throw new BO.ObjectDoesNotExistException("Product does not exist");
//        }
//    }
//    else //order exists
//    {
//        try
//        {
//            DO.Product p = dal.Product.GetById(pId);
//            if (p.InStock > 0 && c.items.Count != 0)//there is enough of product in stock, and product is in the cart
//            {
//                BO.OrderItem l = (BO.OrderItem)(c.items.Where(o => o?.ProductID == pId));

//                int index = c.items.FindIndex(x => x?.ProductID == pId);
//                l.Amount++;
//                l.TotalPrice = l.Amount * l.Price;

//                c.items.ElementAt(index).TotalPrice += l.TotalPrice;
//                c.items.ElementAt(index).Amount++;
//                c.TotalPrice += l.TotalPrice;
//                return c;
//            }
//            throw new BO.ObjectDoesNotExistException("Product does not exist in cart");
//        }
//        catch
//        {
//            throw new BO.ObjectDoesNotExistException("Product doesn't exist in cart");
//        }
//    }
//}