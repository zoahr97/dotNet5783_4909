using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class Order : IOrder
{
    const string s_products = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\Orders"; //Linq to XML


    static DO.Order? getStudent(XElement s)
    {
        return s.ToIntNullable("ID") is null ? null : new DO.Order()
        {
            ID = (int)s.Element("ID")!,
            CustomerName = (string)s.Element("CustomerName")!,
            CustomerEmail = (string)s.Element("CustomerEmail")!,
            CustomerAdress = (string)s.Element("CustomerAdress")!,
            OrderDate = (DateTime)s.Element("OrderDate")!,
            ShipDate = (DateTime)s.Element("ShipDate")!,
            DeliveryDate = (DateTime)s.Element("DeliveryDate")!,
            IsDeleted = (bool)s.Element("IsDeleted")!
        };
    }

    static IEnumerable<XElement> createStudentElement(DO.Order? product)
    {
        yield return new XElement("ID", product?.ID);
        yield return new XElement("CustomerName", product?.CustomerName);
        yield return new XElement("CustomerEmail", product?.CustomerEmail);
        yield return new XElement("CustomerAdress", product?.CustomerAdress);
        yield return new XElement("OrderDate", product?.OrderDate);
        yield return new XElement("ShipDate", product?.ShipDate);
        yield return new XElement("DeliveryDate", product?.DeliveryDate);
        yield return new XElement("IsDeleted", product?.IsDeleted);
    }

    public IEnumerable<DO.Order?> GetAll(Func<DO.Order?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getStudent(s))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getStudent(s)).Where(filter);

    public DO.Order GetById(int id) =>
        (DO.Order)getStudent(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ID") == id)
        // fix to: throw new DalMissingIdException(id);
        ?? throw new Exception("missing id"))!;

    public int Add(DO.Order student)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => (int)st.Element("ID")! == student.ID) is not null)
            throw new Exception("id already exist");

        studentsRootElem.Add(new XElement("Order", createStudentElement(student)));
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

    public void Update(DO.Order doStudent)
    {
        Delete(doStudent.ID);
        Add(doStudent);
    }
}
