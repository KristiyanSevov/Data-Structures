using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        LinkedStack<int> test = new LinkedStack<int>();
        test.Push(1);
        test.Push(2);
        test.Push(3);
        test.Pop();
        Console.WriteLine(test.Count);
        foreach (var item in test.ToArray())
        {
            Console.WriteLine(item);
        }
    }
}

