using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class ReversedList<T> : IEnumerable<T>
{
    private const int defaultCapacity = 2;
    private T[] arr;
    public int Capacity { get; private set; }
    public int Count { get; private set; }

    public ReversedList() : this(defaultCapacity)
    {
    }

    public ReversedList(int capacity)
    {
        this.Capacity = capacity;
        this.Count = 0;
        this.arr = new T[this.Capacity];
    }

    public void Add(T element)
    {
        if (this.Count == this.Capacity)
        {
            this.Grow();
        }
        this.arr[this.Count] = element;
        this.Count++;
    }

    private void Grow()
    {
        T[] grownArr = new T[Capacity * 2];
        for (int i = 0; i < this.Count; i++)
        {
            grownArr[i] = this.arr[i];
        }
        this.arr = grownArr;
        this.Capacity *= 2;  
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= this.Count)
            {
                throw new InvalidOperationException();
            }
            return this.arr[this.Count - 1 - index]; //to fix
        }
        set
        {
            if (index < 0 || index >= this.Count)
            {
                throw new InvalidOperationException();
            }
            this.arr[this.Count - 1 - index] = value;
        }
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new InvalidOperationException();
        }
        index = this.Count - 1 - index;
        for (int i = index; i < this.Count - 1; i++)
        {
            this.arr[i] = this.arr[i + 1];
        }
        this.arr[this.Count - 1] = default(T);
        this.Count--;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return arr[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
