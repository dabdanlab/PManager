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
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace PManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<RegisterModels> registerCollection;




        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
            {
                var client = new MongoClient("mongodb://localhost:27017");
                IMongoDatabase db = client.GetDatabase("PManager");
                this.registerCollection = db.GetCollection<RegisterModels>("AccDb");
            }
        }

        public ActionResult Index()
        {
            List<RegisterModels> w = registerCollection.AsQueryable<RegisterModels>().ToList();
            return View(w);
        }
        [HttpGet]
        // GET: Animals/Details/5
        public ActionResult Details(string id)
        {
            ObjectId oId = new ObjectId(id);
            RegisterModels models = registerCollection.Find(e => e.Id == oId).FirstOrDefault();
            return View(models);
        }

        public ActionResult Edit(string id)
        {
            ObjectId oId = new ObjectId(id);
            RegisterModels models = registerCollection.Find(e => e.Id == oId).FirstOrDefault();
            return View(models);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, RegisterModels models)
        {
            models.Id = new ObjectId(id);
            var filter = Builders<RegisterModels>.Filter.Eq("Id", models.Id);
            var updateDef = Builders<RegisterModels>.Update.Set("Fullname", models.Fullname);
            updateDef = updateDef.Set("LastName", models.Fullname);
            var result = registerCollection.UpdateOne(filter, updateDef);

            if (result.IsAcknowledged)
            {
                ViewBag.Message = "updated successfully!";
            }
            else
            {
                ViewBag.Message = "Error while updating!";
            }
            return View(models);
        }
    }
}

