using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVCProjectCodeFirstNTier.DAL.Models;
using MVCProjectCodeFirstNTier.DAL.Contexts;

namespace MVCProjectCodeFirstNTier.BLL.Services
{
    public class UserService
    {
        private readonly UserContext _context = new UserContext();

        public IEnumerable<User> getAllUsers()
        {
            return _context.Users.ToList();
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}
