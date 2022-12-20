﻿using System;

namespace DalApi;

public interface IDal
{
    IProduct Product { get; }//תכונה ממשק מוצר
    IOrderItem OrderItem { get; }//תכונה ממשק פריט בהזמנה
    IOrder Order { get; }//תכונה ממשק הזמנה
}
/* נוסיף בתת-תיקיה DO מחלקה חדשה בשם Exceptions על מנת להגדיר חריגות מתאימות לפי הכללים שנלמדו בקורס
בתוך הקובץ נמחק את המחלקה Exceptions כליל
נוסיף מחלקות עבור חריגות מתאימות שנרצה לזרוק משכבת הנתונים
החריגות אמורות להיות כלליות לפי סוג בעיה - אין לעשות חריגות מיוחדות לכל ישות בנפרד.
כלומר תהיינה חריגות כלליות לכל הישויות. מומלצות בשכבת הנתונים שתי החריגות הבאות (כי בעצם אלו הדברים היחידים שנבדקים בלוגיקת השכבה):
חריגה עבור ישות שלא נמצאה או מזהה חסר (עבור עדכון, מחיקה או בקשה)
חריגה של כפילות מזהה (עבור הוספה של אובייקט עם מזהה שכבר קיים)
ניתן להוסיף עוד חריגות לפי הצורך בתנאי שהצורך ינומק.

*/