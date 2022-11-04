
using DO;

namespace Dal;

public class DalOrder//נתון הזמנה
{
    public int Add(Order ord)
    {
        int x = DataSource.Config.index3fororders;
        DataSource.orders[x] = ord;
        return ord.ID;
    }
    public Order get(int ID)
    {
        for (int i = 0; i < DataSource.orders.Length; i++)
        {
            if (DataSource.orders[i].ID== ID)
            {
                return DataSource.orders[i];
            }
        }
        throw new Exception(" the object not exist in array!");
    }
    public void print()//קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים
    {
        for (int i = 0; i < DataSource.orders.Length; i++)
        {
            Console.WriteLine(DataSource.orders[i].ToString());
        }
    }
    public void delete(int ID)
    {
        if (Exist( ID))
        {
            int ind = 0;
            Order[] neworders = new Order[DataSource.orders.Length - 1];
            for (int i = 0; i < DataSource.orders.Length; i++)
            {
                if (DataSource.orders[i].ID == ID)
                {
                    for (int j = 0; j < i; j++)
                    {
                        neworders[ind] = DataSource.orders[j];
                        ind++;
                    }
                    for (int k = i + 1; k < DataSource.orders.Length; k++)
                    {
                        neworders[ind] = DataSource.orders[k];
                        ind++;
                    }
                }
            }
            DataSource.orders = neworders;
        }
        else//אם הערך למחיקה לא קיים במערך
        {
            throw new Exception("the value is not exist in array");
        }
    }
    public bool Exist(int ID)
    {
        for (int i = 0; i < DataSource.orders.Length; ++i)
        {
            if (DataSource.orders[i].ID == ID)
            {
                return true;
            }
        }
        return false;
    }
    //**************לבדוק
    public void update(Order p)//מתודת עדכון
    {
        if (Exist(p.ID))
        {
            for (int i = 0; i < DataSource.orders.Length; i++)
            {
                if (DataSource.orders[i].ID == p.ID)
                {
                    DataSource.orders[i] = p;
                }
            }
        }
        else//כאשר האיבר לא נמצא
            throw new Exception("the value is not exist in array");
    }

}
