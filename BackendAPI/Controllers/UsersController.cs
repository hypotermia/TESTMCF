using BackendAPI.Models;
using BackendAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackendAPI.Repository.WebApiHelper;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryUsers _iUsersRepo;
        public UsersController(IRepositoryUsers iUsersRepo) {
            _iUsersRepo = iUsersRepo;
        }
        [HttpPost]
        public async Task<ApiResponseObj> InsertUsers([FromBody] MsUser user)
        {
            try
            {
                var checkAvail = _iUsersRepo.CheckUsernameAvail(user.UserNames);
                if (checkAvail == true) {
                    await _iUsersRepo.AddTaskAsync(user);
                }
                else
                {
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Username telah terdaftar !!",
                        status = true
                    };
                }

                return new ApiResponseObj
                {
                    data = null,
                    message = "Success add/update Data!",
                    status = true
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseObj
                {
                    data = null,
                    message = ex.Message,
                    status = false
                };
            }
        }
        [HttpPost]
        public async Task<ApiResponseObj> Login([FromBody] Login user)
        {
            try
            {
                var checkAvail = _iUsersRepo.CheckUsernameAvail(user.username);
                if (checkAvail == true)
                {
                    var data = await _iUsersRepo.GetTaskByIdAsync(user.username, user.password);
                    if (data != null) {
                        HttpContext.Session.SetString("username", user.username);
                        HttpContext.Session.SetString("isLoggedIn", "true");
                        return new ApiResponseObj
                        {
                            data = data,
                            message = "Login Success!!",
                            status = true
                        };
                    }
                    else
                    {
                        return new ApiResponseObj
                        {
                            data = null,
                            message = "Invalid username or password.",
                            status = false
                        };
                    }

                }
                else
                {
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Username/Password salah!!",
                        status = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new ApiResponseObj
                {
                    data = null,
                    message = ex.Message,
                    status = false
                };
            }
        }

    }
}
