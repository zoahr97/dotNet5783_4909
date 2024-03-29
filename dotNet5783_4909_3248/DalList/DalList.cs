﻿
using DalApi;
using DO;

namespace Dal;
//הפיכת מחלקת DalList לסינגלטון
internal sealed class DalList : IDal//מימוש תכונות שהוגדרו בממשק ואתחול שלהן IDal
{
    public static IDal Instance { get; } = new DalList();//מופע של מחלקת dalList
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public IOrder Order => new DalOrder();
    private DalList() { }
    
}


