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

    //private bool IsExist(int ProductID)//מתודת עזר המחזירה אמת אם האיבר קיים אחרת יוחזר שקר
    //{
    //    int indexOfSameId = DS.products.FindIndex(x => x.ProductID == ProductID);
    //    if (indexOfSameId == -1)//כאשר האיבר לא קיים ברשימה
    //    {
    //        return false;
    //    }
    //    else//האיבר קיים ברשימה
    //    {
    //        return true;
    //    }
    //}
   
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
            //List<Product>? allproducts = DS.products.FindAll(x => x.IsDeleted != true);
            //return allproducts;
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




//using System;
//using System.Threading;
//using DO;
//using DalApi;

//namespace Dal;

//public class DalProduct : IProduct
//{
//    DataSource _ds = DataSource.s_instance;//to access the data 

//    public int Add(DO.Product p)//add Product to a list and return its id
//    {
//        if (p.ID == 0)//want to add a new item to the list
//        {
//            p.ID = DataSource.Config.NextProductNumber;//set an id number to Product p
//            _ds.productList.Add(p);//add p to the Product list
//            return p.ID;//return the id
//        }
//        int ind = _ds.productList.FindIndex(x => x?.ID == p.ID && x?.IsDeleted == false);//save index of product with matching id if not deleted
//        if (ind != -1)//exists already so cant add again
//        {
//            throw new IdExistException("Unothorized override");//error
//        }
//        _ds.productList.Add(p);//add p to the Product list
//        return p.ID;//return the id
//        //ind = _ds.productList.FindIndex(x => x?.ID == p.ID && x?.IsDeleted == true);//save index of product with matching id if deleted
//        //if (ind != -1)//already exists but deleted 
//        //{
//        //    _ds.productList.Add(p);//add p to the Product list
//        //    return p.ID;//return the id
//        //}
//        //throw new IdExistException("Unothorized override");//error
//    }

//    public Product GetById(int id)
//    {
//        Product? res = _ds.productList.Find(x => x?.ID == id && x?.IsDeleted == false);//find a priduct with same id and exists
//        if (res?.ID != id || res?.IsDeleted == true)//if not found
//            throw new IdNotExistException("The product does not exist\n");
//        return res ?? throw new IdNotExistException("id does not exist\n");
//    }

//    public void Delete(int id)
//    {
//        int index = _ds.productList.FindIndex(x => x?.ID == id);

//        if (index == -1)//if does not exist
//            throw new IdNotExistException("Product does not exist");
//        _ds.productList.RemoveAt(index);//remove from list
//    }
//    public void Update(Product p)
//    {
//        int index = _ds.productList.FindIndex(x => x?.ID == p.ID);

//        if (index == -1)//if does not exist
//            throw new IdNotExistException("The product you wish to update does not exist");
//        _ds.productList[index] = p;//place new product in place of existing one 
//    }

//    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter)
//    {
//        if (filter == null)//select whole list
//        {
//            return from v in _ds.productList
//                   where v?.IsDeleted == false
//                   select v;
//        }
//        return from v in _ds.productList//select with filter
//               where v?.IsDeleted == false && filter(v)
//               select v;
//    }

//    public Product GetByFilter(Func<Product?, bool>? filter)
//    {
//        if (filter == null)
//        {
//            throw new ArgumentNullException(nameof(filter));//filter is null
//        }
//        foreach (Product? p in _ds.productList)
//        {
//            if (p != null && p?.IsDeleted == false && filter(p))
//            {
//                return (Product)p;
//            }
//        }
//        throw new Exceptions("Does not exist\n");
//    }


//}




























//int Add(T item);
//T GetById(int id);
//void Update(T item);
//void Delete(int id);
//IEnumerable<T> GetAll();


//if (IsExist(id))
//{
//    int index= DS.products.FindIndex(x => x.ProductID == id);
//    return DS.products[index];
//}
//else
//{
//    throw new DoesntExistException("the product is not exist in list of products!!!");
//}

//if(IsExist(id))
//{
//    int index = DS.products.FindIndex(x => x.ProductID == id);
//    DS.products.RemoveAt(index);
//}
//else
//{
//    throw new DoesntExistException("the product for Delete is not exist in list of products!!!");
//}

//if(IsExist(item.ProductID))
//{
//    int index = DS.products.FindIndex(x => x.ProductID == item.ProductID);
//    DS.products[index] = item;
//}
//else
//{
//    throw new DoesntExistException("the product for Update is not exist in list of products!!!");
//}
//public class DalProduct//נתון מוצר
//{
//    public int Add(Product p)
//    {
//        int x = DataSource.Config.index1forproducts;
//        for (int i = 0; i < x; i++)
//        {
//            if (DataSource.products[i].ProductID == p.ProductID)
//            {
//                throw new Exception(" the object already exist in array!");
//            }
//        }
//        DataSource.products[x] = p;
//        DataSource.Config.index1forproducts++;
//        return p.ProductID;
//    }
//    //מתודת בקשה\קריא של אובייקט בודד שתקבל מספר מזהה של הישות (שימו לב - לא מדובר במציין\אינדקס במערך!) ותחזיר את האובייקט המתאים
//    public Product get(int ID)
//    {
//        for (int i = 0; i < DataSource.Config.index1forproducts; i++)
//        {
//            if (DataSource.products[i].ProductID == ID)
//            {
//                return DataSource.products[i];
//            }
//        }
//        throw new Exception(" the object not exist in array!");
//    }
//    public int numElement()
//    {
//        return DataSource.Config.index1forproducts;
//    }
//    public Product[] GetProductslist()
//    {
//        return DataSource.products;
//    }
//    //מתודת מחיקת אובייקט של ישות שתקבל מספר מזהה של הישות
//    public void delete(int id)
//    {

//        if (Exist(id))
//        {
//            Product[] newproducts = new Product[DataSource.products.Length - 1];
//            for (int i = 0; i < DataSource.Config.index1forproducts; i++)
//            {
//                if (DataSource.products[i].ProductID == id)
//                {
//                    for (int j = 0; j < i; j++)
//                    {
//                        newproducts[DataSource.Config.ind1] = DataSource.products[j];
//                        DataSource.Config.ind1++;
//                    }
//                    for (int k = i + 1; k < DataSource.Config.index1forproducts; k++)
//                    {
//                        newproducts[DataSource.Config.ind1] = DataSource.products[k];
//                        DataSource.Config.ind1++;
//                    }
//                }
//            }
//            DataSource.products = newproducts;
//            DataSource.Config.index1forproducts--;
//        }
//        else//אם הערך למחיקה לא קיים במערך
//        {
//            throw new Exception("the value of delete is not exist in array");
//        }
//    }
//    public bool Exist(int id)
//    {
//        for (int i = 0; i < DataSource.Config.index1forproducts; ++i)
//        {
//            if (DataSource.products[i].ProductID == id)
//            {
//                return true;
//            }
//        }
//        return false;
//    }
//    //מתודת עדכון אובייקט שתקבל אובייקט חדש
//    public void update(Product p)
//    {
//        if (Exist(p.ProductID))
//        {
//            for (int i = 0; i < DataSource.Config.index1forproducts; i++)
//            {
//                if (DataSource.products[i].ProductID == p.ProductID)
//                {
//                    DataSource.products[i] = p;
//                }
//            }
//        }
//        else//כאשר האיבר לא נמצא
//            throw new Exception("the value is not exist in array");
//    }
//}
