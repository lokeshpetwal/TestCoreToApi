using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestCoreToApi.Models;

namespace TestCoreToApi.Controllers
{    [Authorize]
    public class EmployeeController : Controller
    {
        Uri apuri = new Uri("http://localhost:7447/Api/Controller/");
        HttpClient Client;

        public EmployeeController()
        {
            Client = new HttpClient();
            Client.BaseAddress = apuri;
        }

        public IActionResult ShowEmployee()
        {

            List<EmployeeModel> obj = new List<EmployeeModel>();

            HttpResponseMessage lisemp = Client.GetAsync(Client.BaseAddress + "Show/Employee").Result;

            if (lisemp.IsSuccessStatusCode)
            {
                string data = lisemp.Content.ReadAsStringAsync().Result;
                var emp = JsonConvert.DeserializeObject<List<EmployeeModel>>(data);

                obj = emp;

            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(EmployeeModel emp)
        {
            string data = JsonConvert.SerializeObject(emp);
            StringContent content = new StringContent(data, Encoding.UTF8, "Application/Json");

            HttpResponseMessage lis = Client.PostAsync(Client.BaseAddress + "Post/Employee", content).Result;

            if (lis.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowEmployee");
            }
            return RedirectToAction("ShowEmployee");
        }

        public IActionResult Delete(int id)
        {
            var res = Client.DeleteAsync(Client.BaseAddress + "del/Employee" + '?' + "Id" + "=" + id.ToString()).Result;

            return RedirectToAction("ShowEmployee");
        }

        public IActionResult Edit(int id)
        {
            TempData["edit"] = "edit";
            var res = Client.GetAsync(Client.BaseAddress + "Edit/Employee" + '?' + "Id" + "=" + id.ToString()).Result;
            string data = res.Content.ReadAsStringAsync().Result;
            var emp = JsonConvert.DeserializeObject<EmployeeModel>(data);

            return View("AddEmployee", emp);

        }
        [AllowAnonymous]
        public IActionResult DashBoard()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn()
        {
            return View();
        }
           [HttpPost]
            public IActionResult LogIn(LoginModel log)
        {
            string data = JsonConvert.SerializeObject(log);
            StringContent content = new StringContent(data, Encoding.UTF8, "Application/Json");

            HttpResponseMessage lis = Client.PostAsync(Client.BaseAddress + "user/login", content).Result;

            if (lis.IsSuccessStatusCode)
            {
                string data1 = lis.Content.ReadAsStringAsync().Result;
                var emp = JsonConvert.DeserializeObject<LoginModel>(data1);

                if(emp.Email== "email is not found")
                {
                    TempData["email"] = "Email not found";
                }
                else
                {
                    if (emp.Email == log.Email && emp.Password == emp.Password)
                    {
                        var claims = new[] { new Claim(ClaimTypes.Email, emp.Email) };
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = true
                        };
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                        return RedirectToAction("DashBoard");
                    }
                    else
                    {
                        TempData["password"] = "Invalid password";
                    }
                }
              
            }
            return View();

        }

        //public IActionResult Logout()
        //{
           
        //}




    }
}
