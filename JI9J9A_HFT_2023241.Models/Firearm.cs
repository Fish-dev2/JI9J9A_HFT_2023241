using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<Owner> OwnersHavingThisGun { get; set; }
        [JsonIgnore]
        public virtual ICollection<Register> Registers { get; set; }

        public Firearm()
        {
            OwnersHavingThisGun = new HashSet<Owner>();
            Registers = new HashSet<Register>();
        }
        public Firearm(string line):this()
        {
            string[] split = line.Split('#');

            GunId = int.Parse(split[0]);
            Name = split[1];
            Manufacturer = split[2];
            FireRate = int.Parse(split[3]);
            ReleaseDate = DateTime.Parse(split[4]);
            AmmoId = int.Parse(split[5]);

        }
        public override bool Equals(object obj)
        {
            Firearm f = obj as Firearm;
            if (f == null)
            {
                return false;
            }
            return f.Name == this.Name && f.Manufacturer == this.Manufacturer && f.FireRate == this.FireRate && f.ReleaseDate == this.ReleaseDate && f.AmmoType.Equals(this.AmmoType);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name,Manufacturer,FireRate,ReleaseDate,AmmoType);
        }
    }
}
