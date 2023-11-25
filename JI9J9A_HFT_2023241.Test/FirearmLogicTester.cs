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
    internal class FirearmLogicTester
    {
        FirearmLogic logic;
        Mock<IRepository<Firearm>> mockFirearmRepo;
        [SetUp]
        public void Init()
        {
            mockFirearmRepo = new Mock<IRepository<Firearm>>();
            List<Ammo> ammoList = new List<Ammo>()
            {
                new Ammo("1#7.62x39mm#7.62#39#Full Metal Jacket"),
                new Ammo("2#.45 ACP#11.43#23#Semi-Jacketed Hollow Point"),
                new Ammo("3#9mm Parabellum#9#19#Full Metal Jacket"),
            };
            List<Firearm> list = new List<Firearm>()
            {
                new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1")
                {
                    AmmoType = ammoList[0]
                },
                new Firearm("2#M1911#Colt#500#1989.03.25#2")
                {
                    AmmoType = ammoList[1]
                },
                new Firearm("3#Glock 17#Glock#700#1995.11.30#3")
                {
                    AmmoType = ammoList[2]
                },
                new Firearm("11#AK-74#Kalashnikov#650#2000.11.28#1")
                {
                    AmmoType = ammoList[0]
                },
            };

            var helper = list.AsQueryable();
            mockFirearmRepo.Setup(m => m.ReadAll()).Returns(helper);
            logic = new FirearmLogic(mockFirearmRepo.Object);

        }
        [Test]
        public void CreateFirearmTest()
        {
            var fa = new Firearm("4#AR-15#ArmaLite#850#2000.08.17#4");
            logic.Create(fa);
            mockFirearmRepo.Verify(r => r.Create(fa), Times.Once);
        }
        [Test]
        public void FirearmsUsingSpecifiedAmmoTest()
        {
            Ammo am = new Ammo("1#7.62x39mm#7.62#39#Full Metal Jacket");
            var result = logic.FirearmsUsingSpecifiedAmmo(am);
            var expected = new List<Firearm>()
            {
                new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1")
                {
                    AmmoType = am
                },
                new Firearm("11#AK-74#Kalashnikov#650#2000.11.28#1")
                {
                    AmmoType = am
                },
            };
            Assert.AreEqual(expected, result);
        }
    }
}
