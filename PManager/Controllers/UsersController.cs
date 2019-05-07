using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PManager.Entities;
using PManager.Services;
using PManager.APP_Start;

namespace PManager.Controllers
{
    public class UsersController : Controller
    {
        private IUserService userService;
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();        }
    }

    [HttpPost]
    public IActionResult Register(RegisterModel model)
    {
        
    }
}

