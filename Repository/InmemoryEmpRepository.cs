using EFFIK.Tests.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFFIK.Tests.Repository
{
    public class InmemoryEmpRepository : IEmployeeRepository
    {
        private List<Employee> _db = new List<Employee>();

        public Exception ExceptionToThrow { get; set; }

        public void CreateNewEmployee(Employee employeeToCreate)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(employeeToCreate);
        }

        public void DeleteEmployee(int id)
        {
            _db.Remove(GetEmployeeById(id));
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _db.ToList();
        }

        public Employee GetEmployeeById(int id)
        {
            return _db.FirstOrDefault(d => d.EmployeeId == id);
        }

        public int SaveChanges()
        {
            return 1;
        }

        public void SaveChanges(Employee employeeToUpdate)
        {

            foreach (Employee employee in _db)
            {
                if (employee.EmployeeId == employeeToUpdate.EmployeeId)
                {
                    _db.Remove(employee);
                    _db.Add(employeeToUpdate);
                    break;
                }
            }
        }

        public void Add(Employee employeeToAdd)
        {
            _db.Add(employeeToAdd);
        }
    }
}
