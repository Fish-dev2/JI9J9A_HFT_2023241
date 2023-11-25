using JI9J9A_HFT_2023241.Models;
using System;
using System.Text.Json.Serialization;

namespace JI9J9A_HFT_2023241.Models
{
    public class Register
    {
        public int Id { get; set; }
        
        public int FirearmId { get; set; }
        public int OwnerId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Firearm Firearm { get; set; }
        [JsonIgnore]
        public virtual Owner Owner { get; set; }
        public Register()
        {
            
        }
        public Register(string line)
        {
            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            FirearmId = int.Parse(split[1]);
            OwnerId = int.Parse(split[2]);
            RegistrationDate = DateTime.Parse(split[3]);

        }

        public override bool Equals(object obj)
        {
            Register r = obj as Register;
            if (obj == null)
            {
                return false;
            }
            return r.FirearmId == this.FirearmId && r.OwnerId == this.OwnerId && r.RegistrationDate == this.RegistrationDate;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(FirearmId, OwnerId, RegistrationDate);
        }
    }
}