
static class StaticTools
{
    public static int string2(this string str)
    {
        return int.Parse(str);
    }
}
static class SticTools
{
    public static int ToInt3(this int str)
    {
        str += 8;
        return str;
    }
}
static class exstension3
{
    public static double Todouble (this double str)
    {
        str += 5.5;
        return str;
    }
}

internal class Program
{
    private static void Main(string[] args)
    {
        string s = "555";
        int y = s.string2();
        Console.WriteLine(y);
        int g = 1;
        Console.WriteLine(g.ToInt3() );
        double d = 2.5;
        Console.WriteLine(d.Todouble());
      
    }
}