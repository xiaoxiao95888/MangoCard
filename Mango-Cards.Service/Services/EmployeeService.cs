using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mango_Cards.Library.Models;
using Mango_Cards.Library.Services;

namespace Mango_Cards.Service.Services
{
    public class EmployeeService : BaseService, IEmployeeService
    {
        public EmployeeService(MangoCardsDataContext dbContext)
            : base(dbContext)
        {
        }

        public Employee GetEmployee(Guid id)
        {
            return DbContext.Employees.FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<Employee> GetEmployees()
        {
            return DbContext.Employees;
        }

        public void Insert(Employee employee)
        {
            DbContext.Employees.Add(employee);
            DbContext.SaveChanges();
        }

        public void Update()
        {
            DbContext.SaveChanges();
        }
    }
}
