using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using NTier.DAL.Contexts;
using NTier.DAL.Models;
using NTier.BLL.Models;

namespace NTier.BLL.Services
{
    public class UserService
    {
        private readonly UserContext _context = new UserContext();

        public List<BLLUsers> GetAllUsers()
        {
            List<BLLUsers> bllUsers = new List<BLLUsers>();
            
            var dalUsers = _context.Users.ToList();
            foreach (var user in dalUsers)
            {
                BLLUsers currUser = new BLLUsers();
                currUser.userID = user.userID;
                currUser.username = user.username;
                currUser.email = user.email;
                bllUsers.Add(currUser);
            }
            return bllUsers;
        }

        public void Create(BLLUsers bllUser)
        {
            var user = new Users
            {
                username = bllUser.username,
                email = bllUser.email,
                password = PasswordService.HashPassword(bllUser.password),
                confirmedPassword = PasswordService.HashPassword(bllUser.confirmedPassword)
            };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public BLLUsers FindUser(int? id)
        {
            Users dalUser = _context.Users.Find(id);
            if (dalUser == null)
            {
                return null;
            }
            var user = new BLLUsers
            {
                userID = dalUser.userID,
                username = dalUser.username,
                email = dalUser.email
            };
            return user;
        }

        public void EditUser(BLLUsers bllUser)
        {
            var dalUser = _context.Users.Find(bllUser.userID);
            if (dalUser != null)
            {
                dalUser.username = bllUser.username;
                dalUser.email = bllUser.email;
                _context.Entry(dalUser).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void DeleteUser(int id)
        {
            Users dalUser = _context.Users.Find(id);
            _context.Users.Remove(dalUser);
            _context.SaveChanges();
        }
    }
}