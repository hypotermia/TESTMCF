using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public interface IRepositoryBpkb
    {
        Task<IEnumerable<TrBpkb>> GetTasksAsync(); 
        Task<TrBpkb> GetTaskByIdAsync(string uId);
        Task AddTaskAsync(TrBpkb bpkb);
        Task UpdateTaskAsync(string uId, TrBpkb trBpkb);
        Task DeleteTaskAsync(string uId);

        Task<IEnumerable<MsStorageLocation>> GetDropdown();
    }
}
