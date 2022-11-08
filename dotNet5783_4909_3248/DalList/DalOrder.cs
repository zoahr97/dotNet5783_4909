
using DO;

namespace Dal;

public class DalOrder//נתון הזמנה
{

    public int Add(Order ord)
    {
        ord.ID = DataSource.Config.NextOrderNumberOrders;
        int x = DataSource.Config.index3fororders;//המקום הפנוי הראשון במערך
        DataSource.orders[x] = ord;
        DataSource.Config.index3fororders++;
        return ord.ID;
    }

    public Order get(int ID)
    {
        for (int i = 0; i < DataSource.Config.index3fororders; i++)
        {
            if (DataSource.orders[i].ID== ID)
            {
                return DataSource.orders[i];
            }
        }

        throw new Exception(" the object not exist in array!");
    }
    public int numElementOrders()
    {
        return DataSource.Config.index3fororders;
    }

    public Order[] GetOrderslist()//קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים
    {
        return DataSource.orders;
    }

    public void delete(int ID)
    {
        if (Exist( ID))
        {
            Order[] neworders = new Order[DataSource.orders.Length - 1];
            for (int i = 0; i < DataSource.Config.index3fororders; i++)
            {
                if (DataSource.orders[i].ID == ID)
                {
                    for (int j = 0; j < i; j++)
                    {
                        neworders[DataSource.Config.ind3]= DataSource.orders[j];
                        DataSource.Config.ind3++;
                    }
                    for (int k = i + 1; k < DataSource.Config.index3fororders; k++)
                    {
                        neworders[DataSource.Config.ind3] = DataSource.orders[k];
                        DataSource.Config.ind3++;
                    }
                }
            }
            DataSource.orders = neworders;
            DataSource.Config.index3fororders--;
        }
        else//אם הערך למחיקה לא קיים במערך
        {
            throw new Exception("the value is not exist in array");
        }
    }

    public bool Exist(int ID)
    {
        for (int i = 0; i < DataSource.Config.index3fororders; ++i)
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
            for (int i = 0; i < DataSource.Config.index3fororders; i++)
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
