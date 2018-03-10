using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        for (int i = arr.Length / 2; i >= 0; i--)
        {
            int leftIndex = i * 2 + 1;
            int rightIndex = i * 2 + 2;
            int childIndex;
            if (leftIndex >= arr.Length)
            {
                continue;
            }
            else if (rightIndex >= arr.Length)
            {
                childIndex = leftIndex;
            }
            else
            {
                childIndex = arr[leftIndex].CompareTo(arr[rightIndex]) > 0 ? leftIndex : rightIndex;
            }
            if (arr[i].CompareTo(arr[childIndex]) < 0)
            {
                Swap(arr, i, childIndex);
            }
        }

        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            MaxHeapifyDown(arr, 0, i - 1);
        }
    }

    private static void MaxHeapifyDown(T[] arr, int index, int endIndex)
    {
        while (index <= endIndex / 2)
        {
            int leftIndex = index * 2 + 1;
            int rightIndex = index * 2 + 2;
            int childIndex;
            if (leftIndex > endIndex)
            {
                break;
            }
            else if (rightIndex > endIndex)
            {
                childIndex = leftIndex;
            }
            else
            {
                childIndex = arr[leftIndex].CompareTo(arr[rightIndex]) > 0 ? leftIndex : rightIndex;
            }
            if (arr[index].CompareTo(arr[childIndex]) > 0)
            {
                break;
            }
            Swap(arr, index, childIndex);
            index = childIndex;
        }
    }

    private static void Swap(T[] arr, int index, int parentIndex)
    {
        T temp = arr[parentIndex];
        arr[parentIndex] = arr[index];
        arr[index] = temp;
    }
}
