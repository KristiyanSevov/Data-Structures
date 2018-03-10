namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Hierarchy<T> : IHierarchy<T>
    {
        class Node<T>
        {
            public T Value { get; set; }
            public List<Node<T>> Children { get; set; }

            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node<T>>();
            }
        }

        private Node<T> root;
        Dictionary<T, Node<T>> elements;
        Dictionary<T, Node<T>> parents;

        public Hierarchy(T root)
        {
            Node<T> node = new Node<T>(root);
            this.root = node;
            this.elements = new Dictionary<T, Node<T>>();
            this.parents = new Dictionary<T, Node<T>>();
            this.elements.Add(root, node);
            this.parents.Add(root, null);
        }

        public int Count
        {
            get
            {
                return this.parents.Count;
            }
        }

        public void Add(T element, T child)
        {
            if (!this.elements.ContainsKey(element) || this.elements.ContainsKey(child))
            {
                throw new ArgumentException();
            }
            Node<T> node = new Node<T>(child);
            elements.Add(child, node);
            elements[element].Children.Add(node);
            parents.Add(child, elements[element]);
        }

        public void Remove(T element)
        {
            if (!elements.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            Node<T> node = elements[element];
            if (node == this.root)
            {
                throw new InvalidOperationException();
            }
            Node<T> parent = parents[element];
            parent.Children.Remove(node);
            foreach (var child in node.Children)
            {
                parent.Children.Add(child);
                parents[child.Value] = parent;
            }
            elements.Remove(element);
            parents.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!elements.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            Node<T> node = elements[item];
            foreach (var child in node.Children)
            {
                yield return child.Value;
            }
        }

        public T GetParent(T item)
        {
            if (!parents.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            Node<T> node = parents[item];
            if (node == null)
            {
                return default(T);
            }
            return node.Value;
        }

        public bool Contains(T value)
        {
            return elements.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var kvp in elements)
            {
                if (other.Contains(kvp.Key))
                {
                    yield return kvp.Key;
                }
            }
        } 

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node<T>> queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);
            while (queue.Count > 0)
            {
                Node<T> node = queue.Dequeue();
                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}