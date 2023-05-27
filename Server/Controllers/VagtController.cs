﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Common.Model;
using static System.Net.WebRequestMethods;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [ApiController]
    [Route("api/vagter")]
    public class VagtController : ControllerBase
    {
        private IVagt myRepo;

        public VagtController(IVagt myRepo)
        {
            this.myRepo = myRepo;
        }
        // GET: api/<Vagter>
        [HttpGet]
        public IEnumerable<Vagt> GetVagter()
        {
            Console.WriteLine("get ");
            return myRepo.getAll();
        }

        [HttpGet]
        [Route("kategorier")]
        public IEnumerable<Kategori> GetAllKategori(int b_id)
        {
            Console.WriteLine("get ");
            return myRepo.getAllKategori();
        }

        [HttpGet]
        [Route("avail/{brugerid}")]
        public IEnumerable<Vagt> GetAvailable(int brugerid)
        {
            Console.WriteLine("get ");
            return myRepo.getAvailable(brugerid);
        }

        [HttpGet]
        [Route("spec/{b_id}")]
        public IEnumerable<Vagt> GetAllMine(int b_id)
        {
            Console.WriteLine("get ");
            return myRepo.getAllMine(b_id);
        }


        // En metode, der håndterer HTTP POST requests til /api/Booking
        [HttpPost]
        [Route("ny")]
        public void AddVagt(Vagt vagt)
        {
            // Skriver en besked til konsollen med bookingens ID
            Console.WriteLine("post " + vagt.ID);
            // Tilføjer bookingen til databasen gennem vores repository
            myRepo.AddVagt(vagt);
        }

        [HttpPost]
        [Route("tag/{bruger}")]
        public void TagVagt(Vagt vagt, int bruger)
        {
            myRepo.TagVagt(vagt, bruger);
        }
        [HttpPut]
        [Route("afmeld/{bruger}")]
        public void AfmeldVagt(Vagt vagt, int bruger)
        {
            myRepo.AfmeldVagt(vagt, bruger);
        }

        // En metode, der håndterer HTTP DELETE requests til /api/Booking/{Id}
        [HttpDelete]
        [Route("{Id}")]
        public void DeleteVagt(int ID)
        {
            // Skriver en besked til konsollen om at bookingen er blevet slettet
            Console.WriteLine("Deleted");
            // Sletter bookingen fra databasen gennem vores repository
            myRepo.DeleteVagt(ID);
        }

        [HttpGet] // Angiver, at denne metode skal køre, når en HTTP GET-anmodning modtages.
        [Route("vagter/{vagtID}")] // Angiver, at denne metode skal matche en rute med en enkelt parametre "shelterId"
        public Vagt GetVagt(int vagtID) // Henter et enkelt Shelter-objekt fra vores repository baseret på den angivne shelterId.
        {
            Console.WriteLine("Bruger found OK");
            return myRepo.GetVagt(vagtID);
        }

        [HttpPut]
        public void UpdateBruger(Vagt vagt)
        {
            Console.WriteLine("Updated" + vagt.ID);

            myRepo.UpdateVagt(vagt);
        }

        [HttpPut]
        [Route("lock/{currentlockstatus}")]
        public async Task ToggleLockStatus(Vagt vagt, bool currentlockstatus)
        {
            await myRepo.ToggleLockStatus(vagt, currentlockstatus);
        }
    }
}
