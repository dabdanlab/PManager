using PManager.APP_Start;
using PManager.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PManager.Services
{
    public interface IAccountServies
    {
        User Login(string UserName, string Password);
    }

    public class AccountServices : IAccountServies
    {
        private DataContext _context;

        public User Login(string UserName, string Password)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                return null;
            var user = _context.Users.SingleOrDefault(x => x.UserName == UserName);

            if (user == null)
                return null;

            if (Password == null)
                return null;

            return user;
        }
    }
}
