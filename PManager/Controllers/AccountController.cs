using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PManager.Models;
using PManager.Services;

namespace PManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountServices _accountServices;

        public ActionResult Index()
        {   
            return View();
        }

        public ActionResult Profile()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        private bool Validatelogin(string Username, string Password)
        {
            return true;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginInfromationModels models,string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (Validatelogin(models.Username, models.Password))
            {
                var claims = new List<Claim>
            {
                new Claim("user", models.Username),
                new Claim("password", models.Password)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "user", "password")));

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return Redirect("/Account/Profile");
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.Authentication.SignOutAsync("CookieAuthentication");
            return Redirect("Index");
        }
    }
}