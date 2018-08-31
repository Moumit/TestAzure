using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.BO;

namespace Wpf.Service
{
    public class CountryService
    {
        public bool Insert(Country newCountry)
        {
            try
            {
                TestAzureEntities db = new TestAzureEntities();
                db.Countries.Add(newCountry);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;

        }

        public bool Update(Country updateCountry)
        {
            try
            {
                TestAzureEntities db = new TestAzureEntities();
                var tempEntity = db.Countries.Find(updateCountry.CountryId);
                db.Entry(tempEntity).CurrentValues.SetValues(updateCountry);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IQueryable<Country> GetCountries()
        {
            TestAzureEntities db = new TestAzureEntities();
            return db.Countries;
        }
    }
}
