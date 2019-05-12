using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace PManager.Models
{
    public class UserModels
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("UserName")]
        public string UserName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [BsonElement("Age")]
        public string Age { get; set; }
        [BsonElement("Adress")]
        public string Adress { get; set; }
    }
}
