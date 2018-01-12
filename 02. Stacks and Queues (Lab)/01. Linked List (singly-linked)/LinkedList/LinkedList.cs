using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    private Node<T> head;
    private Node<T> tail;
    public int Count { get; private set; }

    public LinkedList()
    {
        this.head = null;
        this.tail = null;
        this.Count = 0;
    }

    public void AddFirst(T item)
    {
        Node<T> node = new Node<T>(item);
        if (this.Count == 0)
        {
            this.head = node;
            this.tail = node;
        }
        else
        {
            node.Next = this.head;
            this.head = node;
        }
        this.Count++;
    }

    public void AddLast(T item)
    {
        Node<T> node = new Node<T>(item);
        if (this.Count == 0)
        {
            this.head = node;
            this.tail = node;
        }
        else
        {
            this.tail.Next = node;
            this.tail = node;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T removed = this.head.Value;
        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
        }
        this.Count--;
        return removed;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T removed = this.tail.Value;
        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            Node<T> parent = this.head;
            while (parent.Next != this.tail)
            {
                parent = parent.Next;
            }
            parent.Next = null;
            this.tail = parent;
        }
        this.Count--;
        return removed;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T> current = this.head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;

        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

class Node<T>
{
    public T Value { get; set; }
    public Node<T> Next { get; set; }

    public Node(T value)
    {
        this.Value = value;
        this.Next = null;
    }
}
