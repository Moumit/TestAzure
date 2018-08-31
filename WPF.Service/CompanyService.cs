using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.BO;

namespace Wpf.Service
{
   public class CompanyService
    {
        public bool Insert(Company newCompany)
        {
            try
            {
                TestAzureEntities db = new TestAzureEntities();
                db.Companies.Add(newCompany);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        public bool Update(Company updateCompany)
        {
            try
            {
                TestAzureEntities db = new TestAzureEntities();
                var tempEntity = db.Companies.Find(updateCompany.CompanyId);
                db.Entry(tempEntity).CurrentValues.SetValues(updateCompany);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IQueryable<Company> GetCompanies()
        {
            TestAzureEntities db = new TestAzureEntities();
            return db.Companies;
        }
    }
}
