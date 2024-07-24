using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FrontEnd.Models;
namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _httpClient;
        public AccountController() {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7035/");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Users/Login", jsonContent);
                var result = JsonConvert.DeserializeObject<ApiResponseObj>(await response.Content.ReadAsStringAsync());
                if (result.status != false)
                {
                    HttpContext.Session.SetString("username", model.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Invalid username or password.";
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
