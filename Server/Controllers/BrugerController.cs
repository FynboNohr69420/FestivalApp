using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Common.Model;

namespace Server.Controllers
{
    // Angiver at klassen er en API-controller
    [ApiController]
    // Angiver baseruten for controllerens handlinger
    [Route("api/brugere")]
    public class BrugerController : ControllerBase
    {

        private IBruger myRepo;

        public BrugerController(IBruger myRepo)
        {
            this.myRepo = myRepo;
        }

        [HttpGet]
        public IEnumerable<Bruger> getAll()
        {
            Console.WriteLine("get ");
            return myRepo.getAll();
        }

        // En metode, der håndterer HTTP POST requests til /api/Booking
        [HttpPost]
        public void Add(Bruger bruger)
        {
            // Skriver en besked til konsollen med bookingens ID
            Console.WriteLine("post " + bruger.ID);
            // Tilføjer bookingen til databasen gennem vores repository
            myRepo.Add(bruger);
        }

        // En metode, der håndterer HTTP DELETE requests til /api/Booking/{Id}
        [HttpDelete]
        [Route("{Id}")]
        public void DeleteBruger(int Id)
        {
            // Skriver en besked til konsollen om at bookingen er blevet slettet
            Console.WriteLine("Deleted");
            // Sletter bookingen fra databasen gennem vores repository
            myRepo.DeleteBruger(Id);
        }

        [HttpGet] // Angiver, at denne metode skal køre, når en HTTP GET-anmodning modtages.
        [Route("GetBruger/{brugerID}")] // Angiver, at denne metode skal matche en rute med en enkelt parametre "shelterId"
        public Bruger GetBruger(int brugerID) // Henter et enkelt Shelter-objekt fra vores repository baseret på den angivne shelterId.
        {
            Console.WriteLine("Bruger found OK");
            return myRepo.GetBruger(brugerID);
        }

        //[HttpPut]
        //public async Task<ActionResult<Bruger>> UpdateBruger(Bruger bruger)
        //{
        //    try
        //    {
        //        var BrugerToUpdate = await bruger.GetEmployee(id);

        //        if (employeeToUpdate == null)
        //        {
        //            return NotFound($"Employee with Id = {id} not found");
        //        }

        //        return await employeeRepository.UpdateEmployee(employee);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error updating data");
        //    }
        //}

    }
}

