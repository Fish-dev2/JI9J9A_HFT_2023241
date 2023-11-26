using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using JI9J9A_HFT_2023241.Models;
using System.Collections;
using System.Collections.Generic;

namespace JI9J9A_HFT_2023241.Client
{
    internal class Program
    {
        static RestService rest;
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:27031/", "firearm");


        }
    }
}
