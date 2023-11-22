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

            foreach ( var item in result)
            {
                Console.WriteLine(item.Name);
            }
            //foreach (var item in result2)
            //{
            //    Console.WriteLine(item.FirstName +" "+ item.LastName);
            //}
            Console.WriteLine(result2);
            foreach ( var item in result3)
            {
                Console.WriteLine(item.LicenceType.ToString() +""+ item.Count);
            }
            Console.WriteLine("Átlagos fegyver darabszám: "+ result4);


            //foreach (var item in result5)
            //{
            //    string name = (string)item.GetType().GetProperty("FirearmName").GetValue(item);

            //    IEnumerable<object> counts = (IEnumerable<object>)item.GetType().GetProperty("LicenceTypeCounts").GetValue(item);
            //    Console.WriteLine(name);
            //    foreach (var count in counts)
            //    {
            //        Console.WriteLine("\t"+count.GetType().GetProperty("LicenceType").GetValue(count) + ": "+
            //            (int)count.GetType().GetProperty("Count").GetValue(count));
            //    }
                
            //}
            foreach (var item in result5)
            {
                
            }


            Console.ReadLine();
        }
    }
}
