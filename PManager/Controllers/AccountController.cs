using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PManager.Models;
using PManager.Services;

namespace PManager.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountServices;
        private readonly AccountServices accountServices;
        private readonly UserServices userServices;
        private readonly IMongoCollection<RegisterModels> registerCollection;
        private readonly IConfiguration _configuration;

        public AccountController(IAccountServices accountServices, IConfiguration configuration)
        {
            _accountServices = accountServices;
            _configuration = configuration;

            {
                var client = new MongoClient("mongodb://localhost:27017");
                IMongoDatabase db = client.GetDatabase("PManager");
                this.registerCollection = db.GetCollection<RegisterModels>("AccDb");
            }
        }

        public ActionResult Index()
        {   
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModels models)
        {
            registerCollection.InsertOne(models);
            ViewBag.Message = "Employee added successfully!";
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModels
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModels models)
        {
            if (ModelState.IsValid)
            {
                var (isValid, user) = await _accountServices.Validate(models.Email, models.Password);
                if (isValid)
                {
                    await LoginAsync(user);
                    if (Validatelogin(models.ReturnUrl))
                    {
                        return Redirect(models.ReturnUrl);
                    }

                    return RedirectToAction("Login", "Account");
                }

                ModelState.AddModelError("InvalidCredentials", "Invalid credentials.");
            }

            return View(models);
        }


        private bool Validatelogin(string ReturnUrl)
        {
            return !string.IsNullOrWhiteSpace(ReturnUrl) && Uri.IsWellFormedUriString(ReturnUrl, UriKind.Relative);
        }

        [HttpPost]
        public async Task LoginAsync(LoginModels models)
        {

            var properties = new AuthenticationProperties { };
            {
                var claims = new List<Claim>
                {
                    new Claim("Email", models.Email),
                    new Claim("Password", models.Password)
                };

                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "Cookies", "Email", "password")));
            }
        }

        public async Task<IActionResult> Logout(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!_configuration.GetValue<bool>("Account:ShowLogoutPrompt"))
            {
                return await Logout();
            }

            return View();
        }


        public IActionResult Cancel(string returnUrl)
        {
            if (Validatelogin(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}