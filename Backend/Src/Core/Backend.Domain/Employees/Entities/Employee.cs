using Backend.Domain.Common;

namespace Backend.Domain.Employees.Entities
{
    public class Employee : AuditableBaseEntity
    {
        private Employee()
        {
        }

        public Employee(string name, double salary, string email, string address, string phoneNumber)
        {
            Name = name;
            Salary = salary;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public string Name { get; private set; }
        public double Salary { get; private set; }
        public string Email { get; private set; }
        public string Address { get; private set; }
        public string PhoneNumber { get; private set; }

        public void Update(string name, double salary, string email, string address, string phoneNumber)
        {
            Name = name;
            Salary = salary;
            Email = email;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}