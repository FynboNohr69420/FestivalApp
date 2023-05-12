using Common.Model;

namespace Client.Services
{
    public interface IVagtService
    {
        Task<IEnumerable<Vagt>> getAll();

        Task Add(Vagt vagt);
    }
}
