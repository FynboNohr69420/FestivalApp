using Common.Model;
using Client;
using System;

namespace Client.Services
{
    public interface IVagtService
    {
        Task<IEnumerable<Vagt>> getAll();

        Task AddVagt(Vagt vagt);
    }
}
