using System;
using Client.Context;
using Microsoft.EntityFrameworkCore;

namespace Client.Data
{
	public class FrivilligService
	{
		private readonly ApplicationDbContext _applicationDbContext;

		public FrivilligService(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
		}

		//Henter alle frivillige
		public async Task<List<Frivillig>> GetAllFrivillig()
		{
			return await _applicationDbContext.Frivillig.ToListAsync();
		}

		//Tilføj ny frivillig
        public async Task<bool> AddNewFrivillig(Frivillig frivillig)
        {
			await _applicationDbContext.Frivillig.AddAsync(frivillig);
			await _applicationDbContext.SaveChangesAsync();
			return true;
        }

		//Hent frivillig ud fra ID
        public async Task<Frivillig> AddNGetFrivilligById(int id)
        {
           
        }
    }
}

