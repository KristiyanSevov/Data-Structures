using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int DefaultCapacity = 16;
    private const double LoadFactor = 0.75;
    private LinkedList<KeyValue<TKey, TValue>>[] table;

    public int Count { get; private set; }

    public int Capacity
    {
        get
        {
            return this.table.Length;
        }
    }

    public HashTable() : this(DefaultCapacity)
    { }

    public HashTable(int capacity)
    {
        this.table = new LinkedList<KeyValue<TKey, TValue>>[capacity];
    }

    public void Add(TKey key, TValue value)
    {
        if (this.ContainsKey(key))
        {
            throw new ArgumentException("Duplicate key");
        }
        this.GrowIfNeeded();
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;
        KeyValue<TKey, TValue> pair = new KeyValue<TKey, TValue>(key, value);
        if (table[index] == null)
        {
            table[index] = new LinkedList<KeyValue<TKey, TValue>>();
        }
        this.table[index].AddLast(pair);
        this.Count++;
    }

    private void GrowIfNeeded()
    {
        if ((this.Count + 1) / (double)this.Capacity > LoadFactor)
        {
            var grownTable = new LinkedList<KeyValue<TKey, TValue>>[this.Capacity * 2];
            foreach (var node in this.table.Where(x => x != null))
            {
                foreach (var item in node)
                {
                    int index = Math.Abs(item.Key.GetHashCode()) % grownTable.Length;
                    if (grownTable[index] == null)
                    {
                        grownTable[index] = new LinkedList<KeyValue<TKey, TValue>>();
                    }
                    grownTable[index].AddLast(item);
                }
            }
            this.table = grownTable;
        }
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        if (!this.ContainsKey(key))
        {
            this.Add(key, value);
            return true;
        }
        var pair = this.Find(key);
        pair.Value = value;
        return false;
    }

    public TValue Get(TKey key)
    {
        var pair = this.Find(key);
        if (pair == null)
        {
            throw new KeyNotFoundException();
        }
        return pair.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return this.Get(key);
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        try
        {
            value = this.Get(key);
            return true;
        }
        catch (KeyNotFoundException)
        {
            value = default(TValue);
            return false;
        }
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;
        if (this.table[index] != null)
        {
            foreach (var item in this.table[index])
            {
                if (item.Key.Equals(key))
                {
                    return item;
                }
            }
        }
        return null;
    }

    public bool ContainsKey(TKey key)
    {
        try
        {
            this.Get(key);
            return true;
        }
        catch (KeyNotFoundException)
        {
            return false;
        }
    }

    public bool Remove(TKey key)
    {
        int index = Math.Abs(key.GetHashCode()) % this.Capacity;
        var pair = this.Find(key);
        if (pair != null)
        {
            this.table[index].Remove(pair);
            this.Count--;
            return true;
        }
        return false;
    }

    public void Clear()
    {
        this.table = new LinkedList<KeyValue<TKey, TValue>>[this.Capacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            foreach (var item in this)
            {
                yield return item.Key;
            }
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            foreach (var item in this)
            {
                yield return item.Value;
            }
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var node in this.table.Where(x => x != null))
        {
            foreach (var item in node)
            {
                yield return item;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
