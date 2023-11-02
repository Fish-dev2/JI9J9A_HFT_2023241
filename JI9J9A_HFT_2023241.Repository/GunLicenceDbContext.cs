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

        public GunLicenceDbContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                string conn = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                AttachDbFilename=|DataDirectory|\licences.mdf;
                                Integrated Security=True;MultipleActiveResultSets=true";
                builder
                //.UseSqlServer(conn)
                .UseInMemoryDatabase("licenses")
                .UseLazyLoadingProxies();

            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Firearm>(
                fa => fa.HasOne(fa => fa.AmmoType)
                .WithMany(ammo => ammo.FirearmsUsingAmmo)
                .HasForeignKey(fa => fa.AmmoId)
                .OnDelete(DeleteBehavior.NoAction));

            modelBuilder.Entity<Firearm>()
                .HasMany(f => f.OwnersHavingThisGun)
                .WithMany(o => o.LicensedGuns)
                .UsingEntity<Register>(
                t => t.HasOne(t => t.Owner)
                .WithMany().HasForeignKey(t => t.OwnerId).OnDelete(DeleteBehavior.Cascade),
                t => t.HasOne(t => t.Firearm)
                .WithMany().HasForeignKey(t => t.FirearmId).OnDelete(DeleteBehavior.Cascade));


            //létrehozza a kapcsolatot hogy az Ownernek legyen Registers összekötése
            //és hogy a Registersnek Idegen kulcsa az OwnerId
            modelBuilder.Entity<Register>()
                .HasOne( r => r.Owner)
                .WithMany(o => o.Registers)
                .HasForeignKey(r => r.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Register>()
                .HasOne(r => r.Firearm)
                .WithMany(f => f.Registers)
                .HasForeignKey(r => r.FirearmId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Ammo>().HasData(new Ammo[]
            {
                new Ammo("1#9MM#9#5#HollowPoint"),
                new Ammo("2#762#9#5#FullMetalJacket")
            });

            modelBuilder.Entity<Firearm>().HasData(new Firearm[]
            {
                new Firearm("1#MP5#Heckler&Koch#800#2000.01.01#1"),
                new Firearm("2#UZI#Heckler&Koch#800#2000.01.01#1"),
                new Firearm("3#AK47#USSR#800#2000.01.01#2"),
                new Firearm("4#AK63D#USSR#800#2000.01.01#2")
            });


            modelBuilder.Entity<Owner>().HasData(new Owner[]
            {
                new Owner("1#Bálint#Füzi#2024.01.01.#SelfDefense"),
                new Owner("2#Cintia#Kincses#2023.12.31.#Hunting"),
            });
            modelBuilder.Entity<Register>().HasData(new Register[]
            {
                new Register("1#1#1#2022.01.01"),
                new Register("2#2#1#2020.01.01"),
                new Register("3#1#2#2020.01.01"),
                new Register("4#2#2#2020.01.01"),
            });





            /*
             1#MP5#Heckler&Koch#800#2000.01.01#1
2#UZI#Heckler&Koch#800#2000.01.01#1
3#AK47#USSR#800#2000.01.01#2
4#AK63D#USSR#800#2000.01.01#2

1#9MM#9#5#HollowPoint
2#762#9#5#FullMetalJacket
            1#Bálint#Füzi#2024.01.01.#SelfDefense
                2#Cintia#Kincses#2023.12.31.#Hunting

            1#1#1#2022.01.01
2#1#1#2020.01.01
2#1#2#2020.01.01
2#1#2#2020.01.01
             */
        }

    }
}
