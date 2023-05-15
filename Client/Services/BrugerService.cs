using System;
using System.Net.Http;
using System.Net.Http.Json;
using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Client.Pages;
using Common.Model;
using Microsoft.AspNetCore.Components;
using static System.Net.WebRequestMethods;

namespace Client.Services
{
    public class BrugerService : IBrugerService
    {
      

        HttpClient http;
        public BrugerService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<IEnumerable<Bruger>> getAll()
        {
            var brugerlist = await http.GetFromJsonAsync<Bruger[]>("https://localhost:7004/api/brugere");
            return brugerlist;
        }

        public async Task<Bruger> GetBruger(int id)
        {
            return await http.GetFromJsonAsync<Bruger>($"https://localhost:7004/api/brugere/bruger/{id}");
        }

        public async Task Add(Bruger bruger)
        {
            await http.PostAsJsonAsync<Bruger>("https://localhost:7004/api/brugere", bruger); // Sender en POST request med booking som JSON payload til API'en
            Console.WriteLine("klient: add " + bruger.Fornavn + bruger.Efternavn); // Udskriver informationer om den nye booking i konsollen//
            bruger = new();

        }

        public async Task UpdateBruger(Bruger bruger)
        {
            await http.PostAsJsonAsync<Bruger>("https://localhost:7004/api/brugere/update", bruger);
            Console.WriteLine("klient: add " + bruger.Fornavn + bruger.Efternavn); 
            bruger = new();
        }

        public async Task<Bruger> getSpecific(string email)
        {
            var result = await http.GetFromJsonAsync<Bruger>("https://localhost:7004/api/brugere/" + email);

            return result;

        }
        public void GetUrlID()
        {
            var uri = new Uri(NavigationManager.Uri); // Opretter et Uri objekt med URL'en fra NavigationManager
            var queryParameters = System.Web.HttpUtility.ParseQueryString(uri.Query); // Bruger HttpUtility klassen til at parse query stringen i URL'en
            UrlId = Int32.Parse(queryParameters.Get("id")); // Henter ID'en fra URL'en og gemmer den i UrlId variablen
        }
    }
}