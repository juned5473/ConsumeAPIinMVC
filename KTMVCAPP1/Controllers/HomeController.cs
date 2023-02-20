using KTMVCAPP1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace KTMVCAPP1.Controllers
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
            return View();
        }


        public async Task<IActionResult> LoginUser(Employee emp)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent stringContent = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7127/api/Token", stringContent))
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if (token == "Invalid Credentials")
                    {
                        ViewBag.Message = "Incorrect UserId or Password!";
                        return Redirect("~/Home/Index");
                    }
                    HttpContext.Session.SetString("JWToken", token);
                }
                return Redirect("~/Consume/Index");

            }
        }


        public IActionResult Logoff()
        {
            HttpContext.Session.Clear(); //clear token 
            return Redirect("~/Home/Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
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