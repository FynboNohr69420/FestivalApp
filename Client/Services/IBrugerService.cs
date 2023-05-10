using System;
using Common.Model;

namespace Client.Services
{
	public interface IFrivilligService
	{
		Task<IEnumerable<Frivillig>> getAll();

		Task AddFrivillig(Frivillig frivillig);
	}
}

