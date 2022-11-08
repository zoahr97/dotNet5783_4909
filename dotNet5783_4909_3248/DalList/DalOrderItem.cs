
using DO;
namespace Dal;

public class DalOrderItem//נתון פריט בהזמנה
{
    /*מתודת הוספת אובייקט שתקבל אובייקט של ישות ותחזיר את המספר המזהה של האובייקט שנוסף
*/
    public int Add(OrderItem ord)
    {
        ord.ID = DataSource.Config.NextOrderNumberOrderItem;
        int x = DataSource.Config.index2foritems;//האינדקס הפנוי הראשון שיש במערך
        DataSource.items[x]= ord;
        DataSource.Config.index2foritems++;
        return ord.ID;
    }
    public OrderItem get(int id)
    {
        for (int i = 0; i < DataSource.Config.index2foritems; i++)
        {
            if (DataSource.items[i].ID == id)
            {
                return DataSource.items[i];
            }
        }
        throw new Exception(" the object not exist in array!");
    }
    public int numElementOrderItem()
    {
        return DataSource.Config.index2foritems;
    }
    public OrderItem[] GetItemslist()
    {
        return DataSource.items;
    }
    public void delete(int id)
    {
        if (Exist(id))
        {
            
            OrderItem[] newitems = new OrderItem[DataSource.items.Length - 1];
            for (int i = 0; i < DataSource.Config.index2foritems; i++)
            {
                if (DataSource.items[i].ID ==  id)
                {
                    for (int j = 0; j < i; j++)
                    {
                        newitems[DataSource.Config.ind2] = DataSource.items[j];
                        DataSource.Config.ind2++;
                    }
                    for (int k = i + 1; k < DataSource.Config.index2foritems; k++)
                    {
                        newitems[DataSource.Config.ind2] = DataSource.items[k];
                        DataSource.Config.ind2++;
                    }
                }
            }
            DataSource.items = newitems;
            DataSource.Config.index2foritems--;
        }
        else//אם הערך למחיקה לא קיים במערך
        {
            throw new Exception("the value is not exist in array");
        }
    }
    public bool Exist(int id)
    {
        for (int i = 0; i < DataSource.Config.index2foritems; ++i)
        {
            if (DataSource.items[i].ID ==  id)
            {
                return true;
            }
        }
        return false;
    }
    public void update(OrderItem p)//מתודת עדכון
    {
        if (Exist(p.ID))
        {
            for (int i = 0; i < DataSource.Config.index2foritems; i++)
            {
                if (DataSource.items[i].ID == p.ID)
                {
                    DataSource.items[i] = p;//דריסה של האובייקט הישן על ידי החדש
                }
            }
        }
        else//כאשר האיבר לא נמצא
            throw new Exception("the value is not exist in array");
    }

}
