using JI9J9A_HFT_2023241.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;

namespace JI9J9A_HFT_2023241
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GunLicenceDbContext db = new GunLicenceDbContext();

            foreach (var item in db.Ammunitions)
            {
                Console.WriteLine(item.Name + ":");
                foreach (var gun in item.FirearmsUsingAmmo)
                {
                    Console.WriteLine("\t"+gun.Name);
                }
            }

            //SELECT * FROM Ammunitions
            //INNER JOIN Firearms ON Firearms.AmmoId = Ammo.AmmoId
            //

            foreach (var item in db.Owners)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
                foreach (var gun in item.LicensedGuns)
                {
                    Console.WriteLine("\t"+gun.Name);
                }
            }

            foreach (var item in db.Firearms)
            {
                Console.WriteLine(item.Name);
                foreach (var owner in item.OwnersHavingThisGun)
                {
                    Console.WriteLine("\t" +owner.FirstName + " "+ owner.LastName);
                }
            }


            Console.ReadLine();
        }
    }
}
