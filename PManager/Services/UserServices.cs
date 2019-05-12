using System.Collections.Generic;
using System.Linq;
using PManager.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson;
using PManager.Entities;
using System.Threading.Tasks;
using System;

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

        public List<UserModels> Get()
        {
            return userCollection.Find(w => true).ToList();
        }

        public UserModels Get(UserModels models, string id)
        {
            return userCollection.Find<UserModels>(w => w.Id == models.Id).FirstOrDefault();
        }

        public UserModels Create(UserModels models)
        {
            userCollection.InsertOne(models);
            return models;
        }

        public void Update(string id, UserModels model)
        {
            userCollection.ReplaceOne(w => w.Id == model.Id, model);
        }

        public void Remove(UserModels model)
        {
            userCollection.DeleteOne(w => w.Id == model.Id);
        }

        public void Remove(UserModels model, string id)
        {
            userCollection.DeleteOne(w => w.Id == model.Id);
        }
    }
}
