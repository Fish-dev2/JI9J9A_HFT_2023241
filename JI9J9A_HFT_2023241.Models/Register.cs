using JI9J9A_HFT_2023241.Models;
using System;

namespace JI9J9A_HFT_2023241.Repository
{
    public class Register
    {
        public int Id { get; set; }
        
        public int GunId { get; set; }
        public int OwnerId { get; set; }
        public DateTime RegistrationDate { get; set; }

        public virtual Firearm Firearm { get; set; }
        public virtual Owner Owner { get; set; }
        public Register()
        {
            
        }
        public Register(string line)
        {
            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            GunId = int.Parse(split[1]);
            OwnerId = int.Parse(split[2]);
            RegistrationDate = DateTime.Parse(split[3]);

        }

    }
}