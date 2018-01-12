using System;

public class CircularQueue<T>
{
    private const int DefaultCapacity = 4;
    private T[] arr;
    private int capacity;
    private int head;
    private int tail;
    public int Count { get; private set; }

    public CircularQueue(int initialCapacity = DefaultCapacity)
    {
        this.arr = new T[initialCapacity];
        this.capacity = initialCapacity;
        this.head = 0;
        this.tail = 0;
        this.Count = 0;
    }

    public void Enqueue(T element)
    {
        if (this.Count == this.capacity)
        {
            this.Grow();
        }
        this.arr[this.tail] = element;
        this.tail = (this.tail + 1) % this.capacity;
        this.Count++;
    }

    private void Grow()
    {
        T[] grownArr = new T[this.capacity * 2];
        this.CopyAllElements(grownArr);
        this.arr = grownArr;
        this.head = 0;
        this.tail = this.Count;
        this.capacity *= 2;
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < this.Count; i++)
        {
            int index = (i + this.head) % this.capacity;
            newArray[i] = this.arr[index];
        }
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T element = this.arr[this.head];
        this.arr[this.head] = default(T);
        this.head = (this.head + 1) % this.capacity;
        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        T[] result = new T[this.Count];
        this.CopyAllElements(result);
        return result;
    }
}


public class Example
{
    public static void Main()
    {

        CircularQueue<int> queue = new CircularQueue<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
