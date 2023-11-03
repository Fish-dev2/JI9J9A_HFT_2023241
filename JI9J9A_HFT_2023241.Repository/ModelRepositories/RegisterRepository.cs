using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Repository
{
    public class RegisterRepository : Repository<Register>, IRepository<Register>
    {
        public RegisterRepository(GunLicenceDbContext ctx) : base(ctx)
        {
        }

        public override Register Read(int id)
        {
            return this.ctx.Registers.First(t => t.Id == id);
        }

        public override void Update(Register item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            ctx.SaveChanges();
        }
    }
}
