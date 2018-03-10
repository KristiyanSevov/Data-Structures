using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Program
{
    static void Main(string[] args)
    {
        ReversedList<int> test = new ReversedList<int>();
        test.Add(0);
        test.Add(1);
        test.Add(2);
        test.Add(3);
        test.Add(4);
        test.RemoveAt(1);
        foreach (var item in test)
        {
            Console.WriteLine(item);
        }
    }
}
