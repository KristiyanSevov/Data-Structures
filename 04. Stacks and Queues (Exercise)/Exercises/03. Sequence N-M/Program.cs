using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _06.Sequence_N_M
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<Node> queue = new Queue<Node>();
            string[] inputs = Console.ReadLine().Split(' ');
            int start = int.Parse(inputs[0]);
            int end = int.Parse(inputs[1]);
            queue.Enqueue(new Node(start, null));
            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                if (current.Value == end)
                {
                    PrintQueue(current);
                    return;
                }                               //making it work for negatives, e.g. -5 -10 input     
                if (((current.Prev == null || current.Value < current.Prev.Value) && current.Value > end) ||  
                    ((current.Prev == null || current.Value > current.Prev.Value) && current.Value < end)) 
                {
                    queue.Enqueue(new Node(current.Value + 1, current));
                    queue.Enqueue(new Node(current.Value + 2, current));
                    queue.Enqueue(new Node(current.Value * 2, current));
                } 
            }
        }

        private static void PrintQueue(Node node)
        {
            List<int> path = new List<int>();
            while (node != null)
            {
                path.Add(node.Value);
                node = node.Prev;
            }
            path.Reverse();
            Console.WriteLine(String.Join(" -> ", path));
        }
    }
    
    class Node
    {
        public Node Prev { get; set; }
        public int Value { get; set; }

        public Node(int value, Node prev)
        {
            this.Prev = prev;
            this.Value = value;
        }
    }
}
