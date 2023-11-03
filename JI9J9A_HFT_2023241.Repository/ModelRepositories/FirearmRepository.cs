using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Repository
{
    public class FirearmRepository : Repository<Firearm>, IRepository<Firearm>
    {
        public FirearmRepository(GunLicenceDbContext ctx) : base(ctx)
        {

        }

        public override Firearm Read(int id)
        {
            return this.ctx.Firearms.First(t => t.AmmoId == id);
        }

        public override void Update(Firearm item)
        {
            var old = Read(item.GunId);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
