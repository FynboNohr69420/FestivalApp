using System;
using Common.Model;

namespace Server.Repositories
{
	public class FrivilligeRepositoryInMemory : IFrivillig
	{
        private static List<Frivillig> mFrivillig = Frivillig.ToList();

        public FrivilligeRepositoryInMemory()
        {
        }

        public void Add(Frivillig frivillig)
        {
            mFrivillig.Add(frivillig);
        }

        public Frivillig[] getAll()
        {
            return mFrivillig.ToArray();
        }
    }
}

