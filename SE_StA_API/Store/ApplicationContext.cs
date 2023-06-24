using SE_StA_API.DataObject;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SE_StA_API.Store {
    public class ApplicationContext : IdentityDbContext {
        public ApplicationContext (DbContextOptions<ApplicationContext> options) : base(options) {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            //additional settings for certain entity options
            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// database table Dummies
        /// </summary>
        public DbSet<Country> Countries { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}