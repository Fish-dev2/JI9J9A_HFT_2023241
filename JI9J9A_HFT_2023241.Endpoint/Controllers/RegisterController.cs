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
    public class RegisterController : ControllerBase
    {
        IRegisterLogic logic;
        IHubContext<SignalRHub> hub;

        public RegisterController(IRegisterLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        // GET: api/<RegisterController>
        [HttpGet]
        public IEnumerable<Register> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<RegisterController>/5
        [HttpGet("{id}")]
        public Register Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<RegisterController>
        [HttpPost]
        public void Create([FromBody] Register value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("RegisterCreated", value);
        }

        // PUT api/<RegisterController>/5
        [HttpPut]
        public void Update([FromBody] Register value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("RegisterUpdated", value);
        }

        // DELETE api/<RegisterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var registerToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("RegisterDeleted", registerToDelete);
        }
    }
}
