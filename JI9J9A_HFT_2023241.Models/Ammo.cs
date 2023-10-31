using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Models
{
    public class Ammo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AmmoId { get; set; }
        [StringLength(240)]
        public string Name { get; set; }
        [Range(0,100)]
        public double Diameter { get; set; }
        [Range(0,200)]
        public double Length { get; set; }
        [StringLength(50)]
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
