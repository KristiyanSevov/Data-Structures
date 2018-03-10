using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private class Node
    {
        public T Value { get; set; }
        public Node RightChild { get; set; }
        public Node LeftChild { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.LeftChild = null;
            this.RightChild = null;
        }
    }

    private Node root;

    public BinarySearchTree()
    {
        this.root = null;
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }
        Node parent = null;
        Node current = this.root;
        while (current != null)
        {
            int compare = current.Value.CompareTo(value);
            if (compare > 0)
            {
                parent = current;
                current = current.LeftChild;
            }
            else if (compare < 0)
            {
                parent = current;
                current = current.RightChild;
            }
            else
            {
                return;
            }
        }
        if (parent.Value.CompareTo(value) > 0)
        {
            parent.LeftChild = new Node(value);
        }
        else
        {
            parent.RightChild = new Node(value);
        }
    }

    public bool Contains(T value)
    {
        Node current = this.root;
        while (current != null)
        {
            if (current.Value.CompareTo(value) == 0)
            {
                return true;
            }
            else if (current.Value.CompareTo(value) < 0)
            {
                current = current.RightChild;
            }
            else
            {
                current = current.LeftChild;
            }
        }
        return false;
    }

    public void DeleteMin()
    {
        if (root == null)
        {
            return;
        }
        if (root.LeftChild == null && root.RightChild == null)
        {
            root = null;
            return;
        }
        Node parent = null;
        Node current = this.root;
        while (current.LeftChild != null)
        {
            parent = current;
            current = current.LeftChild;
        }
        parent.LeftChild = current.RightChild;
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node current = this.root;
        while (current != null)
        {
            if (current.Value.CompareTo(item) > 0)
            {
                current = current.LeftChild;
            }
            else if (current.Value.CompareTo(item) < 0)
            {
                current = current.RightChild;
            }
            else
            {
                BinarySearchTree<T> result = new BinarySearchTree<T>();
                result.CopySubtree(current);
                return result;
            }
        }
        //return new BinarySearchTree<T>();
        return null;
    }

    private void CopySubtree(Node node)
    {
        if (node != null)
        {
            this.Insert(node.Value);
            this.CopySubtree(node.LeftChild);
            this.CopySubtree(node.RightChild);
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        List<T> result = new List<T>();
        Node start = this.root;
        Range(start, result, startRange, endRange);
        return result;

    }

    private void Range(Node node, List<T> result, T startRange, T endRange)
    {
        if (node != null)
        {
            if (node.Value.CompareTo(startRange) > 0)
            {
                Range(node.LeftChild, result, startRange, endRange);
            }
            if (node.Value.CompareTo(startRange) >= 0 && node.Value.CompareTo(endRange) <= 0)
            {
                result.Add(node.Value);
            }
            if (node.Value.CompareTo(endRange) < 0)
            {
                Range(node.RightChild, result, startRange, endRange);
            } 
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node != null)
        {
            EachInOrder(node.LeftChild, action);
            action(node.Value);
            EachInOrder(node.RightChild, action);
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {

    }
}
