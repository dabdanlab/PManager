using System.Collections.Generic;
using System.Linq;
using PManager.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace PManager.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IMongoCollection<RegisterModels> registerCollection;
        private readonly IMongoCollection<LoginModels> loginCollection;
        private readonly IDictionary<string, LoginModels> _users;

        public AccountServices(IConfiguration config)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("PManager");
            this.registerCollection = db.GetCollection<RegisterModels>("AccDb");
        }
        public AccountServices(IDictionary<string, LoginModels> users) => _users = users;

        public Task<(bool, LoginModels)> Validate(string Email, string Password)
        {
            var isValid = _users.ContainsKey(Email) && string.Equals(_users[Email].Password, Password, StringComparison.Ordinal);
            var result = (isValid, isValid ? _users[Email] : null);
            return Task.FromResult(result);
        }
        public LoginModels LoginIn(LoginModels models)
        {
            var W = loginCollection.AsQueryable<LoginModels>().Where(w => w.Email == models.Email && w.Password == models.Password).FirstOrDefault();
            return W;
        }

        public RegisterModels Register(RegisterModels models)
        {
            registerCollection.InsertOne(models);
            return models;
        }
    }
}
