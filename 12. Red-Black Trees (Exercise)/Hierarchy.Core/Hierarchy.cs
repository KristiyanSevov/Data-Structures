namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Dictionary<T, Node> nodes = new Dictionary<T, Node>();
        private Dictionary<T, Node> parents = new Dictionary<T, Node>();
        private Node root;

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            nodes.Add(root, this.root);
            parents.Add(root, null);
        }

        public int Count
        {
            get
            {
                return nodes.Count;
            }
        }

        public void Add(T element, T child)
        {
            if (!nodes.ContainsKey(element) || nodes.ContainsKey(child))
            {
                throw new ArgumentException();
            }
            Node parent = nodes[element];
            Node newNode = new Node(child);
            parent.Children.Add(newNode);
            nodes[child] = newNode;
            parents[child] = parent;
        }

        public void Remove(T element)
        {
            if (!nodes.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            if (parents[element] == null)
            {
                throw new InvalidOperationException();
            }
            Node toDelete = nodes[element];
            Node parent = parents[element];
            parent.Children.Remove(toDelete);
            parent.Children.AddRange(toDelete.Children);
            foreach (var child in toDelete.Children)
            {
                parents[child.Value] = parent;
            }
            nodes.Remove(element);
            parents.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!nodes.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            return nodes[item].Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!nodes.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            return parents[item] != null ? parents[item].Value : default(T);
        }

        public bool Contains(T value)
        {
            return nodes.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            foreach (var item in this)
            {
                if (other.Contains(item))
                {
                    yield return item;
                }
            }
        } 

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(this.root);
            while (queue.Count != 0)
            {
                Node node = queue.Dequeue();
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

        private class Node
        {
            public T Value { get; set; }
            public List<Node> Children { get; set; }

            public Node(T value)
            {
                this.Value = value;
                this.Children = new List<Node>();
            }
        }
    }
}