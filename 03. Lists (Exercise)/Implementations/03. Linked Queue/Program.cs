using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        LinkedQueue<int> test = new LinkedQueue<int>();
        test.Enqueue(1);
        test.Enqueue(2);
        test.Enqueue(3);
        int x = test.Dequeue();
        Console.WriteLine("Item: " + x);
        Console.WriteLine("Count: "+ test.Count);
        foreach (var item in test.ToArray())
        {
            Console.WriteLine(item);
        }
    }
}

