using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Chainblock : IChainblock
{
    private Dictionary<int, Transaction> transactions = new Dictionary<int, Transaction>();
    public int Count => transactions.Count;

    public void Add(Transaction tx)
    {
        if (!transactions.ContainsKey(tx.Id))
        {
            transactions.Add(tx.Id, tx);
        }
        transactions[tx.Id] = tx;
    }

    public void ChangeTransactionStatus(int id, TransactionStatus newStatus)
    {
        if (!transactions.ContainsKey(id))
        {
            throw new ArgumentException();
        }
        transactions[id].Status = newStatus;
    }

    public bool Contains(Transaction tx)
    {
        return transactions.ContainsKey(tx.Id);
    }

    public bool Contains(int id)
    {
        return transactions.ContainsKey(id);
    }

    public IEnumerable<Transaction> GetAllInAmountRange(double lo, double hi)
    {
        return transactions.Values.Where(x => x.Amount >= lo && x.Amount <= hi);
    }

    public IEnumerable<Transaction> GetAllOrderedByAmountDescendingThenById()
    {
        return transactions.Values.OrderByDescending(x => x.Amount).ThenBy(x => x.Id);
    }

    public IEnumerable<string> GetAllReceiversWithTransactionStatus(TransactionStatus status)
    {
        var result = transactions.Values
            .Where(x => x.Status == status)
            .OrderByDescending(x => x.Amount)
            .Select(x => x.To);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();

    }

    public IEnumerable<string> GetAllSendersWithTransactionStatus(TransactionStatus status)
    {
        var result = transactions.Values
    .Where(x => x.Status == status)
    .OrderByDescending(x => x.Amount)
    .Select(x => x.From);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public Transaction GetById(int id)
    {
        if (transactions.ContainsKey(id))
        {
            return transactions[id];
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<Transaction> GetByReceiverAndAmountRange(string receiver, double lo, double hi)
    {
        var result = transactions.Values
            .Where(x => x.To == receiver && x.Amount >= lo && x.Amount < hi)
            .OrderByDescending(x => x.Amount)
            .ThenBy(x => x.Id);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<Transaction> GetByReceiverOrderedByAmountThenById(string receiver)
    {
        var result = transactions.Values
            .Where(x => x.To == receiver)
            .OrderByDescending(x => x.Amount)
            .ThenBy(x => x.Id);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<Transaction> GetBySenderAndMinimumAmountDescending(string sender, double amount)
    {
        var result = transactions.Values
            .Where(x => x.From == sender && x.Amount > amount)
            .OrderByDescending(x => x.Amount);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<Transaction> GetBySenderOrderedByAmountDescending(string sender)
    {
        var result = transactions.Values
            .Where(x => x.From == sender)
            .OrderByDescending(x => x.Amount);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<Transaction> GetByTransactionStatus(TransactionStatus status)
    {
        var result = transactions.Values
            .Where(x => x.Status == status)
            .OrderByDescending(x => x.Amount);
        if (result.Any())
        {
            return result;
        }
        throw new InvalidOperationException();
    }

    public IEnumerable<Transaction> GetByTransactionStatusAndMaximumAmount(TransactionStatus status, double amount)
    {
        var result = transactions.Values
            .Where(x => x.Status == status && x.Amount <= amount)
            .OrderByDescending(x => x.Amount);
        if (result.Any())
        {
            return result;
        }
        return Enumerable.Empty<Transaction>();
    }

    public IEnumerator<Transaction> GetEnumerator()
    {
        return transactions.Values.GetEnumerator();
    }

    public void RemoveTransactionById(int id)
    {
        if (!transactions.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }
        transactions.Remove(id);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

