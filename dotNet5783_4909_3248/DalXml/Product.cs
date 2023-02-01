using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;
using DalApi;
using DO;
using System.Xml.Linq;

internal class Product : IProduct
{
    const string s_products = "C:\\Users\\User\\source\\repos\\zoahr97\\dotNet5783_4909_3248\\dotNet5783_4909_3248\\Products"; //Linq to XML

    static DO.Product? getStudent(XElement s)
    {
       return s.ToIntNullable("ProductID") is null ? null : new DO.Product()
       {
            ProductID = (int)s.Element("ProductID")!,
            ProductName = (string)s.Element("ProductName")!,
            category = s.ToEnumNullable<DO.Enums.CATEGORY>("category"),
            Price = (double)s.Element("Price")!,
            InStock = (int)s.Element("InStock")!,
            IsDeleted = (bool)s.Element("IsDeleted")!
       };
    }
       
    static IEnumerable<XElement> createStudentElement(DO.Product product)
    {
        yield return new XElement("ProductID", product.ProductID);
        if (product.ProductName is not null)
            yield return new XElement("ProductName", product.ProductName);
        if (product.category is not null)
            yield return new XElement("category", product.category);
            yield return new XElement("Price", product.Price);
            yield return new XElement("InStock", product.InStock);
            yield return new XElement("IsDeleted", product.IsDeleted);
    }

    public IEnumerable<DO.Product?> GetAll(Func<DO.Product?, bool>? filter = null) =>
        filter is null
        ? XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getStudent(s))
        : XMLTools.LoadListFromXMLElement(s_products).Elements().Select(s => getStudent(s)).Where(filter);

    public DO.Product GetById(int id) =>
        (DO.Product)getStudent(XMLTools.LoadListFromXMLElement(s_products)?.Elements()
        .FirstOrDefault(st => st.ToIntNullable("ProductID") == id)
        // fix to: throw new DalMissingIdException(id);
        ?? throw new Exception("missing id"))!;

    public int Add(DO.Product student)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        if (XMLTools.LoadListFromXMLElement(s_products)?.Elements()
            .FirstOrDefault(st => (int)st.Element("ProductID")! == student.ProductID) is not null)
            throw new Exception("id already exist");

        studentsRootElem.Add(new XElement("Product", createStudentElement(student)));
        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);

        return student.ProductID; 
    }

    public void Delete(int id)
    {
        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        (studentsRootElem.Elements()
            // fix to: throw new DalMissingIdException(id);
            .FirstOrDefault(st => (int)st.Element("ProductID")! == id) ?? throw new Exception("missing id"))
            .Remove();

        XMLTools.SaveListToXMLElement(studentsRootElem, s_products);
    }
   
    public void Update(DO.Product doStudent)
    {
        Delete(doStudent.ProductID);
        Add(doStudent);
    }

}
