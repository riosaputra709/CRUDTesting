using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using WEBCRUDTESTING.Models;

namespace WEBCRUDTESTING.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _loginApiUrl = "https://localhost:7265/api/Auth/login";

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            ViewData["pesanError"] = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login)
        {
            string username = login.username;
            string password = login.password;

            // Membuat model untuk dikirim ke API Login
            var loginModel = new
            {
                username = username,
                password = password
            };

            var jsonContent = new StringContent(
                JsonSerializer.Serialize(loginModel),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync(_loginApiUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {
                // Mengambil token JWT dari respons
                var token = await response.Content.ReadAsStringAsync();

                // Menyimpan token JWT ke session
                HttpContext.Session.SetString("Token", token);

                // Redirect ke halaman Home setelah login berhasil
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Menangani error jika login gagal
                ViewData["pesanError"] = "Invalid username or password.";
                return View("index");
            }
        }
    }

}
