using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LicenceValidUntil { get; set; }
        public LicenceType LicenceType { get; set; }
        public ICollection<Firearm> LicensedGuns { get; set; }

        public Owner()
        {
            
        }
        public Owner(string line)
        {
            string[] split = line.Split('#');
            OwnerId = int.Parse(split[0]);
            FirstName = split[1];
            LastName = split[2];
            LicenceValidUntil = DateTime.Parse(split[3]);
            LicenceType =(LicenceType)Enum.Parse(typeof(LicenceType), split[4]);
        }

    }
}
