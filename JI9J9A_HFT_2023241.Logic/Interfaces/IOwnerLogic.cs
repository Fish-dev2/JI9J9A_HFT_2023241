using JI9J9A_HFT_2023241.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static JI9J9A_HFT_2023241.Logic.OwnerLogic;

namespace JI9J9A_HFT_2023241.Logic
{
    public interface IOwnerLogic
    {
        void Create(Owner item);
        void Delete(int id);
        Owner Read(int id);
        IEnumerable<Owner> ReadAll();
        void Update(Owner item);
        public IEnumerable<Owner> ExpiredLicences();
        double AverageAmountOfGuns();
        IEnumerable<LicenceInfo> AmountOfEachLicenceGivenOut();
    }
}
