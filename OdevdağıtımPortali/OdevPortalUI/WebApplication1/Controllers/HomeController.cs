using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            using var httpClient = new HttpClient();
            var jwt = HttpContext.Session.GetString("jwt");
            var email = HttpContext.Session.GetString("email");
            var userId = HttpContext.Session.GetString("id");
            ViewBag.jwt = jwt;
            ViewBag.userId = userId;
            if (jwt == null)
            {
                return Redirect("/Giris/Index");
            }
            else
            {
                httpClient.BaseAddress = new Uri("https://localhost:7295/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var result = httpClient.GetAsync("api/Dersler").Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var lesson = JsonSerializer.Deserialize<List<DersModel>>(jsonString);
                var models = lesson.ToList();                
                return View(models);
            }
        }
        public IActionResult MyHomeWork()
        {
            using var httpClient = new HttpClient();
            var jwt = HttpContext.Session.GetString("jwt");
            var email = HttpContext.Session.GetString("email");
            var userId = HttpContext.Session.GetString("id");
            ViewBag.jwt = jwt;
            ViewBag.userId = userId;
            if (jwt == null)
            {
                return Redirect("/Giris/Index");
            }
            else
            {
                httpClient.BaseAddress = new Uri("https://localhost:7295/");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
                var result = httpClient.GetAsync("api/Odevler/UserByHomeWork?userId=" + userId).Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var lesson = JsonSerializer.Deserialize<List<OdevModel>>(jsonString);
                var models = lesson.ToList();
                return View(models);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
