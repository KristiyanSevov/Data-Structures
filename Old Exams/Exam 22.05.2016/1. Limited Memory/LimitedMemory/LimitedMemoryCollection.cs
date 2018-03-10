using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;

namespace LimitedMemory
{
    public class LimitedMemoryCollection<K, V> : ILimitedMemoryCollection<K, V>
    {
        private Dictionary<K, LinkedListNode<Pair<K, V>>> dict = new Dictionary<K, LinkedListNode<Pair<K, V>>>();
        private LinkedList<Pair<K, V>> byUpdateOrder = new LinkedList<Pair<K, V>>();
        private int capacity;

        public LimitedMemoryCollection(int capacity)
        {
            this.capacity = capacity;
        } 

        public IEnumerator<Pair<K, V>> GetEnumerator()
        {
            return byUpdateOrder.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Capacity { get => this.capacity; }

        public int Count { get => this.byUpdateOrder.Count; }

        public void Set(K key, V value)
        {
            var pair = new Pair<K, V>(key, value);
            var node = new LinkedListNode<Pair<K, V>>(pair);
            if (!this.dict.ContainsKey(key))
            {
                this.dict.Add(key, node);
                this.byUpdateOrder.AddFirst(node);
                if (this.Count > this.capacity)
                {
                    var toDelete = this.byUpdateOrder.Last;
                    this.dict.Remove(toDelete.Value.Key);
                    this.byUpdateOrder.RemoveLast();
                }
            }
            else
            {
                this.byUpdateOrder.AddFirst(node);
                var oldNode = this.dict[key];
                this.byUpdateOrder.Remove(oldNode);
                this.dict[key] = node;
            }
        }

        public V Get(K key)
        {
            if (!this.dict.ContainsKey(key))
            {
                throw new KeyNotFoundException();
            }
            var node = this.dict[key];
            this.byUpdateOrder.Remove(node);
            this.byUpdateOrder.AddFirst(node);
            return node.Value.Value;
        }
    }
}
