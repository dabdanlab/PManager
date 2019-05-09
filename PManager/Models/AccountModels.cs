using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PManager.Models
{
    public class LoginInfromationModels
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Username")]
        [Required(ErrorMessage = "Please enter your Username")]
        public string Username { get; set; }

        [BsonElement("Password")]
        [Required(ErrorMessage = "Please enter your Password")]
        public string Password { get; set; }
    }
}
