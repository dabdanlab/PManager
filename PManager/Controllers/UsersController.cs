using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PManager.Entities;
using PManager.Services;
using PManager.APP_Start;
using PManager.Models;
using MongoDB.Driver;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace PManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserServices _userServices;

        public UsersController(UserServices userServices)
        {
            _userServices = userServices;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: CreateUser/Create
        [HttpPost]
        public IActionResult Register(UserModels userModels)
        {
            _userServices.Register(userModels);
            return null;
        }

    }
}

