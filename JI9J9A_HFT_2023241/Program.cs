﻿using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using JI9J9A_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ConsoleTools;
using System.Text;
using System.Reflection.Metadata;

namespace JI9J9A_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Create(string entity)
        {
            object item = "";
            switch (entity)
            {
                case "Ammo":
                    item = new Ammo();
                    break;
                case "Firearm":
                    item = new Firearm();
                    break;
                case "Owner":
                    item = new Owner();
                    break;
                case "Register":
                    item = new Register();
                    break;
                default:
                    break;
            }

            try
            {
                item = CreateItem(item, entity);
                rest.Post(item, entity.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Operation failed.");
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press anything to continue.");
                Console.ReadLine();
            }

            
        }
        static void List(string entity)
        {


            if (entity == "Ammo")
            {
                ListItems<Ammo>(entity);
            }
            else if (entity == "Firearm")
            {
                ListItems<Firearm>(entity);
            }
            else if(entity == "Owner")
            {
                ListItems<Owner>(entity);
            }
            else if(entity == "Register")
            {
                ListItems<Register>(entity);
            }
            Console.WriteLine("Press anything to continue.");
            Console.ReadKey();


        }
        static void Update(string entity)
        {
            object item = "";
            object olditem = "";
            Console.WriteLine("Enter " + entity + "'s ID to update:");



            try
            {
                int id = int.Parse(Console.ReadLine());
                switch (entity)
                {
                    case "Ammo":
                        item = rest.Get<Ammo>(id, entity.ToLower());
                        ListItem<Ammo>(entity, id);
                        break;
                    case "Firearm":
                        item = rest.Get<Firearm>(id, entity.ToLower());
                        ListItem<Firearm>(entity, id);
                        break;
                    case "Owner":
                        item = rest.Get<Owner>(id, entity.ToLower());
                        ListItem<Owner>(entity, id);
                        break;
                    case "Register":
                        item = rest.Get<Register>(id, entity.ToLower());
                        ListItem<Register>(entity, id);
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
                item = CreateItem(item, entity);
                rest.Put(item, entity.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Operation failed.");
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press anything to continue.");
                Console.ReadLine();
            }


        }
        static void Delete<T>(string entity)
        {
            Console.WriteLine($"Enter {entity}'s id to delete:");


            try
            {
                int id = int.Parse(Console.ReadLine());
                var item = rest.Get<T>(id, entity.ToLower());
                rest.Delete(id, entity.ToLower());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Operation failed.");
                Console.WriteLine("Error: "+ ex.Message);
                Console.WriteLine("Press anything to continue.");
                Console.ReadLine();
            }




        }
        static void Read(string entity)
        {
            Console.WriteLine("Input " + entity.ToLower() + "'s id you want to read: ");
            try
            {
                int input = int.Parse(Console.ReadLine());

                if (entity == "Ammo")
                {
                    ListItem<Ammo>(entity, input);
                }
                else if (entity == "Firearm")
                {
                    ListItem<Firearm>(entity, input);
                }
                else if (entity == "Owner")
                {
                    ListItem<Owner>(entity, input);
                }
                else if (entity == "Register")
                {
                    ListItem<Register>(entity, input);
                }
                Console.WriteLine("Press anything to continue.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Operation failed.");
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Press anything to continue.");
                Console.ReadLine();

                throw;
            }

        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:27031/", "firearm");







            var ammoSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Ammo"))
                .Add("List", () => List("Ammo"))
                .Add("Create", () => Create("Ammo"))
                .Add("Delete", () => Delete<Ammo>("Ammo"))
                .Add("Update", () => Update("Ammo"))
                .Add("Exit", ConsoleMenu.Close);
            var ownerSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Owner"))
                .Add("List", () => List("Owner"))
                .Add("Create", () => Create("Owner"))
                .Add("Delete", () => Delete<Owner>("Owner"))
                .Add("Update", () => Update("Owner"))
                .Add("Exit", ConsoleMenu.Close);
            var registerSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Register"))
                .Add("List", () => List("Register"))
                .Add("Create", () => Create("Register"))
                .Add("Delete", () => Delete<Register>("Register"))
                .Add("Update", () => Update("Register"))
                .Add("Exit", ConsoleMenu.Close);
            var firearmSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Firearm"))
                .Add("List", () => List("Firearm"))
                .Add("Create", () => Create("Firearm"))
                .Add("Delete", () => Delete<Firearm>("Firearm"))
                .Add("Update", () => Update("Firearm"))
                .Add("Exit", ConsoleMenu.Close);
            var statsSubMenu = new ConsoleMenu(args, 1)
                .Add("Average Amount of Guns", () => Stat("AverageAmountOfGuns"))
                .Add("Amount of each licence given out", () => Stat("AmountOfEachLicenceGivenOut"))
                .Add("Expired licences", () => Stat("ExpiredLicences"))
                .Add("Firearms and Licence types", () => Stat("FirearmsAndLicenceTypes"))
                .Add("Top 3 most used ammo types", () => Stat("Top3MostUsedAmmoTypes"))
                .Add("List firearms using specified ammo", () => Stat("FirearmsUsingSpecifiedAmmo"))
                .Add("Exit", ConsoleMenu.Close);
            var menu = new ConsoleMenu(args, 0)
                .Add("Ammos", () => ammoSubMenu.Show())
                .Add("Owners", () => ownerSubMenu.Show())
                .Add("Registers", () => registerSubMenu.Show())
                .Add("Firearms", () => firearmSubMenu.Show())
                .Add("Stats", () => statsSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);



            menu.Show();
        }

        private static void Stat(string input)
        {
            string link = $"Stat/{input}";
            if (input == "AverageAmountOfGuns")
            {
                var value = rest.GetSingle<double>(link);
                Console.WriteLine("Average amount of guns per owner: "+ string.Format("{0:0.00}",value));
            }
            else if (input == "AmountOfEachLicenceGivenOut")
            {
                var value = rest.GetSingle<IEnumerable<LicenceInfo>>(link);
                foreach (var item in value)
                {
                    Console.WriteLine(item.LicenceType+ ": " + item.Count);
                }

            }
            else if (input == "ExpiredLicences")
            {
                var owners = rest.GetSingle<IEnumerable<Owner>>(link);
                Console.WriteLine("People with expired licences: ");
                foreach (var item in owners)
                {
                    Console.WriteLine($"[{item.OwnerId}] {item.FirstName} {item.LastName}");
                }
            }
            else if (input == "FirearmsAndLicenceTypes")
            {
                var value = rest.GetSingle<IEnumerable<LicenceStat>>(link);
                Console.WriteLine("Firearms and the type of licences people own the guns with:");
                foreach (var item in value)
                {
                    Console.WriteLine(item.Firearm+ ":");
                    foreach (var counts in item.licenceCounts)
                    {
                        Console.WriteLine("\t"+counts.Type + ": " + counts.Count);
                    }
                }
            }
            else if (input == "Top3MostUsedAmmoTypes")
            {
                var value = rest.GetSingle<IEnumerable<Ammo>>(link);
                Console.WriteLine("Top 3 most used ammo types:");
                foreach (var item in value)
                {
                    ShowItem(item);
                    Console.WriteLine();
                }
            }
            else if (input == "FirearmsUsingSpecifiedAmmo")
            {
                try
                {
                    FirearmsUsingAmmo(link);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Operation failed.");
                    Console.WriteLine("Error: " + ex.Message);
                    Console.ReadLine();
                }
            }
            else
            {
                throw new ArgumentException("No such menu point exists " + input);
            }
            Console.WriteLine("Press anything to continue.");
            Console.ReadKey();
        }
        

        private static void FirearmsUsingAmmo(string link)
        {
            Console.WriteLine("Input the ID of an ammo to get the firearms using that ammo:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Ammo type: ");
            Ammo ammo = rest.Get<Ammo>(id, "Ammo");
            ShowItem(ammo);
            Console.WriteLine();
            Console.WriteLine("Firearms using specified ammo:");
            var value = rest.Get<IEnumerable<Firearm>>(id, link);
            foreach (var item in value)
            {
                ShowItem(item);
                Console.WriteLine();
            }
        }

        static void ShowHeaders(object item)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    if (prop.Name.Length > 6)
                    {
                        Console.Write(prop.Name.Substring(0, 7) + "\t");
                    }
                    else
                    {
                        Console.Write(prop.Name + "\t");
                    }
                }
            }
        }
        static void ShowItem(object item)
        {
            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    if (prop.GetValue(item).ToString().Length > 6)
                    {
                        Console.Write(prop.GetValue(item).ToString().Substring(0, 7) + "\t");
                    }
                    else
                    {
                        Console.Write(prop.GetValue(item).ToString() + "\t");
                    }
                }
            }
        }
        static void ListItems<T>(string entity)
        {
            List<T> items = rest.Get<T>(entity.ToLower());
            ShowHeaders(items[0]);
            Console.WriteLine();
            foreach (var obj in items)
            {
                ShowItem(obj);
                Console.WriteLine();
            }
        }
        static void ListItem<T>(string entity, int id)
        {
            try
            {
                T item = rest.Get<T>(id, entity.ToLower());
                foreach (var prop in item.GetType().GetProperties())
                {
                    if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                    {
                        Console.Write("[" + prop.Name + "]: " + prop.GetValue(item).ToString());
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Operation failed.");
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadLine();
            }



        }
        static T CreateItem<T>(T item, string entity)
        {

            foreach (var prop in item.GetType().GetProperties())
            {
                if (!Attribute.IsDefined(prop, typeof(KeyAttribute)) && prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    Console.WriteLine("Input " + entity.ToLower() + "'s " + prop.Name.ToLower());
                    object data = "";

                    switch (prop.PropertyType.Name.ToLower())
                    {
                        case "int32":
                            data = int.Parse(Console.ReadLine());
                            break;
                        case "double":
                            data = double.Parse(Console.ReadLine().Replace('.', ','));
                            break;
                        case "datetime":
                            Console.WriteLine("Date in yyyy.mm.dd. format: ");
                            data = DateTime.Parse(Console.ReadLine());
                            break;
                        case "licencetype":
                            Console.WriteLine("Options to choose from are : SelfDefense[0], Hunting[1], Security[2]");
                            data = (LicenceType)int.Parse(Console.ReadLine());
                            break;
                        default:
                            data = Console.ReadLine();
                            break;
                    }
                    prop.SetValue(item, data);
                }
            }
            return item;
        }
    }
}
