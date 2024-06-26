using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class OdevController : Controller
    {
        public IActionResult Index()
        {
            using var httpClient = new HttpClient();
            var jwt = HttpContext.Session.GetString("jwt");
            var email = HttpContext.Session.GetString("email");
            var userId = HttpContext.Session.GetString("id");
            var role = HttpContext.Session.GetString("role");
            var ders = HttpContext.Session.GetInt32("dersId");
            ViewBag.jwt = jwt;
            ViewBag.userId = userId;
            ViewBag.role = role;
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
                var result = httpClient.GetAsync("api/Odevler?id=" + ders).Result;
                var jsonString = result.Content.ReadAsStringAsync().Result;
                var homework = JsonSerializer.Deserialize<List<OdevModel>>(jsonString);
                var models = homework.ToList();
                return View(models);
            }
        }
    }
}
