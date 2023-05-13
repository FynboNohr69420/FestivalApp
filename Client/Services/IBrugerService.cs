using System;
using Common.Model;
using Client;

namespace Client.Services
{
	public interface IBrugerService
	{
		public Task<IEnumerable<Bruger>> getAll();

        Task Add(Bruger bruger);
	}
}

