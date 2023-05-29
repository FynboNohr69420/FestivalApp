using System;
using System.Net.Http;
using System.Net.Http.Json;
using Client.Pages;
using Common.Model;
using Microsoft.AspNetCore.Components;

namespace Client.Services
{
    public class BrugerService : IBrugerService // Klassen arver fra IBrugerService interfacet 
    {
        HttpClient http;
        public BrugerService(HttpClient http) // Sørger for at klassen kan kalde og hente Http requests 
        {
            this.http = http;
        }

        public async Task<IEnumerable<Bruger>> getAll() // getAll laver en http get request til /api/brugere og henter alle informationer på alle brugere
        {
            var brugerlist = await http.GetFromJsonAsync<Bruger[]>("https://servercalendarica.azurewebsites.net/api/brugere");
            return brugerlist;
        }

        public async Task<Bruger> GetBruger(int id) // Henter information på den enkelte bruger, baseret på deres id
        {
            return await http.GetFromJsonAsync<Bruger>($"https://servercalendarica.azurewebsites.net/api/brugere/bruger/{id}");
        }

        public async Task Add(Bruger bruger) // Tilføjer en bruger 
        {
            await http.PostAsJsonAsync<Bruger>("https://servercalendarica.azurewebsites.net/api/brugere", bruger); // Sender en POST request med booking som JSON payload til API'en
            Console.WriteLine("klient: add " + bruger.Fornavn + bruger.Efternavn); // Udskriver informationer om den nye booking i konsollen//
        }

        public async Task UpdateBruger(Bruger bruger) // Opdater en bruger 
        {
            await http.PostAsJsonAsync<Bruger>("https://servercalendarica.azurewebsites.net/api/brugere/update", bruger);
            Console.WriteLine("klient: add " + bruger.Fornavn + bruger.Efternavn); 
            bruger = new();
        }

        public async Task<Bruger> getSpecific(string email) // Henter en specifik bruger baseret på email
        {
            var result = await http.GetFromJsonAsync<Bruger>("https://servercalendarica.azurewebsites.net/api/brugere/" + email);

            return result;
        }
        public void DeleteBruger(int ID) // Sletter en bruger baseret på et specifikt ID
        {
            http.DeleteFromJsonAsync<Bruger>($"https://servercalendarica.azurewebsites.net/api/brugere/{ID}");
            Console.WriteLine("Klient: deleted" +  ID);
        }


    }
}