using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Server.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [ApiController]
    [Route("api/frivillig")]
    public class FrivilligController : ControllerBase
    {
     

        private IFrivillig myRepo;
        private IEnumerable<Frivillig> frivillig;

        public FrivilligController(IFrivillig myRepo)
        {
            this.myRepo = myRepo;
        }

        [HttpGet]
        public IEnumerable<Frivillig> Get()
        {
            return myRepo.getAll();
        }

        [HttpGet]
        [Route("mock")]
        public IEnumerable<Frivillig> GetMock()
        {
            return frivillig;
        }

        [HttpPost]
        public void Add(Frivillig frivillig)
        {
            myRepo.Add(frivillig);
        }



    }
}