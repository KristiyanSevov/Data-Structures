using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }
        int heightDiff = this.GetHeigth(node.Left) - this.GetHeigth(node.Right);
        if (heightDiff > 1)
        {
            int childHeightDiff = this.GetHeigth(node.Left.Left) - this.GetHeigth(node.Left.Right);
            if (childHeightDiff < 0)
            {
                node.Left = RotateLeft(node.Left);
            }
            node = RotateRight(node);
        }
        else if (heightDiff < -1)
        {
            int childHeightDiff = this.GetHeigth(node.Right.Left) - this.GetHeigth(node.Right.Right);
            if (childHeightDiff > 0)
            {
                node.Right = RotateRight(node.Right);
            }
            node = RotateLeft(node);
        }
        node.Height = 1 + Math.Max(GetHeigth(node.Left), GetHeigth(node.Right));
        return node;
    }

    private int GetHeigth(Node<T> node)
    {
        return node != null ? node.Height : 0;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> parent = node.Right;
        node.Right = parent.Left;
        parent.Left = node;
        node.Height = 1 + Math.Max(GetHeigth(node.Left), GetHeigth(node.Right));
        return parent;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> parent = node.Left;
        node.Left = parent.Right;
        parent.Right = node;
        node.Height = 1 + Math.Max(GetHeigth(node.Left), GetHeigth(node.Right));
        return parent;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
