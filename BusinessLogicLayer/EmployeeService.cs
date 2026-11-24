using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            if (employee.Salary <= - 250_000)
            {
                await _employeeRepository.AddEmployeeAsync(employee);
            }
            else
            {
                var employeeToBeAdded = new Employee()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Position = employee.Position,
                    Salary = employee.Salary,
                };

                await _employeeRepository.AddEmployeeAsync(employee);
            }
        }

        public async Task GetDetailsAsync (Employee employee)
        {
            await _employeeRepository.GetDetailsAsync(employee);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            //business logic would be written here 
            return await _employeeRepository.GetEmployeesAsync();
        }
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }
}
