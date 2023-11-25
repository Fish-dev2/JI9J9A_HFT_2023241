using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using JI9J9A_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Test
{
    internal class AmmoLogicTester
    {
        AmmoLogic logic;
        Mock<IRepository<Ammo>> mockAmmoRepo;
        [SetUp]
        public void Init()
        {
            mockAmmoRepo = new Mock<IRepository<Ammo>>();
            List<Ammo> list = new List<Ammo>()
            {
                new Ammo("1#7.62x39mm#7.62#39#Full Metal Jacket")
                {
                    FirearmsUsingAmmo = new List<Firearm>()
                    {
                        new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"),
                        new Firearm("11#AK-74#Kalashnikov#650#2000.11.28#1"),
                        new Firearm("21#FN FAL#FN Herstal#650#1985.10.07#1"),
                    }
                },
                new Ammo("2#.45 ACP#11.43#23#Semi-Jacketed Hollow Point")
                {
                    FirearmsUsingAmmo = new List<Firearm>()
                    {
                        new Firearm("2#M1911#Colt#500#1989.03.25#2"),
                        new Firearm("22#1911A1#Colt#500#1991.07.11#2"),
                    }
                },
                new Ammo("3#9mm Parabellum#9#19#Full Metal Jacket")
                {
                    FirearmsUsingAmmo = new List <Firearm>()
                    {
                        new Firearm("3#Glock 17#Glock#700#1995.11.30#3"),
                        new Firearm("23#H&K USP#Heckler & Koch#700#1999.04.15#3"),
                    }
                },
                new Ammo("4#5.56x45mm NATO#5.56#45#Full Metal Jacket")
                {
                    FirearmsUsingAmmo = new List<Firearm>()
                    {
                        new Firearm("4#AR-15#ArmaLite#850#2000.08.17#4"),
                    }
                },

                new Ammo("5#9x19mm#9#19#Full Metal Jacket")
                {
                    FirearmsUsingAmmo = new List<Firearm>()
                    {
                        new Firearm("5#MP5#Heckler & Koch#800#1990.12.05#5"),
                    },
                }
            };
            mockAmmoRepo.Setup(m => m.ReadAll()).Returns(list.AsQueryable());
            logic = new AmmoLogic(mockAmmoRepo.Object);
        }
        [Test]
        public void Top3MostUsedAmmoTypesTester()
        {
            var result = logic.Top3MostUsedAmmoTypes();
            var expected = new List<Ammo>
            {
                new Ammo("1#7.62x39mm#7.62#39#Full Metal Jacket"),
                new Ammo("2#.45 ACP#11.43#23#Semi-Jacketed Hollow Point"),
                new Ammo("3#9mm Parabellum#9#19#Full Metal Jacket"),

            };
            Assert.AreEqual(expected, result);
        }
    }
}
