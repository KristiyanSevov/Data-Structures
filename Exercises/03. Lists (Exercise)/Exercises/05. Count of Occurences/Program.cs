using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05.Count_of_Occurences
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
            foreach (var num in counts)
            {
                Console.WriteLine("{0} -> {1} times", num.Key, num.Value);
            }
        }
    }
}
