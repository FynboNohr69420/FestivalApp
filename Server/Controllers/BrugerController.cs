using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Common.Model;
using static System.Net.WebRequestMethods;

namespace Server.Controllers
{
    // Angiver at klassen er en API-controller
    [ApiController]
    [Route("api/brugere")]
    public class BrugerController : ControllerBase
    {

        private IBruger myRepo; // En reference til IBruger interfacet 

        public BrugerController(IBruger myRepo)
        {
            this.myRepo = myRepo;
        }

        // Http GET Metode til at hente alle brugere 
        [HttpGet]
        public IEnumerable<Bruger> getAll()
        {
            Console.WriteLine("get ");
            return myRepo.getAll();
        }

        // Http GET Metode til at hente en specifik bruger baseret på email 
        [HttpGet]
        [Route("{email}")]
        public Bruger getSpecific(string email)
        {
            return myRepo.getSpecific(email);
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
        [Route("bruger/{brugerID}")] // Angiver, at denne metode skal matche en rute med en enkelt parametre "brugerID"
        public Bruger GetBruger(int brugerID) // Henter et enkelt Bruger-objekt fra vores repository baseret på den angivne brugerID.
        {
            Console.WriteLine("Bruger found OK");
            return myRepo.GetBruger(brugerID);
        }

        // En metode, der håndterer HTTP POST requests til /api/Booking/update
        [HttpPost] 
        [Route("update")]
        public void UpdateBruger(Bruger bruger)
        {
            Console.WriteLine("Updated" + bruger.ID); // Hvis succesfuld printer "updated" og brugerens ID i konsollen 

            myRepo.UpdateBruger(bruger);
        }

    }
}

