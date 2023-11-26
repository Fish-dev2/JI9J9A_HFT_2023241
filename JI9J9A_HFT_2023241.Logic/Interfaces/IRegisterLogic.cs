using JI9J9A_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JI9J9A_HFT_2023241.Logic.RegisterLogic;

namespace JI9J9A_HFT_2023241.Logic
{
    public interface IRegisterLogic
    {
        void Create(Register item);
        void Delete(int id);
        Register Read(int id);
        IEnumerable<Register> ReadAll();
        void Update(Register item);
        public IEnumerable<LicenceStat> FirearmsAndLicenceTypes();
    }
}
