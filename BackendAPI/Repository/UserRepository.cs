using BackendAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Repository
{
    public class UserRepository : IRepositoryUsers
    {
        private readonly MasterDBContext _dbContext;
        public UserRepository(MasterDBContext context)
        {
            _dbContext = context;
        }

        public async Task AddTaskAsync(MsUser user)
        {
            var newUsers = new MsUser
            {
                Id = Guid.NewGuid(),
                UserNames = user.UserNames,
                Passwords = user.Passwords,
                Isactive = true
            };
            _dbContext.MsUsers.Add(newUsers);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<MsUser> GetTaskByIdAsync(string username , string password)
        {
            var users = await _dbContext.MsUsers.FirstOrDefaultAsync(u => u.UserNames == username && u.Passwords == password && u.Isactive == true);

            if (users == null)
            {
                return null;
            }

            return new MsUser
            {
                Id = users.Id,
                UserNames = users.UserNames,
                Passwords = users.Passwords,
                Isactive = users.Isactive
            };
        }
        public Boolean CheckUsernameAvail(string username)
        {
            var users = _dbContext.MsUsers.FirstOrDefaultAsync(u => u.UserNames == username);

            if (users != null)
            {
                return true;
            }
            return false;
        }
    }
}
