
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using PManager.Entities;
using PManager.Services;

namespace PManager.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        private bool ValidateLogin(string UserName, string Password)
        {
            return true;
        }

        public async Task<IActionResult> Login(string UserName, string Password, string returnUrl = null)
        {
            if (!IsAuthenticated(UserName, Password))
                return View();

            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Cookie authentication demo"),
                new Claim(ClaimTypes.Email, UserName)
            };

            // create identity
            ClaimsIdentity identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                    scheme: "DemoSecurityScheme",
                    principal: principal,
                    properties: new AuthenticationProperties
                    {
                        //IsPersistent = true, // for 'remember me' feature
                        //ExpiresUtc = DateTime.UtcNow.AddMinutes(1)
                    });

            return Redirect("/");
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        private bool IsAuthenticated(string UserName, string Password)
        {
            return (UserName == "admin" && Password == "admin123");
        }
    }
}