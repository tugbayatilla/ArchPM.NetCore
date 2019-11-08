using System;
using System.Collections.Generic;
// ReSharper disable All

namespace ArchPM.NetCore.Tests.TestModels
{
    public class Company
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public DateTime FoundAt { get; set; }
        public DateTime? Closed { get; set; }
        public bool? IsBig { get; set; }
        public int? LocationCount { get; set; }
        public List<Branch> Branches { get; set; }
        public Branch MainBranch { get; set; }
        public Employee Owner { get; set; }
        public IEnumerable<Employee> SubWorkers { get; set; }
        public List<Employee> MainWorkers { get; set; }
        public Address MainAddress { get; set; }
    }

    public class Branch
    {
        public Guid BranchId { get; set; }
        public uint EmployeeCount { get; set; }
        public decimal AverageSalary { get; set; }
    }

    public class MainBranch : Branch
    {
        public bool HasBuilding { get; set; }
    }

    public class Employee
    {
        public string Name { get; set; }
        public Company Company { get; set; }
    }

    public class Address
    {
        public Address(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
