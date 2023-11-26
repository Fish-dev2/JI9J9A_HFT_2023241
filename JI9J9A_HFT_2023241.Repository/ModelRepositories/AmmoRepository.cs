using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Repository
{
    public class AmmoRepository : Repository<Ammo>, IRepository<Ammo>
    {
        public AmmoRepository(GunLicenceDbContext ctx) : base(ctx)
        {

        }

        public override Ammo Read(int id)
        {
            return this.ctx.Ammunitions.First(t => t.AmmoId == id);
        }

        public override void Update(Ammo item)
        {
            var old = Read(item.AmmoId);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    prop.SetValue(old, prop.GetValue(item));
                }
            }
            ctx.SaveChanges();
        }
    }
}
