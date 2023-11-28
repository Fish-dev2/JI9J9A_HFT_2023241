using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using static JI9J9A_HFT_2023241.Logic.OwnerLogic;

namespace JI9J9A_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
     
        IOwnerLogic ownerLogic;
        IRegisterLogic registerLogic;
        IFirearmLogic firearmLogic;
        IAmmoLogic ammoLogic;


        public StatController(IOwnerLogic ownerLogic,  IRegisterLogic registerLogic, IFirearmLogic firearmLogic, IAmmoLogic ammoLogic)
        {
            
            this.ownerLogic = ownerLogic;
            this.registerLogic = registerLogic;
            this.firearmLogic = firearmLogic;
            this.ammoLogic = ammoLogic;
        }

        //OWNER
        [HttpGet]
        public IEnumerable<Owner> ExpiredLicences()
        {
            return this.ownerLogic.ExpiredLicences();
        }
        [HttpGet]
        public double? AverageAmountOfGuns()
        {
            return this.ownerLogic.AverageAmountOfGuns();
        }
        [HttpGet]
        public IEnumerable<LicenceInfo> AmountOfEachLicenceGivenOut()
        {
            return this.ownerLogic.AmountOfEachLicenceGivenOut();
        }
        
        //REGISTER
        [HttpGet]
        public IEnumerable<LicenceStat> FirearmsAndLicenceTypes()
        {
            return this.registerLogic.FirearmsAndLicenceTypes();
        }

        //AMMO
        [HttpGet]
        public IEnumerable<Ammo> Top3MostUsedAmmoTypes()
        {
            return this.ammoLogic.Top3MostUsedAmmoTypes();
        }


        [HttpGet("{id}")]
        public IEnumerable<Firearm> FirearmsUsingSpecifiedAmmo(int id)
        {
            return this.firearmLogic.FirearmsUsingSpecifiedAmmo(this.ammoLogic.Read(id));
        }
        

    }
}
