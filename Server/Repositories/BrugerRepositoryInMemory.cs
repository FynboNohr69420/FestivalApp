using System;
using Common.Model;

namespace Server.Repositories
{
	public class BrugerRepositoryInMemory : BrugerRepositorySQL
	{
        private static List<Bruger> mBruger = Bruger.ToList();

        public BrugerRepositoryInMemory()
        {
        }

        public void Add(Bruger bruger)
        {
            mBruger.Add(bruger);
        }

        public Bruger[] getAll()
        {
            return mBruger.ToArray();
        }
    }
}

