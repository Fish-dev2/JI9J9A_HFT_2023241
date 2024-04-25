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
            Database.EnsureCreated();
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
                .HasOne(r => r.Owner)
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
                new Ammo("1#7.62x39mm#7.62#39#Full Metal Jacket"),
                new Ammo("2#.45 ACP#11.43#23#Semi-Jacketed Hollow Point"),
                new Ammo("3#9mm Parabellum#9#19#Full Metal Jacket"),
                new Ammo("4#5.56x45mm NATO#5.56#45#Full Metal Jacket"),
                new Ammo("5#9x19mm#9#19#Full Metal Jacket"),
                new Ammo("6#.308 Winchester#7.82#51#Full Metal Jacket"),
                new Ammo("7#12 gauge#18.5#70#Buckshot"),
                new Ammo("8#9x19mm#9#19#Full Metal Jacket"),
                new Ammo("9#.22 LR#5.56#15#Solid Point"),
                new Ammo("10#9x19mm#9#19#Full Metal Jacket"),
                new Ammo("11#8x57mm Mauser#8#57#Full Metal Jacket"),
                new Ammo("12#.410 bore#8.4#76#Shotshell"),
                new Ammo("13#20mm#20#100#High Explosive"),
            });

            modelBuilder.Entity<Firearm>().HasData(new Firearm[]
            {
                new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"),
                new Firearm("2#M1911#Colt#500#1989.03.25#2"),
                new Firearm("3#Glock 17#Glock#700#1995.11.30#3"),
                new Firearm("4#AR-15#ArmaLite#850#2000.08.17#4"),
                new Firearm("5#MP5#Heckler & Koch#800#1990.12.05#5"),
                new Firearm("6#Remington 700#Remington Arms#60#1985.07.18#6"),
                new Firearm("7#Mossberg 500#O.F. Mossberg & Sons#50#1988.09.22#7"),
                new Firearm("8#Beretta 92#Beretta#550#1998.04.14#8"),
                new Firearm("9#Ruger 10/22#Sturm, Ruger & Co.#70#1992.10.01#9"),
                new Firearm("10#SIG Sauer P226#SIG Sauer#600#2008.06.09#10"),
                new Firearm("11#AK-74#Kalashnikov#650#2000.11.28#1"),
                new Firearm("12#Desert Eagle#Israel Military Industries#500#1992.08.13#2"),
                new Firearm("13#Smith & Wesson M&P#Smith & Wesson#700#1998.06.17#3"),
                new Firearm("14#M4 Carbine#Colt Defense#900#2005.04.30#4"),
                new Firearm("15#HK416#Heckler & Koch#850#1996.09.22#5"),
                new Firearm("16#Winchester Model 70#Winchester Repeating Arms#70#1990.07.14#6"),
                new Firearm("17#Mossberg 590#O.F. Mossberg & Sons#55#1989.12.26#7"),
                new Firearm("18#Beretta M9#Beretta#600#1996.11.03#8"),
                new Firearm("19#Ruger Precision Rifle#Sturm, Ruger & Co.#75#1999.05.19#9"),
                new Firearm("20#SIG Sauer P320#SIG Sauer#650#2010.02.24#10"),
                new Firearm("21#FN FAL#FN Herstal#650#1985.10.07#1"),
                new Firearm("22#1911A1#Colt#500#1991.07.11#2"),
                new Firearm("23#H&K USP#Heckler & Koch#700#1999.04.15#3"),
                new Firearm("24#AR-10#ArmaLite#800#2003.12.28#4"),
                new Firearm("25#MP7#Heckler & Koch#850#1993.08.26#5"),
                new Firearm("26#Remington 870#Remington Arms Company#65#1986.03.18#6"),
                new Firearm("27#Mossberg 835#O.F. Mossberg & Sons#60#1992.12.14#7"),
                new Firearm("28#Beretta 92FS#Beretta#620#1997.09.06#8"),
                new Firearm("29#Ruger Mini-14#Sturm, Ruger & Co.#80#2002.10.21#9"),
                new Firearm("30#SIG Sauer P238#SIG Sauer#675#2013.11.29#10"),
                new Firearm("31#AKM#Kalashnikov#620#2007.05.08#1"),
                new Firearm("32#Glock 19#Glock#680#2000.09.30#3"),
                new Firearm("33#AR-15 Carbine#ArmaLite#870#2009.01.15#4"),
                new Firearm("34#MPX#SIG Sauer#800#2015.06.22#5"),
                new Firearm("35#Remington Model 700#Remington Arms Company#75#1995.12.19#6"),
                new Firearm("36#Mossberg 930#O.F. Mossberg & Sons#58#2001.04.04#7"),
                new Firearm("37#Beretta PX4 Storm#Beretta#610#2006.03.27#8"),
                new Firearm("38#Ruger SR556#Sturm, Ruger & Co.#85#2011.07.20#9"),
                new Firearm("39#SIG Sauer P938#SIG Sauer#700#2018.08.10#10"),
                new Firearm("40#Steyr AUG#Steyr Mannlicher#650#1990.02.18#1"),
                new Firearm("41#Kar98K#Mauser#15#2000.05.20#11"),
                new Firearm("42#Smith and Wesson Governor .410#Smith & Wesson#60#1995.11.12#12"),
                new Firearm("43#Beretta680#Beretta#45#1988.07.30#7"),
                new Firearm("44#R-KMG7A Ripsaw#Ripsaw#700#2018.09.04#13"),
            });


            modelBuilder.Entity<Owner>().HasData(new Owner[]
            {
                new Owner("1#Emma#Williams#2024.03.15.#Hunting"),
                new Owner("2#Liam#Smith#2023.05.20.#SelfDefense"),
                new Owner("3#Olivia#Johnson#2024.08.12.#Security"),
                new Owner("4#Noah#Jones#2024.10.25.#Hunting"),
                new Owner("5#Ava#Brown#2025.01.18.#SelfDefense"),
                new Owner("6#James#Davis#2025.04.30.#Security"),
                new Owner("7#Isabella#Miller#2025.07.22.#Hunting"),
                new Owner("8#Ethan#Wilson#2022.10.10.#SelfDefense"),
                new Owner("9#Sophia#Moore#2026.02.05.#Security"),
                new Owner("10#Mason#Taylor#2026.05.19.#Hunting"),
                new Owner("11#Harper#Anderson#2026.08.27.#SelfDefense"),
                new Owner("12#Aiden#Thomas#2026.11.11.#Security"),
                new Owner("13#Charlotte#Jackson#2027.03.08.#Hunting"),
                new Owner("14#Elijah#White#2027.06.25.#SelfDefense"),
                new Owner("15#Amelia#Harris#2021.09.14.#Security"),
                new Owner("16#Logan#Martin#2022.12.30.#Hunting"),
                new Owner("17#Avery#Thompson#2028.03.22.#SelfDefense"),
                new Owner("18#Lucas#Garcia#2028.06.11.#Security"),
                new Owner("19#Evelyn#Martinez#2028.09.26.#Hunting"),
                new Owner("20#Carter#Robinson#2029.01.14.#SelfDefense"),
            });
            modelBuilder.Entity<Register>().HasData(new Register[]
            {
                new Register("1#9#8#2017.12.14"),
                new Register("2#19#16#2019.11.21"),
                new Register("3#20#6#2019.05.03"),
                new Register("4#25#15#2021.04.29"),
                new Register("5#42#4#2016.08.02"),
                new Register("6#37#1#2018.10.15"),
                new Register("7#38#10#2021.07.28"),
                new Register("8#8#7#2020.06.05"),
                new Register("9#28#3#2018.08.21"),
                new Register("10#21#9#2022.02.20"),
                new Register("11#17#11#2015.07.16"),
                new Register("12#30#18#2017.02.13"),
                new Register("13#12#19#2016.09.22"),
                new Register("14#2#5#2020.10.31"),
                new Register("15#13#12#2019.04.19"),
                new Register("16#41#2#2015.10.27"),
                new Register("17#35#14#2019.08.28"),
                new Register("18#26#17#2017.06.01"),
                new Register("19#22#13#2020.03.09"),
                new Register("20#10#20#2016.01.25"),
                new Register("21#5#18#2019.07.01"),
                new Register("22#3#14#2017.01.09"),
                new Register("23#4#20#2016.12.17"),
                new Register("24#14#8#2022.01.19"),
                new Register("25#33#2#2016.05.04"),
                new Register("26#29#1#2016.04.14"),
                new Register("27#36#12#2018.06.27"),
                new Register("28#1#19#2015.04.05"),
                new Register("29#15#10#2017.09.23"),
                new Register("30#34#15#2021.10.17"),
                new Register("31#27#4#2018.12.28"),
                new Register("32#23#9#2017.03.26"),
                new Register("33#24#16#2022.08.12"),
                new Register("34#6#5#2019.02.14"),
                new Register("35#39#6#2018.04.12"),
                new Register("36#44#10#2016.07.02"),
                new Register("37#11#14#2015.08.03"),
                new Register("38#31#7#2021.06.20"),
                new Register("39#32#3#2022.03.19"),
                new Register("40#7#18#2021.02.14"),
                new Register("41#18#19#2017.10.11"),
                new Register("42#16#11#2018.03.31"),
                new Register("43#43#20#2019.12.08"),
                new Register("44#40#16#2022.10.27"),
                new Register("45#7#3#2020.09.29"),
                new Register("46#1#1#2016.11.12"),
                new Register("47#18#12#2022.04.02"),
                new Register("48#16#6#2018.07.04"),
                new Register("49#26#20#2015.06.07"),
                new Register("50#31#4#2019.03.25"),
                new Register("51#44#16#2017.11.26"),
                new Register("52#22#7#2020.05.30"),
                new Register("53#9#15#2018.10.21"),
                new Register("54#12#20#2016.08.22"),
                new Register("55#28#10#2020.11.02"),
                new Register("56#4#19#2017.08.05"),
                new Register("57#27#18#2015.01.12"),
                new Register("58#25#11#2019.09.15"),
                new Register("59#37#8#2016.12.29"),
                new Register("60#5#2#2021.08.18"),
                new Register("61#2#9#2016.04.09"),
                new Register("62#43#1#2021.07.31"),
                new Register("63#20#16#2016.03.23"),
                new Register("64#3#14#2021.09.05"),
                new Register("65#13#20#2019.04.09"),
                new Register("66#34#5#2018.01.15"),
                new Register("67#38#6#2015.05.23"),
                new Register("68#19#3#2021.11.01"),
                new Register("69#19#2#2017.07.12"),
                new Register("70#24#1#2015.03.21")

            });

        }

    }
}
