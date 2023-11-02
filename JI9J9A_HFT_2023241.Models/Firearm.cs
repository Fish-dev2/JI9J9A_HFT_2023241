using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Collections.Generic;
using JI9J9A_HFT_2023241.Repository;

namespace JI9J9A_HFT_2023241.Models
{
    public class Firearm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GunId { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Manufacturer { get; set; }
        [Range(0,10000)]
        public int FireRate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int AmmoId { get; set; }
        public virtual Ammo AmmoType { get; set; }
        public virtual ICollection<Owner> OwnersHavingThisGun { get; set; }
        public virtual ICollection<Register> Registers { get; set; }

        public Firearm()
        {
            
        }
        public Firearm(string line)
        {
            string[] split = line.Split('#');

            GunId = int.Parse(split[0]);
            Name = split[1];
            Manufacturer = split[2];
            FireRate = int.Parse(split[3]);
            ReleaseDate = DateTime.Parse(split[4]);
            AmmoId = int.Parse(split[5]);

        }

    }
}
