using System;

namespace Client.Services
{
	public interface IFrivilligService
	{
		Task<IEnumerable<Frivillig>> getAll();

		Task AddFrivillig(Frivillig frivillig);
	}
}

