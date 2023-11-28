using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using JI9J9A_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ConsoleTools;

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
                        default:
                            data = Console.ReadLine();
                            break;
                    }

                    prop.SetValue(item, data);
                }
            }
            rest.Post(item,entity.ToLower());
            
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
            int id = int.Parse(Console.ReadLine());
            switch (entity)
            {
                case "Ammo":
                    item = rest.Get<Ammo>(id, entity.ToLower());
                    ListItem<Ammo>(entity, id);
                    break;
                case "Firearm":
                    item =  rest.Get<Firearm>(id, entity.ToLower());
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

            foreach (var prop in item.GetType().GetProperties())
            {
                if (!Attribute.IsDefined(prop, typeof(KeyAttribute)) && prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    Console.WriteLine("Input new " + entity.ToLower() + "'s " + prop.Name.ToLower());
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
            rest.Put(item, entity.ToLower());
        }
        static void Delete(string entity)
        {
            Console.WriteLine($"Enter {entity}'s id to delete:");
            int id = int.Parse(Console.ReadLine());
            rest.Delete(id,entity.ToLower());
        }
        static void Read(string entity)
        {
            Console.WriteLine("Input " + entity.ToLower() + "'s id you want to read: ");
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
                ListItem<Register>(entity,input);
            }
            Console.WriteLine("Press anything to continue.");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:27031/", "firearm");

            
            var ammoSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Ammo"))
                .Add("List", () => List("Ammo"))
                .Add("Create", () => Create("Ammo"))
                .Add("Delete", () => Delete("Ammo"))
                .Add("Update", () => Update("Ammo"))
                .Add("Exit", ConsoleMenu.Close);
            var ownerSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Owner"))
                .Add("List", () => List("Owner"))
                .Add("Create", () => Create("Owner"))
                .Add("Delete", () => Delete("Owner"))
                .Add("Update", () => Update("Owner"))
                .Add("Exit", ConsoleMenu.Close);
            var registerSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Register"))
                .Add("List", () => List("Register"))
                .Add("Create", () => Create("Register"))
                .Add("Delete", () => Delete("Register"))
                .Add("Update", () => Update("Register"))
                .Add("Exit", ConsoleMenu.Close);
            var firearmSubMenu = new ConsoleMenu(args, 1)
                .Add("Read", () => Read("Firearm"))
                .Add("List", () => List("Firearm"))
                .Add("Create", () => Create("Firearm"))
                .Add("Delete", () => Delete("Firearm"))
                .Add("Update", () => Update("Firearm"))
                .Add("Exit", ConsoleMenu.Close);
            var menu = new ConsoleMenu(args, 0)
                .Add("Ammos", () => ammoSubMenu.Show())
                .Add("Owners", () => ownerSubMenu.Show())
                .Add("Registers", () => registerSubMenu.Show())
                .Add("Firearms", () => firearmSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);


            menu.Show();
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
            T item = rest.Get<T>(id,entity.ToLower());

            foreach (var prop in item.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null)
                {
                    Console.Write("["+prop.Name + "]: " + prop.GetValue(item).ToString());
                    Console.WriteLine();
                }
            }
        }
    }
}
