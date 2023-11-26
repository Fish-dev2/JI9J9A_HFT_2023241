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
    public class FirearmLogic : IFirearmLogic
    {
        IRepository<Firearm> repository;

        public FirearmLogic(IRepository<Firearm> repository)
        {
            this.repository = repository;
        }

        public void Create(Firearm item)
        {
            if (item.AmmoId < 0)
            {
                throw new ArgumentException("AmmoId cannot be less than 0.");
            }
            else if (item.FireRate <= 0) 
            {
                throw new ArgumentException("Fire rate cannot be less than or equal to 0.");
            }
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);

        }

        public Firearm Read(int id)
        {
            var item = this.repository.Read(id);
            if (item == null)
            {
                throw new ArgumentException(item.GetType().Name + "not exists");
            }

            return item;
        }

        public IEnumerable<Firearm> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Firearm item)
        {
            this.repository.Update(item);
        }
        public IEnumerable<Firearm> FirearmsUsingSpecifiedAmmo(Ammo ammo)
        {
            var result = from x in this.repository.ReadAll()
                         where x.AmmoId == ammo.AmmoId
                         select x;

            return result;
        }
    }
}
