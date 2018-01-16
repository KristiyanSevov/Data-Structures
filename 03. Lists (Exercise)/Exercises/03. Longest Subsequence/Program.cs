using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.Longest_Subsequence
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> nums = Console.ReadLine().Split(' ').Select(int.Parse).ToList();
            List<int> result = FindLongestSubsequence(nums);
            Console.WriteLine(String.Join(" ", result));
        }

        private static List<int> FindLongestSubsequence(List<int> nums)
        {
            int count = 1;
            int maxCount = 1;
            int value = nums[0];
            for (int i = 0; i < nums.Count - 1; i++)
            {
                if (nums[i] == nums[i+1])
                {
                    count++;
                    if (i == nums.Count - 2 && count > maxCount)
                    {
                        value = nums[i];
                        maxCount = count;
                    }
                }
                else
                {
                    if (count > maxCount)
                    {
                        maxCount = count;
                        value = nums[i];
                    }
                    count = 1;
                }
            }
            List<int> result = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                result.Add(value);
            }
            return result;
        }
    }
}
