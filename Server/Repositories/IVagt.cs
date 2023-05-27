using Common.Model;
using System;
namespace Server.Repositories
{
    public interface IVagt
    {
        Vagt[] getAll();
        Vagt[] getAvailable(int brugerid);

        Kategori[] getAllKategori();
        void AddVagt(Vagt vagt);
        void DeleteVagt(int ID);
        void UpdateVagt(Vagt vagt);
        Vagt GetVagt(int vagtID);
        public void TagVagt(Vagt vagt, int bruger);
        public void AfmeldVagt(Vagt vagt, int bruger);

        public Task ToggleLockStatus(Vagt vagt, bool currentlockstatus);

        Vagt[] getAllMine(int b_id);
    }
}
