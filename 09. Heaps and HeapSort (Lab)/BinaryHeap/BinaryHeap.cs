using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.MaxHeapifyUp(this.heap.Count - 1);
    }

    private void MaxHeapifyUp(int index)
    {
        while (index != 0)
        {
            int parentIndex = (index - 1) / 2;
            if (this.heap[index].CompareTo(this.heap[parentIndex]) < 0)
            {
                break;
            }
            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void Swap(int index, int parentIndex)
    {
        T temp = this.heap[parentIndex];
        this.heap[parentIndex] = this.heap[index];
        this.heap[index] = temp;
    }

    public T Peek()
    {
        if (this.heap.Count == 0)
        {
            throw new InvalidOperationException("heap is empty"); 
        }
        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count == 0)
        {
            throw new InvalidOperationException("heap is empty");
        }
        T result = this.heap[0];
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.MaxHeapifyDown(0);
        return result;
    }

    private void MaxHeapifyDown(int index)
    {
        while (index <= this.heap.Count / 2)
        {
            int leftIndex = index * 2 + 1;
            int rightIndex = index * 2 + 2;
            int childIndex;
            if (leftIndex >= this.Count)
            {
                break;
            }
            else if (rightIndex >= this.Count)
            {
                childIndex = leftIndex;
            }
            else
            {
                childIndex = this.heap[leftIndex].CompareTo(this.heap[rightIndex]) > 0 ? leftIndex : rightIndex;
            }
            if (this.heap[index].CompareTo(this.heap[childIndex]) > 0)
            {
                break;
            }
            Swap(index, childIndex);
            index = childIndex;
        }
    }
}
