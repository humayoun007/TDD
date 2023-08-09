using EFFIK.Tests.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFIK.Tests
{
    public class EmployeeEntity : DbContext
    {
        public EmployeeEntity():base() { }

        public DbSet<Employee> Employees { get; set; }  

        public DbSet<Department> Departments { get; set; }
        

    }
}
