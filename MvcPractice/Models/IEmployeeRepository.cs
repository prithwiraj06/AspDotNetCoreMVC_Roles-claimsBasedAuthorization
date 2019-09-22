using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.Models
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);
        IEnumerable<Employee> GetAllEmployees();
        Employee CreateEmployee(Employee employee);
        Employee Update(Employee employeeChanges);
        Employee Delete(int id);
    }
}
