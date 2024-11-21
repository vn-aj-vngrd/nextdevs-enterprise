using Backend.Domain.Employees.Entities;

namespace Backend.Domain.Employees.DTOs
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
        }

        public EmployeeDto(Employee employee)
        {
            Id = employee.Id;
            Name = employee.Name;
            Salary = employee.Salary;
            Email = employee.Email;
            Address = employee.Address;
            PhoneNumber = employee.PhoneNumber;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}