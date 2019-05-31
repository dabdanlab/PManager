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
        private readonly IDictionary<string, RegisterModels> _users;

        public AccountServices(IConfiguration config)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("PManager");
            registerCollection = db.GetCollection<RegisterModels>("AccDb");
        }
        public AccountServices(IDictionary<string, RegisterModels> users) => _users = users;

        public Task<(bool, RegisterModels)> Validate(string Email, string Password)
        {
            var isValid = _users.ContainsKey(Email) && string.Equals(_users[Email].Password, Password, StringComparison.Ordinal);
            var result = (isValid, isValid ? _users[Email] : null);
            return Task.FromResult(result);
        }
    }
}
