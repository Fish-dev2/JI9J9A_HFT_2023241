using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [Range(0, 100)]
        public double Diameter { get; set; }
        [Range(0, 200)]
        public double Length { get; set; }
        [StringLength(50)]
        public string BulletType { get; set; }
        [JsonIgnore]
        public virtual ICollection <Firearm> FirearmsUsingAmmo { get; set; }

        public Ammo()
        {
            FirearmsUsingAmmo = new HashSet<Firearm>();
        }
        public Ammo(string line):base()
        {
            string[] split = line.Split('#');
            AmmoId = int.Parse(split[0]);
            Name = split[1];
            Diameter = double.Parse(split[2].Replace('.',','));
            Length = double.Parse(split[3].Replace('.', ','));
            BulletType = split[4];
        }
    }
}
