﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JI9J9A_HFT_2023241;
using System.Text.Json.Serialization;

namespace JI9J9A_HFT_2023241.Models
{
    public enum LicenceType
    {
        SelfDefense,
        Hunting,
        Security
    }
    public class Owner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OwnerId { get; set; }
        [StringLength(240)]
        public string FirstName { get; set; }
        [StringLength(240)]
        public string LastName { get; set; }
        public DateTime LicenceValidUntil { get; set; }
        public LicenceType LicenceType { get; set; }
        public virtual ICollection<Firearm> LicensedGuns { get; set; }
        [JsonIgnore]
        public virtual ICollection<Register> Registers { get; set; }

        public Owner()
        {
            LicensedGuns = new HashSet<Firearm>();
            Registers = new HashSet<Register>();
        }
        public Owner(string line) : this()
        {
            string[] split = line.Split('#');
            OwnerId = int.Parse(split[0]);
            FirstName = split[1];
            LastName = split[2];
            LicenceValidUntil = DateTime.Parse(split[3]);
            LicenceType =(LicenceType)Enum.Parse(typeof(LicenceType), split[4]);
        }
        public override bool Equals(object obj)
        {
            Owner o = obj as Owner;
            if (o == null)
            {
                return false;
            }
            return o.FirstName == this.FirstName && o.LastName == this.LastName && o.LicenceValidUntil == this.LicenceValidUntil && o.LicenceType == this.LicenceType;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, LicenceValidUntil, LicenceType);
        }

    }
}
