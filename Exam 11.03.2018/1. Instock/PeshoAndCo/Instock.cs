using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    private Dictionary<string, Product> products = new Dictionary<string, Product>();
    private List<Product> productsByIndex = new List<Product>();
    OrderedBag<Product> productsByPrice = new OrderedBag<Product>((x, y) => y.Price.CompareTo(x.Price));
    Dictionary<int, List<Product>> productsByQuantity = new Dictionary<int, List<Product>>();
    OrderedSet<string> byOrder = new OrderedSet<string>();

    public int Count => productsByIndex.Count;

    public void Add(Product product)
    {
        products.Add(product.Label, product);
        productsByIndex.Add(product);
        productsByPrice.Add(product);
        byOrder.Add(product.Label);
        if (!productsByQuantity.ContainsKey(product.Quantity))
        {
            productsByQuantity.Add(product.Quantity, new List<Product>());
        }
        productsByQuantity[product.Quantity].Add(product);
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!products.ContainsKey(product))
        {
            throw new ArgumentException();
        }
        Product p = products[product];
        int oldQ = p.Quantity;
        products[product].Quantity = quantity;
        productsByQuantity[oldQ].Remove(p);
        if (!productsByQuantity.ContainsKey(quantity))
        {
            productsByQuantity.Add(quantity, new List<Product>());
        }
        productsByQuantity[quantity].Add(p);
    }

    public bool Contains(Product product)
    {
        return products.ContainsKey(product.Label);
    }

    public Product Find(int index)
    {
        if (index < 0 || index >= productsByIndex.Count)
        {
            throw new IndexOutOfRangeException();
        }
        return productsByIndex[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        var result = productsByPrice.Range(new Product("s", price, 0), true, new Product("j", price, 0), true);
        if (result.Any())
        {
            return result;
        }
        return Enumerable.Empty<Product>();
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        if (!productsByQuantity.ContainsKey(quantity))
        {
            return Enumerable.Empty<Product>();
        }
        return productsByQuantity[quantity];
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        var result = productsByPrice.Range(new Product("s", hi, 0), true, new Product("j", lo, 0), false);
        if (result.Any())
        {
            return result;
        }
        return Enumerable.Empty<Product>();
    }

    public Product FindByLabel(string label)
    {
        if (!products.ContainsKey(label))
        {
            throw new ArgumentException();
        }
        return products[label];
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count > productsByIndex.Count)
        {
            throw new ArgumentException();
        }
        return byOrder.Take(count).Select(x => products[x]);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (count > productsByIndex.Count)
        {
            throw new ArgumentException();
        }
        return productsByPrice.Take(count);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        return productsByIndex.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
