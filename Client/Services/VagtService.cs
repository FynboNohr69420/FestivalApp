using System;
using System.Net.Http.Json;
using Client.Pages;
using Common.Model;
using static System.Net.WebRequestMethods;

namespace Client.Services
{
    public class VagtService : IVagtService
    {

        private Vagt[]? vagtlist = new Vagt[0];

        HttpClient http;
        public VagtService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<IEnumerable<Vagt>> getAll()
        {
            var bruger = await http.GetFromJsonAsync<Bruger[]>("api/brugere");

            return bruger;

        }

        public async Task Add(Vagt vagt)
        {
            await http.PostAsJsonAsync<Bruger>("https://localhost:7004/api/vagter", bruger); // Sender en POST request med booking som JSON payload til API'en
            Console.WriteLine("klient: add " + vagt.ID + vagt.Navn); // Udskriver informationer om den nye booking i konsollen//
            bruger = new();

        }
    }
}