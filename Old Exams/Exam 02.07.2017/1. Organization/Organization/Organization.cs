using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    private MultiDictionary<string, Person> peopleByName = new MultiDictionary<string, Person>(true);
    private List<Person> peopleByInsertion = new List<Person>();
    private OrderedDictionary<int, List<Person>> peopleByNameLength =
        new OrderedDictionary<int, List<Person>>();


    public IEnumerator<Person> GetEnumerator()
    {
        return this.peopleByInsertion.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count { get => this.peopleByInsertion.Count; }
    public bool Contains(Person person)
    {
        if (peopleByName.ContainsKey(person.Name))
        {
            return peopleByName[person.Name].Contains(person);
        }
        return false;
    }

    public bool ContainsByName(string name)
    {
        if (peopleByName.ContainsKey(name))
        {
            return true;
        }
        return false;
    }

    public void Add(Person person)
    {
        peopleByName.Add(person.Name, person);
        if (!peopleByNameLength.ContainsKey(person.Name.Length))
        {
            peopleByNameLength.Add(person.Name.Length, new List<Person>());
        }
        peopleByNameLength[person.Name.Length].Add(person);
        peopleByInsertion.Add(person);
    }

    public Person GetAtIndex(int index)
    {
        if (index < 0 || index > this.Count - 1)
        {
            throw new IndexOutOfRangeException();
        }
        return peopleByInsertion[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        return peopleByName[name];
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        return peopleByInsertion.Take(count);
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        foreach (var kvp in peopleByNameLength.Range(minLength, true, maxLength, true))
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        if (!peopleByNameLength.ContainsKey(length))
        {
            throw new ArgumentException();
        }
        return peopleByNameLength[length];
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.peopleByInsertion;
    }
}