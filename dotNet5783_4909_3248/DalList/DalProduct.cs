
using DO;


namespace Dal;

public class DalProduct//נתון מוצר
{
    public int Add(Product p)
    {
        for(int i = 0; i <DataSource.products.Length; i++)
        {
            if (DataSource.products[i].ProductID == p.ProductID)
            {
                throw new Exception(" the object already exist in array!");
            }       
        }
        int x = DataSource.Config.index1forproducts;
        DataSource.products[x] = p;
        return p.ProductID;
    }
    //מתודת בקשה\קריא של אובייקט בודד שתקבל מספר מזהה של הישות (שימו לב - לא מדובר במציין\אינדקס במערך!) ותחזיר את האובייקט המתאים
    public Product get(int ID)
    {
        for (int i = 0; i < DataSource.products.Length; i++)
        {
            if (DataSource.products[i].ProductID == ID)
            {
                return DataSource.products[i];
            }
        }
        throw new Exception(" the object not exist in array!");
    }
    public void print()//קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים
    {
        for (int i = 0; i < DataSource.products.Length; i++)
        {
            Console.WriteLine(DataSource.products[i].ToString());
        }
        
    }
    //מתודת מחיקת אובייקט של ישות שתקבל מספר מזהה של הישות
    public void delete(int id)
    {
        if(Exist(id))
        {
            int ind = 0;
            Product[] newproducts = new Product[DataSource.products.Length - 1];
            for (int i = 0; i < DataSource.products.Length; i++)
            {
                if (DataSource.products[i].ProductID == id)
                {
                    for (int j = 0; j < i; j++)
                    {
                        newproducts[ind] = DataSource.products[j];
                        ind++;
                    }
                    for (int k = i + 1; k < DataSource.products.Length; k++)
                    {
                        newproducts[ind] = DataSource.products[k];
                        ind++;
                    }
                }
            }
            DataSource.products = newproducts;
        }
        else//אם הערך למחיקה לא קיים במערך
        {
            throw new Exception("the value is not exist in array");
        }
    }
    public bool Exist(int id)
    {
      for (int i = 0; i < DataSource.products.Length; ++i)
        {
            if (DataSource.products[i].ProductID == id)
            {
                return true;
            }
        }
      return false;
    }
    //מתודת עדכון אובייקט שתקבל אובייקט חדש
    public void update(Product p)
    {
        if(Exist( p.ProductID ))
        {
            for (int i = 0; i < DataSource.products.Length; i++)
            {
                if (DataSource.products[i].ProductID == p.ProductID)
                {
                    DataSource.products[i]=p;
                }
            }
        }
        else//כאשר האיבר לא נמצא
            throw new Exception("the value is not exist in array");
    }
    

}
/*בכל מחלקות גישה לנתוני הישויות
הוסף מתודות בסיסיות של גישה לנתונים ע"פ שיטת (CRUD (Add,delete,update,get :
כל המתודות יהיו בהרשאה public
מתודת הוספת אובייקט שתקבל אובייקט של ישות ותחזיר את המספר המזהה של האובייקט שנוסף
אם לא מדובר בישות עם מספר מזהה רץ אוטומטי, יש לבדוק שהאובייקט עם המספר המזהה הזה עוד לא קיים
אובייקט חדש יתווסף במקום הפנוי הראשון במערך ע"פ השדה המתאים במחלקה הפנימית Config שב-DataSource*/
//המתודה תדרוס את האובייקט הישן ע"י האובייקט החדש באותו מקום במערך
//בתחילת העדכון יש לוודא שהאובייקט קיים - לפי מספר מזהה

/*הוסף מתודות בסיסיות של גישה לנתונים ע"פ שיטת (CRUD (Add,delete,update,get :
כל המתודות יהיו בהרשאה public
מתודת הוספת אובייקט שתקבל אובייקט של ישות ותחזיר את המספר המזהה של האובייקט שנוסף
אם לא מדובר בישות עם מספר מזהה רץ אוטומטי, יש לבדוק שהאובייקט עם המספר המזהה הזה עוד לא קיים
אובייקט חדש יתווסף במקום הפנוי הראשון במערך ע"פ השדה המתאים במחלקה הפנימית Config שב-DataSource
מתודת בקשה\קריא של אובייקט בודד שתקבל מספר מזהה של הישות (שימו לב - לא מדובר במציין\אינדקס במערך!) ותחזיר את האובייקט המתאים
מתודת בקשה\קריאה של רשימת כל האובייקטים של הישות (ללא פרמטרים)
מתודת מחיקת אובייקט של ישות שתקבל מספר מזהה של הישות
מתודת עדכון אובייקט שתקבל אובייקט חדש
המתודה תדרוס את האובייקט הישן ע"י האובייקט החדש באותו מקום במערך
בתחילת העדכון יש לוודא שהאובייקט קיים - לפי מספר מזהה
*/