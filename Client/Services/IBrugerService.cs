using System;
using Common.Model;
using Client;

namespace Client.Services
{
	public interface IBrugerService
	{
		public Task<IEnumerable<Bruger>> getAll();

		Task Add(Bruger bruger);
		Task<Bruger> getSpecific(string email);
	}
        Task<Bruger> GetBruger(int id);

        Task Add(Bruger bruger);

        Task UpdateBruger(Bruger bruger);


    }
}

