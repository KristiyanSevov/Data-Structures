using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01.Sum_and_Average
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input == String.Empty)
            {
                Console.WriteLine("Sum=0; Average=0.00");
                return;
            }
            List<int> nums = input.Split(' ').Select(int.Parse).ToList();
            Console.WriteLine("Sum={0}; Average={1:f2}", nums.Sum(), nums.Average());
        }
    }
}
