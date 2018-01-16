using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.Remove_Odd_Occurences
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            Dictionary<int, int> counts = new Dictionary<int, int>();
            foreach (var num in numbers)
            {
                if (!counts.ContainsKey(num))
                {
                    counts.Add(num, 0);
                }
                counts[num]++;
            }
            Console.WriteLine(String.Join(" ", numbers.Where(x => counts[x] % 2 == 0)));
        }
    }
}
