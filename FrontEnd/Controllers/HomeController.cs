using FrontEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger )
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7035/");
        }
        private async Task<IEnumerable<MsStorageLocation>> GetLocationsAsync()
        {
            var response = await _httpClient.GetStringAsync("api/Bpkb/GetDataLocation");
            var result = JsonConvert.DeserializeObject<ApiResponseObj>(response);
            return JsonConvert.DeserializeObject<IEnumerable<MsStorageLocation>>(result.data.ToString());
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync("/api/Bpkb/GetAllData");
            var result = JsonConvert.DeserializeObject<ApiResponseObj>(response);
            var bpkbList = JsonConvert.DeserializeObject<IEnumerable<TrBpkb>>(result.data.ToString());
            
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["Username"] = username;
            return View(bpkbList);
        }
        public async Task<IActionResult> Create()
        {
            var locations = await GetLocationsAsync();
            var viewModel = new BpkbViewModel
            {
                Bpkb = new TrBpkb(),
                Locations = locations
            };
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BpkbViewModel viewModel)
        {
            var username = HttpContext.Session.GetString("username");
            viewModel.Bpkb.CreatedBy = username;
                var jsonContent = new StringContent(JsonConvert.SerializeObject(viewModel.Bpkb), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Bpkb/InsertUpdateBpkb", jsonContent);
                var result = JsonConvert.DeserializeObject<ApiResponseObj>(await response.Content.ReadAsStringAsync());

                if (result.status)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError("", result.message);
            
            viewModel.Locations = await GetLocationsAsync();
            return View(viewModel);
        }
        public async Task<IActionResult> Edit(string id)
        {
            var response = await _httpClient.GetStringAsync($"/api/Bpkb/GetById?uId={id}");
            var result = JsonConvert.DeserializeObject<ApiResponseObj>(response);
            var bpkb = JsonConvert.DeserializeObject<TrBpkb>(result.data.ToString());

            var locations = await GetLocationsAsync();
            var viewModel = new BpkbViewModel
            {
                Bpkb = bpkb,
                Locations = locations
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BpkbViewModel viewModel)
        {
            var username = HttpContext.Session.GetString("username");
            viewModel.Bpkb.LastUpdatedBy = username;
            var jsonContent = new StringContent(JsonConvert.SerializeObject(viewModel.Bpkb), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/Bpkb/InsertUpdateBpkb", jsonContent);
                var result = JsonConvert.DeserializeObject<ApiResponseObj>(await response.Content.ReadAsStringAsync());

                if (result.status)
                    return RedirectToAction(nameof(Index));
                else
                    ModelState.AddModelError("", result.message);
            

            // Re-fetch locations if the model state is invalid or API call fails
            viewModel.Locations = await GetLocationsAsync();
            return View(viewModel);
        }
        public IActionResult Privacy()
        {
            var username = HttpContext.Session.GetString("username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            ViewData["Username"] = username;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _httpClient.PostAsync($"/api/Bpkb/DeleteBpkb?uId={id}", null);
            var result = JsonConvert.DeserializeObject<ApiResponseObj>(await response.Content.ReadAsStringAsync());

            if (result.status)
                return Json(new { success = true, message = result.message });
            else
                return Json(new { success = false, message = result.message });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
