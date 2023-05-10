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
    [Route("api/Bruger")]
    public class BrugerController : ControllerBase
    {
        // En tom liste til senere brug
        private static List<Bruger> mBruger = new List<Bruger>()
        {
            // Der er ingen shelters i listen, så den er tom
        };

        // En reference til databasen gennem vores repository interface
        private IBruger myRepo;

        // Konstruktør for controlleren, der tager imod en IBooking instans
        public BrugerController(IBruger myRepo)
        {
            // Gemmer referencen til databasen i dette objekt
            this.myRepo = myRepo;
        }

        // En metode, der håndterer HTTP GET requests til /api/Booking
        [HttpGet]
        public IEnumerable<Bruger> getBruger()
        {
            // Skriver en besked til konsollen
            Console.WriteLine("get ");
            // Returnerer alle bookings i databasen gennem vores repository
            return myRepo.getBruger();
        }

        // En metode, der håndterer HTTP POST requests til /api/Booking
        [HttpPost]
        public void Add(Bruger bruger)
        {
            // Skriver en besked til konsollen med bookingens ID
            Console.WriteLine("post " + bruger.Id);
            // Tilføjer bookingen til databasen gennem vores repository
            myRepo.Add(bruger);
        }

        // En metode, der håndterer HTTP DELETE requests til /api/Booking/{Id}
        [HttpDelete]
        [Route("{Id}")]
        public void Delete(string Id)
        {
            // Skriver en besked til konsollen om at bookingen er blevet slettet
            Console.WriteLine("Deleted");
            // Sletter bookingen fra databasen gennem vores repository
            myRepo.Delete(Id);
        }


    }
}

