using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Prev { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Next = null;
            this.Prev = null;
        }
    }

    private Node<T> head;
    private Node<T> tail;
    public int Count { get; private set; }

    public DoublyLinkedList()
    {
        this.head = null;
        this.tail = null;
        this.Count = 0;
    }

    public void AddFirst(T element)
    {
        Node<T> node = new Node<T>(element);
        if (this.Count == 0)
        {
            this.head = node;
            this.tail = node;
        }
        else
        {
            this.head.Prev = node;
            node.Next = this.head;
            this.head = node;
        }
        this.Count++;
    }

    public void AddLast(T element)
    {
        Node<T> node = new Node<T>(element);
        if (this.Count == 0)
        {
            this.head = node;
            this.tail = node;
        }
        else
        {
            node.Prev = this.tail;
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
        T element = this.head.Value;
        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
            this.head.Prev = null;
        }
        this.Count--;
        return element;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T element = this.tail.Value;
        if (this.Count == 1)
        {
            this.head = null;
            this.tail = null;
        }
        else
        {
            this.tail = this.tail.Prev;
            this.tail.Next = null;
        }
        this.Count--;
        return element;
    }

    public void ForEach(Action<T> action)
    {
        Node<T> current = this.head;
        while (current != null)
        {
            action(current.Value);
            current = current.Next;
        }
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

    public T[] ToArray()
    {
        T[] result = new T[this.Count];
        Node<T> current = this.head;
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = current.Value;
            current = current.Next;
        }
        return result;
    }
}

class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
