using EFFIK.Tests.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFIK.Tests.Repository
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();
        void CreateNewEmployee(Employee employeeToCreate);

        void DeleteEmployee(int id);

        Employee GetEmployeeById(int id);

        int SaveChanges();
    }
}
