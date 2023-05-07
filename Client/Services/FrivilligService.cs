using System;
using System.Net.Http.Json;
using Client.Pages;
using Client.Services;
using static System.Net.WebRequestMethods;

namespace Client.Services
{
    public class FrivilligService : IFrivilligService
    {
        HttpClient http;
        public FrivilligService(HttpClient http)
        {
            this.http = http;
        }

        public async Task<IEnumerable<Frivillig>> getAll()
        {
            var frivillige = await http.GetFromJsonAsync<Frivillig[]>("api/frivillig");

            return frivillige;

        }

        public async Task AddFrivillig(Frivillig frivillig)
        {
            await http.PostAsJsonAsync<Frivillig>("api/frivillig", frivillig);
        }

    }
}