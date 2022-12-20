using DalApi;
using DO;
using System.Collections.Generic;

namespace Dal;

internal class DalProduct: IProduct
{
    DataSource DS = DataSource.GetInstance(); //מופע של מקור הנתונים 

    private bool IsExist(int ProductID)//מתודת עזר המחזירה אמת אם האיבר קיים אחרת יוחזר שקר
    {
        int indexOfSameId = DS.products.FindIndex(x => x.ProductID == ProductID);
        if (indexOfSameId == -1)//כאשר האיבר לא קיים ברשימה
        {
            return false;
        }
        else//האיבר קיים ברשימה
        {
            return true;
        }
    }
   
    public int Add(Product P)
    {
        int index = DS.products.FindIndex(x => x.ProductID == P.ProductID);
        if(index == -1)
        {
            DS.products.Add(P);
            return P.ProductID;
        }
        else
        {
            if (DS.products[index].IsDeleted!=true)
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
        Product? ProductById = DS.products.Find(x => x.ProductID == id && x.IsDeleted != true);
        if (ProductById.Value.ProductID==0)
        {
            throw new DoesntExistException("the product is not exist in list of products!!!");
        }         
        else
        {
            return (Product)ProductById;
        }
       
    }
    public List<Product> GetAll()
    {
        if (DS.products.Count == 0)
        {
            throw new notExistElementInList("The list for Products is Empty!!");
        }      
        else
        {
            List<Product>? allproducts = DS.products.FindAll(x => x.IsDeleted != true);
            return allproducts;
        }   
    }
    public void Delete(int id)
    {       
        int index = DS.products.FindIndex(x => x.ProductID == id&& x.IsDeleted!=true);
        if (index == -1)
        {
            throw new DoesntExistException("the product for Delete is not exist in list of products!!!");
        }

        else//האיבר קיים ברשימה
        {
            if (DS.products[index].IsDeleted!=true)
            {
                Product p = DS.products[index];
                p.IsDeleted = true;
                DS.products[index] = p;
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
