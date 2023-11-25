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
using static JI9J9A_HFT_2023241.Logic.OwnerLogic;

namespace JI9J9A_HFT_2023241.Test
{
    internal class OwnerLogicTester
    {
        OwnerLogic logic;
        Mock<IRepository<Owner>> mockOwnerRepo;
        [SetUp]
        public void Init()
        {
            mockOwnerRepo = new Mock<IRepository<Owner>>();
            //logic = new OwnerLogic(new FakeOwnerRepository());

            List<Owner> list = new List<Owner>()
            {
                new Owner("1#Emma#Williams#2024.03.15.#Hunting"),
                new Owner("2#Liam#Smith#2023.05.20.#SelfDefense"),
                new Owner("3#Olivia#Johnson#2024.08.12.#Security"),
                new Owner("4#Noah#Jones#2020.10.25.#Hunting"),
            };
            list[0].LicensedGuns = new List<Firearm>();
            list[1].LicensedGuns = new List<Firearm>();
            list[2].LicensedGuns = new List<Firearm>();
            list[3].LicensedGuns = new List<Firearm>();
            list[0].LicensedGuns.Add(new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"));
            list[1].LicensedGuns.Add(new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"));
            list[1].LicensedGuns.Add(new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"));
            list[2].LicensedGuns.Add(new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"));
            list[2].LicensedGuns.Add(new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"));
            list[3].LicensedGuns.Add(new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"));

            var helper = list.AsQueryable();

            mockOwnerRepo.Setup(m => m.ReadAll()).Returns(helper);
            logic = new OwnerLogic(mockOwnerRepo.Object);
        }
        [Test]
        public void ExpiredLicencesTest()
        {
            IEnumerable<Owner> result = logic.ExpiredLicences();
            List<Owner> expected = new List<Owner>()
            {
                new Owner("2#Liam#Smith#2023.05.20.#SelfDefense"),
                new Owner("4#Noah#Jones#2020.10.25.#Hunting"),
            };
            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AmountOfEachLicenceGivenOutTest()
        {
            var result = logic.AmountOfEachLicenceGivenOut();
            var expected = new List<LicenceInfo>()
            {
                new LicenceInfo()
                {
                    LicenceType = LicenceType.SelfDefense,
                    Count = 1
                },
                new LicenceInfo()
                {
                    LicenceType = LicenceType.Security,
                    Count = 1
                },
                new LicenceInfo()
                {
                    LicenceType = LicenceType.Hunting,
                    Count = 2
                }
            };

            Assert.AreEqual(result, expected);
        }
        [Test]
        public void AverageAmountOfGunsTest()
        {
            var result = logic.AverageAmountOfGuns();
            double expected = 1.5;
            Assert.AreEqual(expected, result);
        }
        [Test]
        public void CreateOwnerTest()
        {

            var owner = new Owner("5#Emma#Williams#2024.03.15.#Hunting");
            //ACT
            logic.Create(owner);

            //ASSERT
            mockOwnerRepo.Verify(r => r.Create(owner), Times.Once);
        }

    }
}
