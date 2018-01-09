using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayList<T> : IEnumerable<T>
{
    private const int DefaultCapacity = 2;
    public int Count { get; private set; }
    public int Capacity { get; private set; }
    private T[] Arr;

    public ArrayList() : this(DefaultCapacity)
    {
    }

    public ArrayList(int capacity) 
    {
        this.Count = 0;
        this.Capacity = capacity;
        this.Arr = new T[capacity];
    }

    public ArrayList(IEnumerable<T> collection)
    {
        this.Count = 0;
        this.Capacity = DefaultCapacity;
        this.Arr = new T[DefaultCapacity];
        foreach (var item in collection)
        {
            this.Add(item);
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            return this.Arr[index];
        }

        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            Arr[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.Capacity)
        {
            this.Grow();
        }
        this.Arr[this.Count] = item;
        this.Count++;
    }

    private void Grow()
    {
        T[] grownArr = new T[this.Capacity * 2];
        this.Capacity *= 2;
        Array.Copy(this.Arr, grownArr, this.Count);
        this.Arr = grownArr;
    }

    public T RemoveAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        T removed = this.Arr[index];
        for (int i = index; i < this.Count - 1; i++)
        {
            this.Arr[i] = this.Arr[i + 1];
        }
        this.Arr[this.Count - 1] = default(T);
        this.Count--;
        if (this.Count * 3 <= this.Capacity && this.Capacity > 2)
        {
            this.Shrink();
        }
        return removed;
    }

    private void Shrink()
    {
        T[] shrunkArr = new T[this.Capacity / 2];
        this.Capacity /= 2;
        Array.Copy(this.Arr, shrunkArr, this.Count);
        this.Arr = shrunkArr;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < Count; i++)
        {
            yield return this.Arr[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
       return GetEnumerator();
    }
}
