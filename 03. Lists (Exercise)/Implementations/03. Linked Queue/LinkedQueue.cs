using System;
using System.Collections.Generic;

//Can be done singly-linked but exercise said to use double links
public class LinkedQueue<T>
{
    private class QueueNode<T>
    {
        public T Value { get; private set; }
        public QueueNode<T> NextNode { get; set; }
        public QueueNode<T> PrevNode { get; set; }

        public QueueNode(T value)
        {
            this.Value = value;
            this.NextNode = null;
            this.PrevNode = null;
        }
    }

    private QueueNode<T> head;
    private QueueNode<T> tail;
    public int Count { get; private set; }

    public LinkedQueue()
    {
        this.head = null;
        this.tail = null;
        this.Count = 0;
    }

    public void Enqueue(T element)
    {
        QueueNode<T> added = new QueueNode<T>(element);
        if (this.Count == 0)
        {
            this.head = added;
            this.tail = added;
        }
        else
        {
            added.PrevNode = this.tail;
            this.tail.NextNode = added;
            this.tail = added;
        }
        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T element = this.head.Value;
        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = this.head.NextNode;
            this.head.PrevNode = null;
        }
        this.Count--;
        return element;
    }

    public T[] ToArray() {
        T[] result = new T[this.Count];
        QueueNode<T> current = this.head;
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
