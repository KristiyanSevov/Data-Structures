using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(5);
        tree.Insert(6);
        tree.Insert(3);
        tree.Insert(4);
        tree.Delete(3);

        Console.WriteLine();
    }
}
