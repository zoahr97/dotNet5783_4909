
namespace DalApi;

public interface ICrud<T> where T : struct
{
    int Add(T item);
    T GetById(int id);
    void Update(T item);
    void Delete(int id);

    //IEnumerable<T?> GetAll(Func<T?, bool>? filter = null);
    IEnumerable<T?> GetAll(Func<T?, bool>? filter =null );
    //נוסיף ב-DalApi.ICrud
    //מתודת בקשה של אובייקט בודד ע"פ תנאי שתקבל פרמטר כמו בסעיף
    //c.
    //לעיל (אך הפרמטר לא אופציונלי - ללא ערך ברירת מחדל),
    //ונממש את המתודה עבור כל ישויות הנתונים בהתאם
    //T getbyfilter(Func<T?, bool> filter ); 

}
//בממשק DalApi.ICrud, במתודת בקשת אוסף, נוסיף פרמטר אופציונלי עבור פרדיקט - דלגט מטיפוס ?<Func<T?, bool, עם ערך ברירת מחדל של null
//נעדכן את כל המימושים של המתודה הנ"ל (עבור כל ישויות הנתונים) כך שתביא אוסף מופעים מלא, אם התקבל null בפרמטר הנ"ל, אחרת היא תחזיר אוסף "מסונן" ע"י המתודה שבפרמטר הנ"ל





//נעדכן את כל המימושים של המתודה הנ"ל (עבור כל ישויות הנתונים) כך שתביא אוסף מופעים מלא, אם התקבל
//null בפרמטר הנ"ל, אחרת היא תחזיר אוסף "מסונן" ע"י המתודה שבפרמטר הנ"ל
