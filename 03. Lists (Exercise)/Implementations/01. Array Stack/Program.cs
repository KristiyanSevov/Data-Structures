using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        ArrayStack<int> test = new ArrayStack<int>();
        test.Push(1);
        test.Push(2);
        test.Push(3);
        test.Push(4);
        test.Pop();
        test.Pop();
        Console.WriteLine("Count: "+ test.Count);
        int[] copy = test.ToArray();
        foreach (var item in copy)
        {
           Console.WriteLine(item);
        }
    }
}
