using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using PManager.Models;
using PManager.Services;
using MongoDB.Driver.Linq;

namespace PManager.Controllers
{
    public class AccountController : Controller
    {

        private readonly IMongoCollection<RegisterModels> registerCollection;
        private readonly IConfiguration _configuration;


        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
            {
                var client = new MongoClient("mongodb://localhost:27017");
                IMongoDatabase db = client.GetDatabase("PManager");
                this.registerCollection = db.GetCollection<RegisterModels>("AccDb");
            }
        }

        public IActionResult Index()
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
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool Validatelogin(string email, string password)
        {
            return true;
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Login(RegisterModels models)
        {

            if (!string.IsNullOrEmpty(models.Email) && string.IsNullOrEmpty(models.Password))
            {
                return RedirectToAction("Login");
            }

            if (!ModelState.IsValid)
            {
                var W = registerCollection.AsQueryable<RegisterModels>().Where(w => w.Email == models.Email && w.Password == models.Password).FirstOrDefault();
                try
                {
                    if (models.Email == W.Email && models.Password == W.Password)
                    {

                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, models.Email )};
                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(principal));
                        return RedirectToAction("Index", "Account");
                    }
                    else
                    {
                        return View("Login");
                    }
                }
                catch (NullReferenceException)
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác !");
                    return View("Login");
                }
            }
            else
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác !");
            }
            return View("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                Response.Cookies.Delete(".AspNetCore.CookieAuthentication");

            }

            return RedirectToAction("Index", "Home");
        }

        // GET: Profile
        [HttpGet]
        public ActionResult Profile(string email)
        {
            var em = registerCollection.AsQueryable<RegisterModels>().Where(e => e.Email == email).FirstOrDefault();
            return View(em);
        }

        public ActionResult Update(string email)
        {
            var em = registerCollection.AsQueryable<RegisterModels>().Where(e => e.Email == email).FirstOrDefault();
            return View(em);
        }

        [HttpPost]
        public ActionResult Update(RegisterModels models)
        {
            try
            {
                // TODO: Add update logic here
                var filter = Builders<RegisterModels>.Filter.Eq("Email", models.Email);
                var update = Builders<RegisterModels>.Update
                    .Set("Email", models.Email)
                    .Set("Fullname", models.Fullname)
                    .Set("Phone", models.Phone)
                    .Set("Adress", models.Adress)
                    .Set("Birthday", models.Birthday);
                var result = registerCollection.UpdateOne(filter, update);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}