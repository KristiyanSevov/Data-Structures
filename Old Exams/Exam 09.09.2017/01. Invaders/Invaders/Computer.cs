using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Computer : IComputer
{
    private int energy;
    private OrderedBag<LinkedListNode<Invader>> invadersByPriority =
        new OrderedBag<LinkedListNode<Invader>>((x, y) => x.Value.CompareTo(y.Value));
    private LinkedList<Invader> invadersByInsertion = new LinkedList<Invader>();

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }
        this.energy = energy;
    }

    public int Energy
    {
        get
        {
            return this.energy > 0 ? this.energy : 0;
        }
    }

    public void Skip(int turns)
    {
        var itemsToRemove = new List<LinkedListNode<Invader>>();
        foreach (var node in this.invadersByPriority)
        {
            Invader invader = node.Value;
            invader.Distance -= turns;
            if (invader.Distance <= 0) 
            {
                if (this.energy >= 0) //could overflow if always subtracting
                {
                    this.energy -= invader.Damage;
                }
                itemsToRemove.Add(node);
                invadersByInsertion.Remove(node);
            }
        }
        this.invadersByPriority.RemoveMany(itemsToRemove);

    }

    public void AddInvader(Invader invader)
    {
        LinkedListNode<Invader> node = new LinkedListNode<Invader>(invader);
        this.invadersByInsertion.AddLast(node);
        this.invadersByPriority.Add(node);
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        foreach (var node in invadersByPriority.Take(count).ToList())
        {
            invadersByInsertion.Remove(node);
            this.invadersByPriority.Remove(node);
        }
    }

    public void DestroyTargetsInRadius(int radius)
    {
        var itemsToRemove = invadersByPriority.RangeTo(new LinkedListNode<Invader>(new Invader(int.MinValue, radius)), true).ToList();
        foreach (var node in itemsToRemove)
        {
            invadersByInsertion.Remove(node);
            this.invadersByPriority.Remove(node);
        }
    }

    public IEnumerable<Invader> Invaders()
    {
        return this.invadersByInsertion;
    }
}
