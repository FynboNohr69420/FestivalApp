using System;
using System.Net.Http.Json;
using Client.Pages;
using Common.Model;
using static System.Net.WebRequestMethods;

namespace Client.Services
{
    public class BrugerService : IBrugerService
    {

        private Bruger[]? brugerlist = new Bruger[0];

        HttpClient http;
        public BrugerService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<IEnumerable<Bruger>> getAll()
        {
            var bruger = await http.GetFromJsonAsync<Bruger[]>("api/brugere");

            return bruger;

        }

        public async Task Add(Bruger bruger)
        {
            await http.PostAsJsonAsync<Bruger>("https://localhost:7004/api/brugere", bruger); // Sender en POST request med booking som JSON payload til API'en
            Console.WriteLine("klient: add " + bruger.Fornavn + bruger.Efternavn); // Udskriver informationer om den nye booking i konsollen//
            bruger = new();

        }
    }
}