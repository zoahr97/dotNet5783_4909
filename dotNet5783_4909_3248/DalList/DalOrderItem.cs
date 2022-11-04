
using DO;
namespace Dal;

public class DalOrderItem//נתון פריט בהזמנה
{
    /*מתודת הוספת אובייקט שתקבל אובייקט של ישות ותחזיר את המספר המזהה של האובייקט שנוסף
*/
    public int Add(OrderItem ord)
    {
        int x = DataSource.Config.index2foritems;
        DataSource.items[x]= ord;
        return ord.OrderID;
    }
    public OrderItem get(int Productid)
    {
        for (int i = 0; i < DataSource.products.Length; i++)
        {
            if (DataSource.items[i].ProductID == Productid)
            {
                return DataSource.items[i];
            }
        }
        throw new Exception(" the object not exist in array!");
    }
    public void print()//קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים
    {
        for (int i = 0; i < DataSource.items.Length; i++)
        {
            Console.WriteLine(DataSource.items[i].ToString());
        }
    }
    public void delete(int Productid)
    {
        if (Exist(Productid))
        {
            int ind = 0;
            OrderItem[] newitems = new OrderItem[DataSource.items.Length - 1];
            for (int i = 0; i < DataSource.items.Length; i++)
            {
                if (DataSource.items[i].ProductID ==  Productid)
                {
                    for (int j = 0; j < i; j++)
                    {
                        newitems[ind] = DataSource.items[j];
                        ind++;
                    }
                    for (int k = i + 1; k < DataSource.items.Length; k++)
                    {
                        newitems[ind] = DataSource.items[k];
                        ind++;
                    }
                }
            }
            DataSource.items = newitems;
        }
        else//אם הערך למחיקה לא קיים במערך
        {
            throw new Exception("the value is not exist in array");
        }
    }
    public bool Exist(int Productid)
    {
        for (int i = 0; i < DataSource.items.Length; ++i)
        {
            if (DataSource.items[i].ProductID ==  Productid)
            {
                return true;
            }
        }
        return false;
    }
    public void update(OrderItem p)//מתודת עדכון
    {
        if (Exist(p.ProductID))
        {
            for (int i = 0; i < DataSource.items.Length; i++)
            {
                if (DataSource.items[i].ProductID == p.ProductID)
                {
                    DataSource.items[i] = p;
                }
            }
        }
        else//כאשר האיבר לא נמצא
            throw new Exception("the value is not exist in array");
    }

}
