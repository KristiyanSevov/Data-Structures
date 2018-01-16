using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reverse_Numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input == String.Empty)
            {
                return;
            }
            string[] inputs = input.Split(' ');
            Stack<int> nums = new Stack<int>(inputs.Select(int.Parse));
            while (nums.Count != 0)
            {
                Console.Write(nums.Pop() + " ");
            }
        }
    }
}
