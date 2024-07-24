using BackendAPI.Models;
using BackendAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BackendAPI.Repository.WebApiHelper;

namespace BackendAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BpkbController : ControllerBase
    {
        private readonly IRepositoryBpkb _repositoryBpkb;
        public BpkbController(IRepositoryBpkb repositoryBpkb) {
            _repositoryBpkb = repositoryBpkb;
        }
        [HttpPost]
        public async Task<ApiResponseObj> InsertUpdateBpkb([FromBody] TrBpkb bpkb)
        {
            try
            {
                var checkId = await _repositoryBpkb.GetTaskByIdAsync(bpkb.Agreementnumber);
                if (checkId == null)
                {
                    await _repositoryBpkb.AddTaskAsync(bpkb);
                }
                else
                {
                    await _repositoryBpkb.UpdateTaskAsync(bpkb.Agreementnumber, bpkb);
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
        public async Task<ApiResponseObj>DeleteBpkb(string uId)
        {

            try
            {
                var checkId = await _repositoryBpkb.GetTaskByIdAsync(uId);
                if (checkId == null)
                {
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Data tidak ditemukan!",
                        status = false
                    };
                }
                else
                {
                    await _repositoryBpkb.DeleteTaskAsync(uId);
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Sukses Menghapus Data!",
                        status = true
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
        [HttpGet]
        public async Task<ApiResponseObj> GetById(string uId)
        {
            try
            {
                var checkId = await _repositoryBpkb.GetTaskByIdAsync(uId);
                if (checkId == null)
                {
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Data masih kosong mohon masukan data terlebih dahulu!",
                        status = true
                    };
                }
                else
                {
                    return new ApiResponseObj
                    {
                        data = checkId,
                        message = "Sukses Menampilkan data!",
                        status = true
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
        [HttpGet]
        public async Task<ApiResponseObj> GetDataLocation()
        {
            try
            {
                var checkId = await _repositoryBpkb.GetDropdown();
                if (checkId == null)
                {
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Data location kosong!!",
                        status = true
                    };
                }
                else
                {

                    return new ApiResponseObj
                    {
                        data = checkId,
                        message = "Sukses Menampilkan Data!",
                        status = true
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
        [HttpGet]
        public async Task<ApiResponseObj> GetAllData()
        {
            try
            {
                var checkId = await _repositoryBpkb.GetTasksAsync();
                if (checkId == null)
                {
                    return new ApiResponseObj
                    {
                        data = null,
                        message = "Data BPKB kosong!!",
                        status = true
                    };
                }
                else
                {

                    return new ApiResponseObj
                    {
                        data = checkId,
                        message = "Sukses Menampilkan Data!",
                        status = true
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
