using System;
using System.Collections.Generic;

public class LinkedStack<T>
{
    private class Node<T>
    {
        public T Value { get; set; }
        public Node<T> NextNode { get; set; }

        public Node(T value, Node<T> nextNode = null)
        {
            this.Value = value;
            this.NextNode = nextNode;
        }
    }
    private Node<T> firstNode;
    public int Count { get; private set; }

    public LinkedStack()
    {
        this.Count = 0;
        this.firstNode = null;
    }

    public void Push(T element)
    {
        Node<T> added = new Node<T>(element, this.firstNode);
        this.Count++;
        this.firstNode = added;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T result = this.firstNode.Value;
        this.firstNode = this.firstNode.NextNode;
        this.Count--;
        return result;
    }

    public T[] ToArray()
    {
        T[] result = new T[this.Count];
        Node<T> current = firstNode;
        int index = 0;
        while (current != null)
        {
            result[index] = current.Value;
            current = current.NextNode;
            index++;
        }
        return result;
    }
}

