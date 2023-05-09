using System;
using Common.Model;
namespace Server.Repositories
{
	public interface IFrivillig
	{
        Frivillig[] getAll();
        void Add(Frivillig frivillig);
    }
}

