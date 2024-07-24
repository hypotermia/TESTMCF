using BackendAPI.Models;

namespace BackendAPI.Repository
{
    public interface IRepositoryUsers
    {
        Task<MsUser> GetTaskByIdAsync(string username , string password);
        Boolean CheckUsernameAvail(string username);
        Task AddTaskAsync(MsUser user);

    }
}
