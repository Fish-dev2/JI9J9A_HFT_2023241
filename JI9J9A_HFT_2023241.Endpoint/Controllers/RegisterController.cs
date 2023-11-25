using JI9J9A_HFT_2023241.Logic;
using JI9J9A_HFT_2023241.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JI9J9A_HFT_2023241.Endpoint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        IRegisterLogic logic;

        public RegisterController(IRegisterLogic logic)
        {
            this.logic = logic;
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
        }

        // PUT api/<RegisterController>/5
        [HttpPut("{id}")]
        public void Update(int id, [FromBody] Register value)
        {
            this.logic.Update(value);
        }

        // DELETE api/<RegisterController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            this.logic.Delete(id);
        }
    }
}
