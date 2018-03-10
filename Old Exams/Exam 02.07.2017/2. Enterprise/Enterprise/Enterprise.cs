using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Enterprise : IEnterprise // 59/60 in judge after some submits
{
    private Dictionary<Guid, Employee> employees = new Dictionary<Guid, Employee>();

    public int Count => this.employees.Count;

    public void Add(Employee employee)
    {
        employees.Add(employee.Id, employee);
    }


    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        return employees.Values.Where(x => x.Position == position && x.Salary >= minSalary);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (!employees.ContainsKey(guid))
        {
            return false;
        }
        Employee emp = employees[guid];
        emp.FirstName = employee.FirstName;
        emp.HireDate = employee.HireDate;
        emp.LastName = employee.LastName;
        emp.Position = employee.Position;
        emp.Salary = employee.Salary;
        return true;
    }

    public bool Contains(Guid guid)
    {
        return employees.ContainsKey(guid);
    }

    public bool Contains(Employee employee)
    {
        return employees.ContainsKey(employee.Id);
    }

    public bool Fire(Guid guid)
    {
        if (!employees.ContainsKey(guid))
        {
            return false;
        }
        employees.Remove(guid);
        return true;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!employees.TryGetValue(guid, out Employee emp))
        {
            throw new ArgumentException();
        }
        return emp;
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        var result = employees.Values.Where(x => x.Position == position);
        if (!result.Any())
        {
            throw new ArgumentException();
        }
        return result;
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var result = employees.Values.Where(x => x.Salary >= minSalary);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        var result = employees.Values.Where(x => x.Position == position && x.Salary == salary);
        if (!result.Any())
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        foreach (var kvp in employees)
        {
            yield return kvp.Value;
        }
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!employees.ContainsKey(guid))
        {
            throw new InvalidOperationException();
        }
        return employees[guid].Position;
    }

    public bool RaiseSalary(int months, int percent)
    {
        bool found = false;
        int year = DateTime.Now.Year;
        int month = DateTime.Now.Month;
        foreach (var emp in employees.Values.Where(x => ((x.HireDate.Year - year) * 12 + x.HireDate.Month - month) <= months))
        {
            emp.Salary += emp.Salary * percent;
            found = true;
        }
        return found;
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        return employees.Values.Where(x => x.FirstName == firstName);
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        return employees.Values.Where(x => x.FirstName == firstName && x.LastName == lastName && x.Position == position);
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        Position[] pos = positions.ToArray();
        return employees.Values.Where(x => pos.Contains(x.Position));
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        return employees.Values.Where(x => x.Salary >= minSalary && x.Salary <= maxSalary);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

