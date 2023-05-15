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
            var vagt = await http.GetFromJsonAsync<Vagt[]>("api/vagter");

            return vagt;

        }

        public async Task AddVagt(Vagt vagt)
        {
            await http.PostAsJsonAsync<Vagt>("https://localhost:7004/api/vagter/ny", vagt); // Sender en POST request med booking som JSON payload til API'en
            Console.WriteLine("klient: add " + vagt.ID + vagt.Navn); // Udskriver informationer om den nye booking i konsollen//
            vagt = new();
        }
    }
}