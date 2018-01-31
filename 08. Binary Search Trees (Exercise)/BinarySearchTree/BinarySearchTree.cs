using System;
using System.Collections.Generic;

//Most of these can be written much better but wanted to try out some ideas.

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable   
{
    private Node root;

    private Node FindElement(T element, out Node parent)
    {
        Node current = this.root;
        parent = null;
        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                parent = current;
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                parent = current;
                current = current.Right;
            }
            else
            {
                break;
            }
        }
        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            return new Node(element);
        }
        if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    {
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element, out Node parent);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element, out Node parent);

        return new BinarySearchTree<T>(current);
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            current.Count--;
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void DeleteMax() //up to here were given, from this one are mine
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        Node parent = null;
        Node current = this.root;
        while (current.Right != null)
        {
            current.Count--;
            parent = current;
            current = current.Right;
        }
        if (parent != null)
        {
            parent.Right = current.Left;
        }
        else
        {
            this.root = current.Left;
        }
    }

    public int Count()
    {
        return this.Count(this.root);
    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.Count;
    }

    public int Rank(T element)
    {
        int count = 0;
        bool inTree = true;
        Node input = FindElement(element, out Node parent);
        if (input == null)
        {
            input = FindNextSmaller(element, out parent); //fix for inputs not in tree
            if (input == null)
            {
                return count;
            }
            count++;
            inTree = false;
        }
        if (element.CompareTo(this.root.Value) <= 0)
        {
            count += input.Left != null ? input.Left.Count : 0;
            while (parent != null)
            {
                if (parent != null && input != parent.Left)
                {
                    count += Count(parent.Left) + 1;  //parent + left children unless we're left child (smaller)
                }
                input = FindElement(parent.Value, out Node newParent); //go up
                parent = newParent;
            }
        }
        else
        {
            count = this.root.Count - 1; //-1 for the element itself, now subtract the larger ones
            if (!inTree)
            {
                count++;
            }
            count -= input.Right != null ? input.Right.Count : 0;
            while (parent != null)
            {
                if (parent != null && input != parent.Right)
                {
                    count -= Count(parent.Right) + 1;  //parent + right children unless we're right child (bigger)
                }
                input = FindElement(parent.Value, out Node newParent); //go up
                parent = newParent;
            }
        }
        return count;
    }

    private Node FindNextSmaller(T element, out Node parent)
    {
        parent = null;
        Node result = null;
        Node current = this.root;
        Node currentParent = null;
   
        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                if ((result == null || current.Value.CompareTo(result.Value) > 0) && current.Value.CompareTo(element) < 0)
                {
                    result = current;
                    parent = currentParent;
                }
                if (current.Left == null)
                {
                    return result;
                }
                currentParent = current;
                current = current.Left;
            }
            else
            {
                if ((result == null || current.Value.CompareTo(result.Value) > 0) && current.Value.CompareTo(element) < 0)
                {
                    result = current;
                    parent = currentParent;
                }
                if (current.Right == null)
                {
                    return current;
                }
                currentParent = current;
                current = current.Right;
            }
        }
        return result;
    }

    public T Select(int rank)
    {
        if (this.root == null)
        {
            throw new InvalidOperationException("empty tree");
        }
        if (rank == 0)
        {
            return FindMinElement().Value;
        }
        return Select(this.root, rank);
    }

    private Node FindMinElement()
    {
        Node current = this.root;
        while (current.Left != null)
        {
            current = current.Left;
        }
        return current;
    }

    private T Select(Node node, int rank)
    {
        T result;
        if (rank == 0 || rank == Count(node.Left))
        {
            return node.Value;
        }
        if (node.Left != null && node.Left.Count > rank)
        {
            result = Select(node.Left, rank);
        }
        else
        {
            if (node.Right == null)
            {
                throw new InvalidOperationException("No such element.");
            }
            result = Select(node.Right, rank - Count(node.Left) - 1);
        }
        return result;
    }

    public T Floor(T element) //ugly and inefficient but wanted to try this way
    {
        Node input = FindElement(element, out Node parent);
        if (input == null)
        {
            throw new InvalidOperationException("Input value not in tree");
        }
        if (parent == null || input == parent.Left) //if it's root or left child
        {
            Node current = input.Left;
            if (current == null) //no left child -> no smaller down the tree
            {
                while (parent != null && parent.Right != input) //go up until smaller is found
                {
                    input = FindElement(parent.Value, out Node newParent);
                    parent = newParent;
                }
                if (parent != null)
                {
                    return parent.Value;
                }
                else
                {
                    throw new InvalidOperationException("No smaller element");
                }
            }
            while (current.Right != null) //find biggest of the smaller
            {
                current = current.Right;
            }
            return current.Value;
        }
        else //it's a right child  
        {
            Node current = input.Left;
            if (current == null)
            {
                return parent.Value;
            }
            while (current.Right != null)
            {
                current = current.Right;
            }
            return current.Value.CompareTo(parent.Value) > 0 ? current.Value : parent.Value;
        }
    }

    public T Ceiling(T element)
    {
        Node input = FindElement(element, out Node parent);
        if (input == null)
        {
            throw new InvalidOperationException("Input value not in tree");
        }
        if (parent == null || input == parent.Right)
        {
            Node current = input.Right;
            if (current == null)
            {
                while (parent != null && parent.Left != input) //go up until bigger is found
                {
                    input = FindElement(parent.Value, out Node newParent);
                    parent = newParent;
                }
                if (parent != null)
                {
                    return parent.Value;
                }
                else
                {
                    throw new InvalidOperationException("No bigger element");
                }
            }
            while (current.Left != null) //find smallest of the bigger
            {
                current = current.Left;
            }
            return current.Value;
        }
        else
        {
            Node current = input.Right;
            if (current == null)
            {
                return parent.Value;
            }
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current.Value.CompareTo(parent.Value) < 0 ? current.Value : parent.Value;
        }
    }

    public void Delete(T element)
    {
        Node toDelete = FindElement(element, out Node parent);
        bool isRoot = false;
        if (toDelete == null)
        {
            throw new InvalidOperationException();
        }
        if (parent == null)
        {
            isRoot = true;
            if (this.Count() == 1)
            {
                this.root = null;
                return;
            }

        }
        if (toDelete.Right == null)
        {
            if (isRoot)
            {
                this.root = toDelete.Left;
            }
            else if (toDelete == parent.Left)
            {
                parent.Left = toDelete.Left;
            }
            else
            {
                parent.Right = toDelete.Left;
            }
        }
        else
        {
            Node currentParent = null;
            Node current = toDelete.Right; //find the smallest of the bigger
            while (current.Left != null)
            {
                current.Count--;
                currentParent = current;
                current = current.Left;
            }
            if (currentParent != null)
            {
                currentParent.Left = current.Right;
                current.Right = toDelete.Right;
            }
            current.Left = toDelete.Left;
            if (isRoot)
            {
                this.root = current;
                this.root.Count = 1 + this.Count(this.root.Left) + Count(this.root.Right);
                return;
            }
            else if (parent.Left == toDelete)
            {
                parent.Left = current;
            }
            else
            {
                parent.Right = current;
            }
        }
        while (parent != null) //updating counts up the tree - easier with parent references
        {
            parent.Count--;
            FindElement(parent.Value, out Node newParent);
            parent = newParent;
        }
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
            this.Count = 1;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Count { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(7);
        bst.Insert(9);
        //bst.Delete(7);
        Console.WriteLine("count: " + bst.Count());
        bst.EachInOrder(Console.WriteLine);
    }
}