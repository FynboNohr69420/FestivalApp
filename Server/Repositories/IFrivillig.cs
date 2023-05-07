using System;
using Server.Data;
namespace Server.Repositories
{
	public interface IFrivillig
	{
        Frivillig[] getAll();
        void Add(Frivillig frivillig);
    }
}

