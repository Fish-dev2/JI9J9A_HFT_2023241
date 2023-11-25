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
    internal class RegisterLogicTester
    {
        RegisterLogic logic;
        Mock<IRepository<Register>> mockRegisterRepo;
        [SetUp]
        public void Init()
        {
            mockRegisterRepo = new Mock<IRepository<Register>>();

            List<Register> list = new List<Register>()
            {
                //id, FirearmId, OwnerId, RegDate
                new Register("46#1#1#2016.11.12")
                {
                    Owner = new Owner("1#Emma#Williams#2024.03.15.#Hunting"),
                    Firearm = new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"),
                },
                new Register("28#1#19#2015.04.05")
                {
                    Owner = new Owner("19#Evelyn#Martinez#2028.09.26.#Hunting"),
                    Firearm = new Firearm("1#AK-47#Kalashnikov#600#2005.06.12#1"),
                },

            };
            
            mockRegisterRepo.Setup(m => m.ReadAll()).Returns(list.AsQueryable());
            mockRegisterRepo.Setup(m => m.Read(It.IsAny<int>()))
                .Returns((int id) => list.FirstOrDefault(register => register.Id == id));
            logic = new RegisterLogic(mockRegisterRepo.Object);
            mockRegisterRepo.Setup(m => m.Delete(It.IsAny<int>()))
                .Callback<int>(id =>
                {
                    var itemToRemove = list.FirstOrDefault(register => register.Id == id);
                    if (itemToRemove != null)
                    {
                        list.Remove(itemToRemove);
                    }
                });



        }

        [Test]
        public void FirearmsAndLicenceTypesTester()
        {
            var result = logic.FirearmsAndLicenceTypes();
            var expected = new List<RegisterLogic.LicenceStat>()
            {
                new RegisterLogic.LicenceStat()
                {
                    Firearm = "AK-47",
                    licenceCounts = new List<RegisterLogic.LicenceStat.LicenceCount>()
                    {
                        new RegisterLogic.LicenceStat.LicenceCount()
                        {
                            Count = 2,
                            Type = LicenceType.Hunting
                        }
                    }
                }
            };
            Assert.AreEqual(expected, result);
        }
        [Test]
        [Sequential]
        public void ReadTest([Values(46,28)]int a)
        {
            Register result = logic.Read(a);
            Register expected = logic.ReadAll().First(t => t.Id == a);
            Assert.AreEqual(result, expected);
        }
        [Test]
        [Sequential]
        public void DeleteTest([Values(46, 28)] int a)
        {
            logic.Delete(a);
            //mockRegisterRepo.Verify(m => m.Delete(a), Times.Once);
            var result = logic.ReadAll().FirstOrDefault(register => register.Id == a);
            Assert.AreEqual(result, null);
        }
    }
}
