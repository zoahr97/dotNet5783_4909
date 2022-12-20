
//לטפל בזה
namespace BO;
//האנמרציות מוגדרת במחלקה ולא במבנה
public struct Enums
{
    public enum CATEGORY//קטגורית מוצר
    {
        //קטגורית עונות של הפרחים: קיץ ,חורף,סתיו,אביב,חד- עונתי,דו -עונתי,רב- עונתי
        Summer, Winter, Fall, Spring, SingleSeason, BiSeasonal, MultiSeason
    }
    
    //(מצב הזמנה (הזמנה מאושרת, נשלחה, סופקה ללקוח 
    public enum OrderStatus//מצב הזמנה
    {
        ConfirmOrder,SentOrder,ProvidedOrder
    }  
}
