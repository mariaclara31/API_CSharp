using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace RestTest.Models
{
    //Data Model to match the database
    public class EmployeeContext: DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
    }

    public class Employee 
    { 
        public long ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Position { get; set; }
        public float Salary { get; set; }
    }
}
