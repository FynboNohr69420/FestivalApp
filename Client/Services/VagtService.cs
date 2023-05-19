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
            var vagt = await http.GetFromJsonAsync<Vagt[]>("https://localhost:7004/api/vagter");

            return vagt;
        }

        public async Task<IEnumerable<Vagt>> getAvailable(int brugerid)
        {
            var vagt = await http.GetFromJsonAsync<Vagt[]>($"https://localhost:7004/api/vagter/avail/{brugerid}");

            return vagt;
        }

        public async Task<IEnumerable<Vagt>> getAllMine(int b_id)
        {
            var vagt = await http.GetFromJsonAsync<Vagt[]>($"https://localhost:7004/api/vagter/spec/{b_id}");

            return vagt;
        }

        public async Task AddVagt(Vagt vagt)
        {
            await http.PostAsJsonAsync<Vagt>("https://localhost:7004/api/vagter/ny", vagt); // Sender en POST request med booking som JSON payload til API'en
            Console.WriteLine("klient: add " + vagt.ID + vagt.Navn); // Udskriver informationer om den nye booking i konsollen//
            vagt = new();
        }


        public async Task<Vagt> GetVagt(int id)
        {
            return await http.GetFromJsonAsync<Vagt>($"https://localhost:7004/api/vagter/vagter/{id}");
        }

        public async Task<Vagt> UpdateVagt(Vagt vagt)
        {
            await http.PutAsJsonAsync<Vagt>("https://localhost:7004/api/vagter", vagt);
            return vagt;
            vagt = new();

        }

        public void DeleteVagt(int ID)
        {
            http.DeleteFromJsonAsync<Vagt>($"https://localhost:7004/api/vagter/{ID}");
            Console.WriteLine("Klient: deleted" +  ID);
        }

        public async void TagVagt(Vagt vagt, int bruger)
        {
            await http.PostAsJsonAsync<Vagt>($"https://localhost:7004/api/vagter/tag/{bruger}", vagt); // Sender en POST request med booking som JSON payload til API'en
        }
        public async void AfmeldVagt(Vagt vagt, int bruger)
        {
            await http.PutAsJsonAsync<Vagt>($"https://localhost:7004/api/vagter/afmeld/{bruger}", vagt); // Sender en POST request med booking som JSON payload til API'en
        }
    }
}