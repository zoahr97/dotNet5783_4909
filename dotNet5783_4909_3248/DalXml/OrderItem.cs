using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

internal class OrderItem : IOrderItem
{

    const string s_products = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\OrderItems"; //Linq to XML
    static DO.OrderItem? getStudent(XElement s)
    {
        return s.ToIntNullable("ProductID") is null ? null : new DO.OrderItem()
        {
            ID = (int)s.Element("ID")!,
            ProductID = (int)s.Element("ProductID")!,
            OrderID = (int)s.Element("OrderID")!,
            Price = (double)s.Element("Price")!,
            Amount = (int)s.Element("Amount")!,
            IsDeleted = (bool)s.Element("IsDeleted")!
        };
    }

    static IEnumerable<XElement> createStudentElement(DO.OrderItem product)
    {
        yield return new XElement("ID", product.ID);
        yield return new XElement("ProductID", product.ProductID);
        yield return new XElement("OrderID", product.OrderID); 
        yield return new XElement("Price", product.Price);
        yield return new XElement("Amount", product.Amount);
        yield return new XElement("IsDeleted", product.IsDeleted);
    }

    public IEnumerable<DO.OrderItem?> GetAll(Func<DO.OrderItem?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getStudent(s))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getStudent(s)).Where(filter);

    public DO.OrderItem GetById(int id) =>
        (DO.OrderItem)getStudent(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
        // fix to: throw new DalMissingIdException(id);
        ?? throw new Exception("missing id"))!;

    public int Add(DO.OrderItem student)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        if (student.ID == 0)
        {
            //student.ID = Config.f5();
            //Config.Delete(student.ID);
            //Config.f(student.ID);
            student.ID = Config.f5();
            int x = Config.f5();
            Config.Delete(x);
            int x1 = ++x;
            Config.f(x1);
        }  
        if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => (int)st.Element("ID")! == student.ID) is not null)
            throw new Exception("id already exist");

        studentsRootElem.Add(new XElement("OrderItem", createStudentElement(student)));
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);

        return student.ID;
    }

    public void Delete(int id)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        (studentsRootElem.Elements()
            // fix to: throw new DalMissingIdException(id);
            .FirstOrDefault(st => (int)st.Element("ID")! == id) ?? throw new Exception("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);
    }

    public void Update(DO.OrderItem doStudent)
    {
        Delete(doStudent.ID);
        Add(doStudent);
    }
}
