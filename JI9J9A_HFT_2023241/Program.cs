using JI9J9A_HFT_2023241.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;

namespace JI9J9A_HFT_2023241
{
    internal class Program
    {
        static void Main(string[] args)
        {


            GunLicenceDbContext db = new GunLicenceDbContext();
            AmmoLogic al = new AmmoLogic(new AmmoRepository(db));
            FirearmLogic fl = new FirearmLogic(new FirearmRepository(db));
            OwnerLogic ol = new OwnerLogic(new OwnerRepository(db));
            RegisterLogic rl = new RegisterLogic(new RegisterRepository(db));

            var result = al.Top3MostUsedAmmoTypes();
            var result2 = ol.ExpiredLicences();
            var result3 = ol.AmountOfEachLicenceGivenOut();
            var result4 = ol.AverageAmountOfGuns();
            var result5 = rl.FirearmsAndLicenceTypes();
            Console.WriteLine("Top3Ammo:");
            foreach ( var item in result)
            {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("Expired:");
            foreach (var item in result2)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }
            foreach ( var item in result3)
            {
                Console.WriteLine(item.LicenceType.ToString() +""+ item.Count);
            }
            Console.WriteLine("Átlagos fegyver darabszám: "+ result4);


            foreach (var item in result5)
            {
                Console.WriteLine(item.Firearm);
                foreach (var count in item.licenceCounts)
                {
                    Console.WriteLine("\t"+count.Type + ": " + count.Count);
                }
            }


            Console.ReadLine();
        }
    }
}
