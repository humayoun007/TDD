using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFIK.Tests.Entity
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department department { get; set; }
    }


    public class Department
    {
        public int DepartmentId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

    }

}
