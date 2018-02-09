using System;
using System.Collections.Generic;

//It's not necessary to check gCosts of visited nodes here as our heursitic is consistent, i.e.
//Manhattan distance is consistent on a maze with movement cost 1 and horizontal/vertical moves
public class AStar 
{
    private PriorityQueue<Node> queue = new PriorityQueue<Node>();
    private Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
    private Dictionary<Node, int> gCosts = new Dictionary<Node, int>(); 
    private char[,] map;

    public AStar(char[,] map)
    {
        this.map = map;
    }

    public static int GetH(Node current, Node goal)
    {
        return Math.Abs(goal.Row - current.Row) + Math.Abs(goal.Col - current.Col);
    }

    public IEnumerable<Node> GetPath(Node start, Node goal)
    {
        var cacheKey = Tuple.Create(start, goal);
        Stack<Node> path = new Stack<Node>();
        Node current = FindPath(start, goal);
        path.Push(current);
        while (parents[current] != null)
        {
            current = parents[current];
            path.Push(current);
        }
        this.queue = new PriorityQueue<Node>();
        this.parents.Clear();
        this.gCosts.Clear();
        return path;
    }

    private Node FindPath(Node start, Node goal)
    {
        start.F = 1 + GetH(start, goal);
        this.gCosts.Add(start, 0);
        this.parents.Add(start, null);
        this.queue.Enqueue(start);
        while (this.queue.Count != 0)
        {
            Node node = this.queue.Dequeue();
            if (node.Row == goal.Row && node.Col == goal.Col)
            {
                return node;
            }
            AddNeighbour(node.Row + 1, node.Col, node, goal);
            AddNeighbour(node.Row - 1, node.Col, node, goal);
            AddNeighbour(node.Row, node.Col + 1, node, goal);
            AddNeighbour(node.Row, node.Col - 1, node, goal);
        }
        return start; 
    }

    private void AddNeighbour(int row, int col, Node parent, Node goal)
    {
        if (row >= 0 && row < this.map.GetLength(0) &&
            col >= 0 && col < this.map.GetLength(1) && 
            map[row, col] != 'W')
        {
            Node node = new Node(row, col);
            int gCost = this.gCosts[parent] + 1;
            node.F = this.gCosts[parent] + 1 + GetH(node, goal);
            if (!this.gCosts.ContainsKey(node) || gCost < this.gCosts[node]) 
            {
                    this.queue.Enqueue(node);
                    this.parents[node] = parent;
                    this.gCosts[node] = gCost;
            }     
        }
    }
}