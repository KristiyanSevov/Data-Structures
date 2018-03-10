using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        ArrayList<int> test = new ArrayList<int>();
        test.Add(2);
        test.Add(3);
        test.Add(1);
        test.RemoveAt(1);
        test.Add(4);
        foreach (var item in test)
        {
            Console.WriteLine(item);
        }
    }
}
