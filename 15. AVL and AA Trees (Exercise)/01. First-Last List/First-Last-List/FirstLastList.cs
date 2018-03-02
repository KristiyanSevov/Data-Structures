using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private OrderedBag<T> byAscending = new OrderedBag<T>();
    private OrderedBag<T> byDescending = new OrderedBag<T>((x, y) => y.CompareTo(x));
    private Dictionary<int, T> byInsertion = new Dictionary<int, T>();  //can use long instead of int to support more items
    private OrderedMultiDictionary<T, int> insertionValues =    
        new OrderedMultiDictionary<T, int>(true);  //can't use MultiDict because tests have T objects that don't override Equals                              
    private int insertionValue = 0;

    public int Count
    {
        get
        {
            return this.byInsertion.Count;
        }
    }

    public void Add(T element)
    {
        this.byAscending.Add(element);
        this.byDescending.Add(element);
        this.byInsertion.Add(insertionValue, element);
        this.insertionValues.Add(element, insertionValue);
        this.insertionValue++;
    }

    public void Clear()
    {
        this.byAscending = new OrderedBag<T>();
        this.byDescending = new OrderedBag<T>((x, y) => y.CompareTo(x));
        this.byInsertion.Clear();
        this.insertionValues.Clear();
        this.insertionValue = 0;
    }

    public IEnumerable<T> First(int count)
    {
        if (count > this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        foreach (var kvp in this.byInsertion.Take(count))
        {
            yield return kvp.Value;
        }
    }

    public IEnumerable<T> Last(int count) //will work fine as long as there weren't many insertions and deletions 
    {
        if (count > this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        int counter = 0;
        int value = insertionValue;
        while (counter < count)
        {
            if (byInsertion.ContainsKey(value))
            {
                yield return byInsertion[value];
                counter++;
            }
            value--;
        }
        //foreach (var kvp in this.byInsertion.Reverse().Take(count)) //too slow, Reverse() takes all elements
        //{
        //    yield return kvp.Value;
        //}
    }

    public IEnumerable<T> Max(int count)
    {
        if (count > this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        foreach (var item in this.byDescending.Take(count))
        {
            yield return item;
        }
    }

    public IEnumerable<T> Min(int count)
    {
        if (count > this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
        foreach (var item in this.byAscending.Take(count))
        {
            yield return item;
        }
    }

    public int RemoveAll(T element)
    {
        this.byAscending.RemoveAllCopies(element);
        this.byDescending.RemoveAllCopies(element);
        int counter = 0;
        foreach (var val in insertionValues[element])
        {
            this.byInsertion.Remove(val);
            counter++;
        }
        this.insertionValues.Remove(element);
        if (this.byInsertion.Count == 0) //somehow that fixes the insertion order of the dict
        {
            this.byInsertion.Clear();
        }
        return counter;
    }
}
