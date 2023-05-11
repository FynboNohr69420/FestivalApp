using System;
using System.Net.Http.Json;
using Client.Pages;
using Common.Model;
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
            var bruger = await http.GetFromJsonAsync<Bruger[]>("api/bruger");

            return bruger;

        }

        public async Task Add(Bruger bruger)
        {
            await http.PostAsJsonAsync<Bruger>("api/bruger", bruger);
        }

    }
}