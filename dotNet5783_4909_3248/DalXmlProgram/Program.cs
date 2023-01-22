Test_Student_LinqToXml();

static void Test_Student_LinqToXml()
{
    DalApi.IProduct dal = new Dal.Product();
    try
    {
        for (int i = 10; i > 0; --i)
            dal.Add(new()
            {
                ID = i,
                FirstName = "FN" + i,
                LastName = "LN" + i,
                StudentStatus = DO.StudentStatus.ACTIVE,
                BirthDate = DateTime.ParseExact("12.03.85", "dd.MM.yy", null),
                Grade = 100
            });

        Console.WriteLine(dal.GetById(1));
        dal.Delete(5);
        dal.Update(new DO.Student
        {
            ID = 3,
            FirstName = "FNNew",
            //LastName = "LNNew",
            //StudentStatus = DO.StudentStatus.FINISHED,
            BirthDate = DateTime.ParseExact("15.05.55", "dd.MM.yy", null),
            Grade = 100
        });

        foreach (var item in dal.GetAll()) Console.WriteLine(item);
    }
    catch (Exception ex) { Console.WriteLine(ex.Message); }
}
