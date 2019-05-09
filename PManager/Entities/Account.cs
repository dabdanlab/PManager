using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PManager.Entities
{
    [Table("Account")]
    public class Account
    {
        public ObjectId Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

    }
}
