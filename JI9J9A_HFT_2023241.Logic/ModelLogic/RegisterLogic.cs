using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using JI9J9A_HFT_2023241.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Logic
{
    public class RegisterLogic : IRegisterLogic
    {
        IRepository<Register> repository;

        public RegisterLogic(IRepository<Register> repository)
        {
            this.repository = repository;
        }

        public void Create(Register item)
        {
            //attribute ellenőrzés
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            //id ellenőrzés
            this.repository.Delete(id);

        }

        public Register Read(int id)
        {
            var item = this.repository.Read(id);
            if (item == null)
            {
                throw new ArgumentException(item.GetType().Name + "not exists");
            }

            return item;
        }

        public IEnumerable<Register> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Register item)
        {
            this.repository.Update(item);
        }
        public IEnumerable<LicenceStat> FirearmsAndLicenceTypes()
        {
            var result2 = (from x in this.repository.ReadAll().AsEnumerable()
                          group x by x.Firearm.Name into grouped
                          select new LicenceStat
                          {
                              Firearm = grouped.Key,
                              licenceCounts = grouped.GroupBy(f => f.Owner.LicenceType)
                             .Select(tg => new LicenceStat.LicenceCount
                             {
                                 Type = tg.Key,
                                 Count = tg.Count()
                             })

                          });
            return result2;
        }
        public class LicenceStat
        {
            public string Firearm { get; set; }
            public IEnumerable<LicenceCount> licenceCounts { get; set; }
            public class LicenceCount
            {
                public int Count { get; set; }
                public LicenceType Type { get; set; }
            }

        }



    }
}
