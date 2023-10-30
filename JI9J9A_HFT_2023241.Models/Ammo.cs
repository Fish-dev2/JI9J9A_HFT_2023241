using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Models
{
    internal class Ammo
    {
        public int AmmoId { get; set; }
        public string Name { get; set; }
        public double Diameter { get; set; }
        public double Length { get; set; }
        public string BulletType { get; set; }
        public Ammo()
        {
            
        }
        public Ammo(string line)
        {
            string[] split = line.Split('#');
            AmmoId = int.Parse(split[0]);
            Name = split[1];
            Diameter = double.Parse(split[2]);
            Length = double.Parse(split[3]);
            BulletType = split[4];
        }
    }
}
