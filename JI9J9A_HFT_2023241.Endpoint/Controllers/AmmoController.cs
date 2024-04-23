using JI9J9A_HFT_2023241.Endpoint.Services;
using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JI9J9A_HFT_2023241.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AmmoController : ControllerBase
    {
        IAmmoLogic logic;
        IHubContext<SignalRHub> hub;

        public AmmoController(IAmmoLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("AmmoCreated", value);
        }

        // PUT api/<FirearmController>/5
        [HttpPut]
        public void Update([FromBody] Ammo value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("AmmoUpdated", value);
        }

        // DELETE api/<FirearmController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var ammoToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("AmmoDeleted", ammoToDelete);
        }
    }
}
