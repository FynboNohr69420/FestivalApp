using System;
using Common.Model;
namespace Server.Repositories
{
	public interface IBruger
	{
        Bruger[] getAll();
        void Add(Bruger bruger);
        IEnumerable<Bruger> getBruger();
        void Delete(string id);
    }
}

