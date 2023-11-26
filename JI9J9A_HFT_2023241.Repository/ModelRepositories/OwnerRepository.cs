using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Repository
{
    public class OwnerRepository : Repository<Owner>, IRepository<Owner>
    {
        public OwnerRepository(GunLicenceDbContext ctx) : base(ctx)
        {
        }

        public override Owner Read(int id)
        {
            return this.ctx.Owners.First(t => t.OwnerId == id);
        }

        public override void Update(Owner item)
        {
            var old = Read(item.OwnerId);
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
