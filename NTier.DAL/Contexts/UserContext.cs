using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using NTier.DAL.Models;

namespace NTier.DAL.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext() : base("UserContext")
        {
        }

        public DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}