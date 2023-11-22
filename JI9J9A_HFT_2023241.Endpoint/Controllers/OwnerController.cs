using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JI9J9A_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        IOwnerLogic logic;

        public OwnerController(IOwnerLogic logic)
        {
            this.logic = logic;
        }

        // GET: api/<OwnerController>
        [HttpGet]
        public IEnumerable<Owner> ReadAll()
        {
            return this.logic.ReadAll();
        }

        // GET api/<OwnerController>/5
        [HttpGet("{id}")]
        public Owner Read(int id)
        {
            return this.logic.Read(id);
        }

        // POST api/<OwnerController>
        [HttpPost]
        public void Create([FromBody] Owner value)
        {
            this.logic.Create(value);
        }

        // PUT api/<OwnerController>/5
        [HttpPut]
        public void Update([FromBody] Owner value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<OwnerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
