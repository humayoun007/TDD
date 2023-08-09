using EFFIK.Tests.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFIK.Tests.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeeEntity _db = new EmployeeEntity();
        public void CreateNewEmployee(Employee employeeToCreate)
        {
            _db.Employees.Add(employeeToCreate);
            _db.SaveChanges();

        }

        public void DeleteEmployee(int id)
        {
            var conToDel = GetEmployeeById(id);
            _db.Employees.Remove(conToDel);
            _db.SaveChanges();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _db.Employees.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _db.Employees.FirstOrDefault(d => d.EmployeeId == id);
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }
    }
}
