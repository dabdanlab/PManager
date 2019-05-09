using System.Collections.Generic;
using System.Linq;
using PManager.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace PManager.Services
{
    public class UserServices
    {
        private readonly IMongoCollection<UserModels> userCollection;

        public UserServices(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("PManager"));
            var database = client.GetDatabase("PManager");
            userCollection = database.GetCollection<UserModels>("UserDb");
        }

        public UserModels Register(UserModels user)
        {
            userCollection.InsertOne(user);
            return user;
        }

    }
}
