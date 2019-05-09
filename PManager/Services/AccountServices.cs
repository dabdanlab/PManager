using System.Collections.Generic;
using System.Linq;
using PManager.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace PManager.Services
{
    public class AccountServices
    {
        private readonly IMongoCollection<LoginInfromationModels> accCollection;

        public AccountServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PManager"));
            var database = client.GetDatabase("PManager");
            accCollection = database.GetCollection<LoginInfromationModels>("UserDb");
        }

        public LoginInfromationModels LoginIn(LoginInfromationModels models)
        {
            var W = accCollection.AsQueryable<LoginInfromationModels>().Where(w => w.Username == models.Username && w.Password == models.Password).FirstOrDefault();
            return W;
        }
    }
}
