using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using JI9J9A_HFT_2023241.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;


namespace JI9J9A_HFT_2023241.Logic
{
    public class OwnerLogic : IOwnerLogic
    {
        IRepository<Owner> repository;

        public OwnerLogic(IRepository<Owner> repository)
        {
            this.repository = repository;
        }

        public void Create(Owner item)
        {
            if (item.FirstName.Length < 3 || item.LastName.Length < 3)
            {
                throw new ArgumentException("Name cannot be shorter than 3 characters.");
            }
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);

        }

        public Owner Read(int id)
        {
            var item = this.repository.Read(id);
            if (item == null)
            {
                throw new ArgumentException(item.GetType().Name + "not exists");
            }

            return item;
        }

        public IEnumerable<Owner> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Owner item)
        {
            this.repository.Update(item);
        }

        public IEnumerable<Owner> ExpiredLicences()
        {
            var result = from x in this.repository.ReadAll()
                         where x.LicenceValidUntil < DateTime.Now
                         select x;

            return result;
        }
        public double AverageAmountOfGuns()
        {
            var result = (from x in this.repository.ReadAll()
                          select x.LicensedGuns.Count).Average();
            return result;
        }
        public IEnumerable<LicenceInfo> AmountOfEachLicenceGivenOut()
        {
            var result = from x in this.repository.ReadAll()
                         group x by x.LicenceType into licenceInfo
                         orderby licenceInfo.Count()
                         select new LicenceInfo { LicenceType = licenceInfo.Key, Count = licenceInfo.Count() };
            return result;
        }



    }
}
