using Common.Model;
using System;
namespace Server.Repositories
{
    public interface IVagt
    {
        Vagt[] getAll();
        void AddVagt(Vagt vagt);
        void DeleteVagt(int ID);
        void UpdateVagt(Vagt vagt);
        Vagt GetVagt (int vagtID);
        Vagt GetSpecificVagt (int vagtID);
    }
}
