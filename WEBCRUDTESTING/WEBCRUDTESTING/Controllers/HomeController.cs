using System;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using WEBCRUDTESTING.Models;

namespace WEBCRUDTESTING.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiUrl = "https://localhost:7265/api/ManagementProduct/";

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");

            if (string.IsNullOrEmpty(token))
            {
                // Token tidak ditemukan, redirect ke login
                return RedirectToAction("Index", "Login");
            }

            await doSearch(); 
            return View();
        }

        public async Task doSearch(string keyword = null)
        {
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response;

            if (keyword == null)
            {
                response = await client.GetAsync(_apiUrl + "AllProduct");
            }
            else
            {
                response = await client.GetAsync(_apiUrl + "AllProduct?name=" + keyword);
            }
            

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                List<ProductModel> data = JsonConvert.DeserializeObject<List<ProductModel>>(content);

                ViewData["products"] = data;
            }
            else
            {
                // Tangani error jika request gagal
                ViewData["products"] = new List<ProductModel>();
            }
        }


        [HttpPost]

        public async Task<IActionResult> Index(ProductModel product) //index via search keyword product name
        {
            await doSearch(product.name);

            return View("Index");
        }

    }

}
