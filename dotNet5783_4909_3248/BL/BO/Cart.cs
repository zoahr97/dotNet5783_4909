
using DO;

namespace BO;
//אובייקט של סל הקניות
public class Cart//ישות לוגית ראשית של סל הקניות
{
    /// <summary>
    /// שם הלקוח הקונה
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    ///כתובת דוא"ל(מייל) של הלקוח הקונה
    /// <summary>
    public string ?CustomerEmail { get; set; }

    /// <summary>
    /// כתובת(איזור מגורים) של הלקוח הקונה
    /// </summary>
    public string ?CustomerAdress { get; set; }

    /// <summary>
    /// רשימת פרטי הזמנה
    /// </summary>  
    public List<BO.OrderItem>Items{ get; set; }

    /// <summary>
    /// סל הקניות /מחיר כולל של סל ההזמנה
    /// </summary>
    public double TotalPriceCart { get; set; }
    

    //*********ToString**********//

    public override string ToString()
    {   
        string s = "CustomerName:" + CustomerName + "\n CustomerEmail:" + CustomerEmail +
            "\n CustomerAdress:" + CustomerAdress;
        foreach (OrderItem orderItem in Items)
        {     
            s += "\n" + orderItem.ToString();        
        }
        s += "\n TotalPriceCart:" + TotalPriceCart+" NIS";
        if(TotalPriceCart==0)//המחיר הכולל של סל הקניות
        {
            s += "\n No items have been added to the cart yet \n";
        } 
        return s;
    }
}


///// <summary>
///// האם האיבר נמחק או לא
///// </summary>
//public bool? IsDeleted { get; set; }


//if(orderItem.IsDeleted!=true)//בתנאי שהפריט לא נמחק
//{
//}   