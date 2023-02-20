using KTMVCAPP1.Data;
using KTMVCAPP1.Models;
using KTMVCAPP1.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KTMVCAPP1.Controllers
{
    public class LoginController : Controller
    {
        /*private readonly IEmployeeRepository empRepository;

        public LoginController(IEmployeeRepository empRespositary)
        {
            empRepository = empRespositary;
        }


        public IActionResult Login(string Email, string Password)
        {
            var linq = from n in empRepository.GetAll()
                       where Email == n.Email && Password == n.Password
                       select n;
            if (linq.Count().Equals(0))
            {
                ViewData["ValidateMessage"] = "User not found";
                return View();
              
            }
            return RedirectToAction("Index", "Employees");
       
  
        }*/

        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Employees");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Employee adminlogin)
        {
            if (adminlogin.Email == "admin@gmail.com" && adminlogin.Password == "admin123")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier,adminlogin.Email),
                   /* new Claim("OtherPrperties","Example Role"),*/
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent =true,
                   
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Employees");
            }



            ViewData["ValidateMessage"] = "User not found";
            return View();
        }
    }
}
