using Common.Model;
using Client;
using System;

namespace Client.Services
{
    public interface IVagtService
    {
        Task<IEnumerable<Vagt>> getAll();
        Task<IEnumerable<Vagt>> getAvailable(int brugerid);
        Task<Vagt> GetVagt(int id);
        Task AddVagt(Vagt vagt);
        Task<Vagt> UpdateVagt(Vagt vagt);
        public void DeleteVagt(int id);
        public void TagVagt(Vagt vagt, int bruger);
        public void AfmeldVagt(Vagt vagt, int bruger);
        Task<IEnumerable<Vagt>> getAllMine(int b_id);

    }
}
