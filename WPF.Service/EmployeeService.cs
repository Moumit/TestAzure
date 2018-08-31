using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.BO;

namespace Wpf.Service
{
    public class EmployeeService
    {
        public bool Insert(Employee newEmployee)
        {
            try
            {
                TestAzureEntities db = new TestAzureEntities();
                db.Employees.Add(newEmployee);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
           
        }

        public bool Update(Employee updateEmployee)
        {
            try
            {
                TestAzureEntities db = new TestAzureEntities();
                var tempEntity = db.Employees.Find(updateEmployee.EmployeeId); 
                db.Entry(tempEntity).CurrentValues.SetValues(updateEmployee);                 
                db.SaveChanges();                
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IQueryable<Employee> GetEmployees()
        {
            TestAzureEntities db = new TestAzureEntities();
            return db.Employees;
        }
    }
}
