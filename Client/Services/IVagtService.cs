using Common.Model;
using Client;
using System;

namespace Client.Services
{
    public interface IVagtService
    {
        Task<IEnumerable<Vagt>> getAll();
        Task<Vagt> GetVagt(int id);
        Task AddVagt(Vagt vagt);
        public Task<Vagt> UpdateVagt(Vagt vagt);

        public void 
    }
}
