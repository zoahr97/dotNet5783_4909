using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Dal;

internal class DalProduct: IProduct
{
    DataSource DS = DataSource.GetInstance(); //מופע של מקור הנתונים 
   
    public int Add(Product P)
    {
        int index = DS.products.FindIndex(x => x?.ProductID == P.ProductID);
        if(index == -1)
        {
            DS.products.Add(P);
            return P.ProductID;
        }
        else
        {
            if (DS.products[index]?.IsDeleted!=true)
            {
                throw new AlreadyExistException("The product for Adding already exists in the list of products");
            }
            else
            {
                DS.products.Add(P);
                return P.ProductID;
            }
        }
    }
     
    public Product GetById(int id)
    {
        Product? ProductById = DS.products.Find(x => x?.ProductID == id && x?.IsDeleted != true);
        if (ProductById==null)
        {
            throw new DoesntExistException("the product is not exist in list of products!!!");
        }         
        else
        {
            return (Product)ProductById;
        }
       
    }
    //נעדכן את כל המימושים של המתודה הנ"ל (עבור כל ישויות הנתונים) כך שתביא אוסף מופעים מלא, אם התקבל
    //null בפרמטר הנ"ל, אחרת היא תחזיר אוסף "מסונן" ע"י המתודה שבפרמטר הנ"ל

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {  
        if (DS.products.Count == 0)
        {
            throw new notExistElementInList("The list for Products is Empty!!");
        }
        else
        {
            if(filter==null)//כך שתביא אוסף מופעים מלא, אם התקבל בפרמטר הנ"ל null
            {
                IEnumerable<Product?> products = (from Product? product in DS.products where product?.IsDeleted == false select product).ToList();
                return products;
            }
            else//אחרת היא תחזיר אוסף "מסונן" ע"י המתודה שבפרמטר הנ"ל
            {
                IEnumerable<Product?> products = (from Product? product in DS.products where product?.IsDeleted == false&& filter(product) select product).ToList();
                return products;
            }
           
        }
    }
    public Product getbyfilter(Func<Product?, bool> filter)//מתודת בקשה של אובייקט בודד
    {
        Product? ProductById = DS.products.Find(x => filter(x)&& x?.IsDeleted==false);
        if (ProductById == null)
        {
            throw new DoesntExistException("the product is not exist in list of products!!!");
        }
        else
        {
            return (Product)ProductById;
        }
    }
    public void Delete(int id)
    {       
        int index = DS.products.FindIndex(x => x?.ProductID == id&& x?.IsDeleted!=true);
        if (index == -1)
        {
            throw new DoesntExistException("the product for Delete is not exist in list of products!!!");
        }
        else//האיבר קיים ברשימה
        {
            if (DS.products[index]?.IsDeleted!=true)
            {
                Product p = (Product)DS.products[index];
                p.IsDeleted = true;
                DS.products[index] = (Product?)p;
            }
            else//המוצר כבר מחוק
            {
                throw new DoesntExistException("the product for Delete already deleted!!!");
            }
        }
    }
    public void Update(Product item)
    {      
        try
        {
            GetById(item.ProductID);
        }
        catch
        {
            throw new DoesntExistException("the product for Update is not exist in list of products!!!");
        }
        Delete(item.ProductID);
        Add(item);
    }

}




