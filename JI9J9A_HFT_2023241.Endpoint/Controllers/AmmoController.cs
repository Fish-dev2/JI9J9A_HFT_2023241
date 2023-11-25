using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JI9J9A_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AmmoController : ControllerBase
    {
        IAmmoLogic logic;

        public AmmoController(IAmmoLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<FirearmController>
        [HttpGet]
        public IEnumerable<Ammo> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<FirearmController>/5
        [HttpGet("{id}")]
        public Ammo Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<FirearmController>
        [HttpPost]
        public void Create([FromBody] Ammo value)
        {
            this.logic.Create(value);
        }

        // PUT api/<FirearmController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] Ammo value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<FirearmController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
