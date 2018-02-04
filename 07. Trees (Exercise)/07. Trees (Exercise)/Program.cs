using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static Dictionary<int, Tree<int>> tree = new Dictionary<int, Tree<int>>();

    static void Main(string[] args)
    {
        int inputs = int.Parse(Console.ReadLine());
        for (int i = 0; i < inputs - 1; i++)
        {
            int[] nodes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int parent = nodes[0];
            int child = nodes[1];
            if (!tree.ContainsKey(parent))
            {
                tree.Add(parent, new Tree<int>(parent, null));
            }
            tree.Add(child, new Tree<int>(child, tree[parent]));
            tree[parent].Children.Add(tree[child]);
        }
        //Console.WriteLine("Root node: {0}", FindRoot().Value);           //ex.1
        //PrintTree();                                                     //ex.2
        //PrintLeafNodes();                                                //ex.3
        //PrintMiddleNodes();                                              //ex.4
        //Console.WriteLine("Deepest node: {0}", FindDeepestNode().Value); //ex.5
        //PrintLongestPath();                                              //ex.6
        //int sum = int.Parse(Console.ReadLine());
        //PrintPathsWithSum(sum);                                          //ex.7
        //PrintSubtreesWithSum(sum);                                       //ex.8
    }

    private static Tree<int> FindRoot()
    {
        return tree.Values.Where(x => x.Parent == null).First();
    }

    private static void PrintTree()
    {
        Tree<int> root = FindRoot();
        Print(root, 0);
    }

    private static void Print(Tree<int> node, int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + node.Value);
        foreach (var child in node.Children)
        {
            Print(child, indent + 2);
        }
    }

    private static void PrintLeafNodes()
    {
        Tree<int> root = FindRoot();
        List<Tree<int>> results = new List<Tree<int>>();
        Console.Write("Leaf nodes: ");
        GetLeafNodes(root, results);
        Console.WriteLine(string.Join(" ", results.Select(x => x.Value).OrderBy(x => x)));
    }

    private static void GetLeafNodes(Tree<int> node, List<Tree<int>> results)
    {
        if (node.Children.Count == 0)
        {
            results.Add(node);
        }
        foreach (var child in node.Children)
        {
            GetLeafNodes(child, results);
        }
    }

    private static void PrintMiddleNodes()
    {
        Console.WriteLine("Middle nodes: {0}", string.Join(" ", tree.Values
            .Where(x => x.Parent != null && x.Children.Count != 0)
            .Select(x => x.Value)
            .OrderBy(x => x)));
    }

    private static Tree<int> FindDeepestNode()
    {
        Tree<int> root = FindRoot();
        List<Tree<int>> leafNodes = new List<Tree<int>>();
        GetLeafNodes(root, leafNodes);
        int maxDepth = -1;
        Tree<int> deepestNode = null;
        foreach (var node in leafNodes)
        {
            int depth = 0;
            Tree<int> current = node;
            while (current.Parent != null)
            {
                current = current.Parent;
                depth++;
            }
            if (depth > maxDepth)
            {
                maxDepth = depth;
                deepestNode = node;
            }
        }
        return deepestNode;
    }

    //private static Tree<int> FindDeepestNode() //DFS - alternative solution
    //{
    //    Tree<int> root = FindRoot();
    //    FindDeepestNode(root, 0);
    //    return result;
    //}

    //private static int maxDepth = -1;
    //private static Tree<int> result = null;
    //private static void FindDeepestNode(Tree<int> node, int depth)
    //{
    //    foreach (var child in node.Children)
    //    {
    //        FindDeepestNode(child, depth + 1);
    //    }
    //    if (depth > maxDepth)
    //    {
    //        maxDepth = depth;
    //        result = node;
    //    }
    //}

    //private static Tree<int> FindDeepestNode()  //BFS - alternative solution
    //{
    //    int maxDepth = -1;
    //    Tree<int> result = null;
    //    var queue = new Queue<Tuple<Tree<int>, int>>();
    //    queue.Enqueue(Tuple.Create(FindRoot(), 0));
    //    while (queue.Count != 0)
    //    {
    //        var tuple = queue.Dequeue();
    //        Tree<int> node = tuple.Item1;
    //        int nodeDepth = tuple.Item2;
    //        foreach (var child in node.Children)
    //        {
    //            queue.Enqueue(Tuple.Create(child, nodeDepth + 1));
    //        }
    //        if (nodeDepth > maxDepth)
    //        {
    //            maxDepth = nodeDepth;
    //            result = node;
    //        }
    //    }
    //    return result;
    //}

    private static void PrintLongestPath()
    {
        Tree<int> deepest = FindDeepestNode();
        Stack<int> path = GetPath(deepest);
        Console.WriteLine("Longest path: {0}", string.Join(" ", path));
    }

    private static Stack<int> GetPath(Tree<int> node)
    {
        Stack<int> path = new Stack<int>();
        path.Push(node.Value);
        while (node.Parent != null)
        {
            node = node.Parent;
            path.Push(node.Value);
        }
        return path;
    }

    private static void PrintPathsWithSum(int sum) //do later with subpaths
    {
        Console.WriteLine("Paths of sum {0}:", sum);
        Tree<int> root = FindRoot();
        List<Tree<int>> leafNodes = new List<Tree<int>>();
        GetLeafNodes(root, leafNodes);
        foreach (var node in leafNodes)
        {
            Stack<int> path = GetPath(node);
            if (path.Sum() == sum)
            {
                Console.WriteLine(string.Join(" ", path));
            }
        }
    }

    private static void PrintSubtreesWithSum(int sum)
    {
        Console.WriteLine("Subtrees of sum {0}:", sum);
        Tree<int> root = FindRoot();
        SubtreesDFS(root, sum);
    }

    private static int SubtreesDFS(Tree<int> node, int sum)
    {
        int subtreeSum = node.Value;
        foreach (var child in node.Children)
        {
            subtreeSum += SubtreesDFS(child, sum);
        }
        if (subtreeSum == sum)
        {
            PrintSubtree(node);
            Console.WriteLine();
        }
        return subtreeSum;
    }

    private static void PrintSubtree(Tree<int> node)
    {
        Console.Write(node.Value + " ");
        foreach (var child in node.Children)
        {
            PrintSubtree(child);
        }
    }
}

class Tree<T>
{
    public T Value { get; set; }
    public Tree<T> Parent { get; set; }
    public List<Tree<T>> Children { get; set; }

    public Tree(T value, Tree<T> parent)
    {
        this.Value = value;
        this.Parent = parent;
        this.Children = new List<Tree<T>>();
    }
}

