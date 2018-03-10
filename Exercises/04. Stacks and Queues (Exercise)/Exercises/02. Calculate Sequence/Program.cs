using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02.Calculate_Sequence
{
    class Program
    {
        static void Main(string[] args)
        {
            //Queue not really needed for this one
            Queue<int> sequence = new Queue<int>();
            List<int> result = new List<int>();

            int start = int.Parse(Console.ReadLine());
            sequence.Enqueue(start);
            result.Add(start);

            int num;
            int next;
            while (result.Count < 50)
            {
                num = sequence.Dequeue();
                next = num + 1;
                sequence.Enqueue(next);
                result.Add(next);
                if (result.Count == 50)
                {
                    break;
                }
                next = num * 2 + 1;
                sequence.Enqueue(next);
                result.Add(next);
                if (result.Count == 50)
                {
                    break;
                }
                next = num + 2;
                sequence.Enqueue(next);
                result.Add(next);
            }
            Console.WriteLine(String.Join(", ", result));
        }
    }
}
