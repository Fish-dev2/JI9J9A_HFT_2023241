﻿using JI9J9A_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Logic.Interfaces
{
    public interface IAmmoLogic
    {
        void Create(Ammo item);
        void Delete(int id);
        double? GetAverageRatePerYear(int year);
        Ammo Read(int id);
        IEnumerable<Ammo> ReadAll();
        void Update(Ammo item);
    }
}
