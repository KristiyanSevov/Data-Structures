using System;
using System.Collections.Generic;
using System.Linq;

public class ArrayStack<T>
{
    private const int InitialCapacity = 16;
    private int capacity;
    private T[] arr;
    public int Count { get; private set; }

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.capacity = capacity;
        this.arr = new T[this.capacity];
        this.Count = 0;
    }

    public void Push(T element)
    {
        if (this.Count == this.capacity)
        {
            this.Grow();
        }
        this.arr[this.Count] = element;
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        T element = this.arr[this.Count - 1];
        this.arr[this.Count - 1] = default(T);
        this.Count--;
        return element;
    }

    public T[] ToArray()
    {
        T[] result = new T[this.Count];
        Array.Copy(this.arr, result, this.Count);
        Array.Reverse(result);
        return result;
    }

    private void Grow()
    {
        T[] grownArr = new T[this.capacity * 2];
        Array.Copy(this.arr, grownArr, this.Count);
        this.arr = grownArr;
        this.capacity *= 2;
    }
}
