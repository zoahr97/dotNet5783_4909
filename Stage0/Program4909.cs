using System;

namespace Stage0
{
    partial class program//מחלקה חלקית
    {
        // static void Main(string[] args)
        //{
        //    Welcome4909();
        //    Welcome3248();
        //    Console.ReadKey();
        //}
        static partial void Welcome3248();//פונקציית מחלקה
        private static void Welcome4909()//refactor method
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
            Console.ReadKey();
            Console.ResetColor();   
        }
    }
}
