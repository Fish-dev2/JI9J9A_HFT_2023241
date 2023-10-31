using JI9J9A_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace JI9J9A_HFT_2023241.Repository
{
    public class GunLicenceDbContext : DbContext
    {
        public DbSet<Firearm> Firearms { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Ammo> Ammunitions { get; set; }
        public DbSet<Register> Registers { get; set; }

    }
}
