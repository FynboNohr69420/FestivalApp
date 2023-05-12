using Common.Model;
using System;
namespace Server.Repositories
{
    public interface IVagt
    {
        Vagt[] getAll();
        void Add(Vagt vagt);
        void DeleteVagt(int ID);
        void UpdateVagt(Vagt vagt);
    }
}
