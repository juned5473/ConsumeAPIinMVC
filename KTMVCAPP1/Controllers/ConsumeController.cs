using KTMVCAPP1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace KTMVCAPP1.Controllers
{
    public class ConsumeController : Controller
    {
        String baseURL = "https://localhost:7127/api/employee/";
        HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            var emps = await GetEmps();
            return View(emps);
        }
       
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Employee emp)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
            await client.PostAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult>Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<Employee>(jsonStr);

            if(res == null)
            {
                return NotFound();
            }

            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Employee emp)
        {
            if (id != emp.Id)
            {
                return NotFound();
            }
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var stringContent = new StringContent(JsonConvert.SerializeObject(emp), Encoding.UTF8, "application/json");
            await client.PutAsync(url, stringContent);

            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<Employee>(jsonStr);

            if (res == null)
            {
                return NotFound();
            }

            return View(res);
        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            await client.DeleteAsync(url);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL + id;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            string jsonStr = await client.GetStringAsync(url);

            var emp = JsonConvert.DeserializeObject<Employee>(jsonStr);

            if (emp == null)
            {
                return NotFound();
            }

            return View(emp);
        }






        [HttpGet]
        public async Task<List<Employee>>GetEmps()
        {
            var accessToken = HttpContext.Session.GetString("JWToken");
            var url = baseURL;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string jsonStr = await client.GetStringAsync(url);

            var res = JsonConvert.DeserializeObject<List<Employee>>(jsonStr).ToList();

            return res;
        }


    }
}
