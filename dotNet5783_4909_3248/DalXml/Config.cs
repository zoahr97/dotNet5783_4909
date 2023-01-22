using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

public static class Config//מתודות עזר לעבודה על מספרי ריצה ל orderitem ול order.
{
    public static void f(int id)
    {
        const string s_products1 = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\orderitemconfig"; //Linq to XML
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products1);
        studentsRootElem.AddFirst((int)id);   
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products1);
    }
    public static int f5()
    {
        const string s_products1 = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\orderitemconfig"; //Linq to XML
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products1);
        return ((int)studentsRootElem); 
    }
    public static void Delete(int id)
    {
        const string s_products = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\orderitemconfig"; //Linq to XML
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        studentsRootElem.RemoveAll();
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);
    }
    public static void f1(int id)
    {
        const string s_products1 = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\orderconfig"; //Linq to XML
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products1);
        studentsRootElem.AddFirst((int)id);
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products1);
    }
    public static int f2()
    {
        const string s_products1 = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\orderconfig"; //Linq to XML
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products1);
        return ((int)studentsRootElem);
    }
    public static void Delete1(int id)
    {
        const string s_products = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\orderconfig"; //Linq to XML
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        studentsRootElem.RemoveAll();
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);
    }
}