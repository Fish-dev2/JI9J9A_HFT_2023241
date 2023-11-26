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
    public class AmmoLogic : IAmmoLogic
    {
        IRepository<Ammo> repository;

        public AmmoLogic(IRepository<Ammo> repository)
        {
            this.repository = repository;
        }

        public void Create(Ammo item)
        {
            if (item.Diameter <=0 || item.Length <= 0)
            {
                throw new ArgumentException("Size values cannot be less than or equal to 0.");
            }
            this.repository.Create(item);
        }

        public void Delete(int id)
        {
            this.repository.Delete(id);

        }

        public Ammo Read(int id)
        {
            var item = this.repository.Read(id);
            if (item == null)
            {
                throw new ArgumentException(item.GetType().Name + "not exists");
            }

            return item;
        }

        public IEnumerable<Ammo> ReadAll()
        {
            return this.repository.ReadAll();
        }

        public void Update(Ammo item)
        {
            this.repository.Update(item);
        }

        public IEnumerable<Ammo> Top3MostUsedAmmoTypes()
        {
            var result = (from x in this.repository.ReadAll()
                         orderby x.FirearmsUsingAmmo.Count descending
                         select x).Take(3);
            return result;
        }

    }
}
