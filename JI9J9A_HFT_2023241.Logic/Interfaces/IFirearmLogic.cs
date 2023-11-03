using JI9J9A_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JI9J9A_HFT_2023241.Logic
{
    public interface IFirearmLogic
    {
        void Create(Firearm item);
        void Delete(int id);
        Firearm Read(int id);
        IEnumerable<Firearm> ReadAll();
        void Update(Firearm item);
    }
}
