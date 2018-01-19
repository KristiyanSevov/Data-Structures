using System;
using System.Collections.Generic;

public class Tree<T>
{
    private T value;
    private List<Tree<T>> children;
  
    public Tree(T value, params Tree<T>[] children)
    {
        this.value = value;
        this.children = new List<Tree<T>>(children);
    }

    public void Print(int indent = 0)
    {
        Tree<T> root = this;
        this.PrintTree(root, indent);
    }

    private void PrintTree(Tree<T> node, int indent)
    {
        Console.WriteLine(new string(' ', indent) + node.value);
        foreach (var child in node.children)
        {
            PrintTree(child, indent + 2);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.value);
        foreach (var child in this.children)
        {
            child.Each(action);
        }
    }

    ////Elegant but not very efficient
    //public IEnumerable<T> OrderDFS()
    //{
    //    foreach (var child in this.children)
    //    {
    //        foreach (T value in child.OrderDFS())
    //        {
    //            yield return value;
    //        }
    //    }
    //    yield return this.value;
    //}

    public IEnumerable<T> OrderDFS()
    {
        List<T> result = new List<T>();
        Tree<T> root = this;
        this.OrderDFS(root, result);
        return result;
    }

    private void OrderDFS(Tree<T> node, List<T> result)
    {
        foreach (var child in node.children)
        {
            OrderDFS(child, result);
        }
        result.Add(node.value);
    }


    public IEnumerable<T> OrderBFS()
    {
        Queue<Tree<T>> queue = new Queue<Tree<T>>();
        queue.Enqueue(this);
        while (queue.Count > 0)
        {
            Tree<T> node = queue.Dequeue();
            yield return node.value;
            foreach (var child in node.children)
            {
                queue.Enqueue(child);
            }
        }
    }
}
