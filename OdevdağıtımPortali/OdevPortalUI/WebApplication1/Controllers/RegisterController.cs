using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RegisterController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RegisterModel model)
        {
            using (var httpClient = new HttpClient())
            {
                string apiUrl = "https://localhost:7295/api/Auth/Register";

                var dataForm = new
                {
                    adiSoyadi = model.adiSoyadi,
                    numarasi = model.numarasi,
                    email = model.email,
                    sifre = model.sifre,
                    dersId = model.dersId,
                    roleName = model.roleName,
                };

                string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dataForm);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Redirect("/Giris/Index");
                }
                else
                {
                    string errorResponseBody = await response.Content.ReadAsStringAsync();
                    ViewBag.message = errorResponseBody.ToString();
                    return View();
                }
            }
        }
    }
}
