using Core.DB.Configurations;
using Core.Entities;
using System.Data.Entity;

namespace Core.DB
{
    public class DbCoreDataContext : DbContext
    {
        public DbCoreDataContext()
            : base("name=DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new DictionaryConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new PermissionConfiguration());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Dictionary> Dictionaries { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

    }
}
