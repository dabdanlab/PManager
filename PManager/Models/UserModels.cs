using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PManager.Models
{
    public class UserModels
    {
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
