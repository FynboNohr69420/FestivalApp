using System;
using Common.Model;
using Client;

namespace Client.Services
{
	public interface IBrugerService
	{
		Task<IEnumerable<Bruger>> getAll();

		Task Add(Bruger bruger);
		Task<Bruger> getSpecific(string email);
	}
}

