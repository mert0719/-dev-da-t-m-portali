using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GirisController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginModel model)
        {
            using (var httpClient = new HttpClient())
            {
                string apiUrl = "https://localhost:7295/api/Auth";

                var dataForm = new
                {
                    email = model.email,
                    password = model.password
                };

                string jsonData = JsonConvert.SerializeObject(dataForm);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);
                if (response.IsSuccessStatusCode)
                {

                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = Newtonsoft.Json.JsonConvert.DeserializeObject<SessionModel>(responseBody);
                    string jwtToken = responseObject.jwt;
                    int id = responseObject.id;
                    string email = responseObject.email;
                    string role = responseObject.role;
                    string adiSoyadi = responseObject.adiSoyadi;  
                    int dersId = responseObject.dersId;
                   
                    HttpContext.Session.SetString("jwt", jwtToken);
                    HttpContext.Session.SetString("id", id.ToString());
                    HttpContext.Session.SetString("email", email);
                    HttpContext.Session.SetString("role", role);
                    HttpContext.Session.SetString("adiSoyadi", adiSoyadi);
                    HttpContext.Session.SetInt32("dersId", dersId);

                    return Redirect($"/Odev/Index?dersId={dersId}");

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
