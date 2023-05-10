using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Shared.Model;

namespace Server.Controllers
{
    // Angiver at klassen er en API-controller
    [ApiController]
    // Angiver baseruten for controllerens handlinger
    [Route("api/Frivillig")]
    public class FrivilligController : ControllerBase
    {
        // En tom liste til senere brug
        private static List<Frivillig> mFrivillige = new List<Frivillig>()
        {
            // Der er ingen shelters i listen, så den er tom
        };

        // En reference til databasen gennem vores repository interface
        private IFrivillig myRepo;

        // Konstruktør for controlleren, der tager imod en IBooking instans
        public FrivilligController(IFrivillig myRepo)
        {
            // Gemmer referencen til databasen i dette objekt
            this.myRepo = myRepo;
        }

        // En metode, der håndterer HTTP GET requests til /api/Booking
        [HttpGet]
        public IEnumerable<Frivillig> GetFrivillige()
        {
            // Skriver en besked til konsollen
            Console.WriteLine("get ");
            // Returnerer alle bookings i databasen gennem vores repository
            return myRepo.GetFrivillige();
        }

        // En metode, der håndterer HTTP POST requests til /api/Booking
        [HttpPost]
        public void AddFrivillig(Frivillig frivillig)
        {
            // Skriver en besked til konsollen med bookingens ID
            Console.WriteLine("post " + frivillig.Id);
            // Tilføjer bookingen til databasen gennem vores repository
            myRepo.AddFrivillig(frivillig);
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

