using Microsoft.AspNetCore.Mvc;
using Server.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/vagter")]
    [ApiController]
    public class Vagter : ControllerBase
    {
        private IVagt myRepo;

        public Vagter(IVagt myRepo)
        {
            this.myRepo = myRepo;
        }
        // GET: api/<Vagter>
        [HttpGet]
        public IEnumerable<Vagter> Get()
        {
            Console.WriteLine("get ");
            return myRepo.getAll();
        }

        // GET api/<Vagter>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Vagter>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Vagter>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Vagter>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
