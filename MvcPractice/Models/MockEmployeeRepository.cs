using MvcPractice.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcPractice.Models
{
    public class MockEmployeeRepository: IEmployeeRepository
    {
        List<Employee> _employeeList;
        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee() {Id = 1, Name = "Prithwi", Email = "prethwiraj06@gmail.com", Department = Dept.IT} ,
                new Employee() {Id = 2, Name = "Shiv", Email = "prethwiraj06@gmail.com", Department = Dept.HR},
                new Employee() {Id = 3, Name = "Dilip", Email = "prethwiraj06@gmail.com", Department = Dept.Admin}
            };
        }
        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(e => e.Id == id);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }
        public Employee CreateEmployee(Employee employee)
        {
            employee.Id = _employeeList.Max(e => e.Id) + 1;
            _employeeList.Add(employee);
            return employee;
        }

        public Employee Update(Employee employeeChanges)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == employeeChanges.Id);
            if(employee != null)
            {
                employee.Name = employeeChanges.Name;
                employee.Email = employeeChanges.Email;
                employee.Department = employeeChanges.Department;
            }
            return employee;

        }

        public Employee Delete(int id)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == id);
            if(employee != null)
            {
                _employeeList.Remove(employee);
            }
            return employee;
        }
    }
}
