using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> peopleByEmail = new Dictionary<string, Person>();
    private Dictionary<string, SortedSet<Person>> peopleByDomain = new Dictionary<string, SortedSet<Person>>();
    private Dictionary<string, SortedSet<Person>> peopleByNameAndTown 
        = new Dictionary<string, SortedSet<Person>>();
    private OrderedMultiDictionary<int, Person> peopleByAge = new OrderedMultiDictionary<int, Person>(true);
    private Dictionary<string, OrderedMultiDictionary<int, Person>> peopleByTownAndAge =
        new Dictionary<string, OrderedMultiDictionary<int, Person>>();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (peopleByEmail.ContainsKey(email))
        {
            return false;
        }
        Person person = new Person(email, name, age, town);
        peopleByEmail.Add(email, person);
        string domain = email.Substring(email.IndexOf('@')+1);
        if (!peopleByDomain.ContainsKey(domain))
        {
            peopleByDomain.Add(domain, new SortedSet<Person>());
        }
        peopleByDomain[domain].Add(person);
        string nameAndTown = name + town;
        if (!peopleByNameAndTown.ContainsKey(nameAndTown))
        {
            peopleByNameAndTown.Add(nameAndTown, new SortedSet<Person>());
        }
        peopleByNameAndTown[nameAndTown].Add(person);
        peopleByAge.Add(age, person);
        if (!peopleByTownAndAge.ContainsKey(town))
        {
            peopleByTownAndAge.Add(town, new OrderedMultiDictionary<int, Person>(true));
        }
        peopleByTownAndAge[town].Add(age, person);
        return true;

    }

    public int Count
    {
        get
        {
            return peopleByEmail.Count;
        }
    }

    public Person FindPerson(string email)
    {
        if (!peopleByEmail.ContainsKey(email))
        {
            return null;
        }
        return this.peopleByEmail[email];
    }

    public bool DeletePerson(string email)
    {
        if (!peopleByEmail.ContainsKey(email))
        {
            return false;
        }
        Person person = peopleByEmail[email];
        peopleByEmail.Remove(email);
        string domain = person.Email.Substring(person.Email.IndexOf('@') + 1);
        peopleByDomain[domain].Remove(person);
        peopleByNameAndTown[person.Name + person.Town].Remove(person);
        peopleByAge[person.Age].Remove(person);
        peopleByTownAndAge[person.Town][person.Age].Remove(person);
        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (peopleByDomain.ContainsKey(emailDomain))
        {
            return peopleByDomain[emailDomain];
        }
        return Enumerable.Empty<Person>();
        
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        if (peopleByNameAndTown.ContainsKey(name+town))
        {
            return peopleByNameAndTown[name+town];
        }
        return Enumerable.Empty<Person>();
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return peopleByAge.Range(startAge, true, endAge, true).Values;
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (peopleByTownAndAge.ContainsKey(town))
        {
            return peopleByTownAndAge[town].Range(startAge, true, endAge, true).Values;
        }
        return Enumerable.Empty<Person>();   
    }
}
