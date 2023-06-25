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
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingPositionRoom> BookingPositionRooms { get; set; }
        public DbSet<BookingPositionService> BookingPositionServices { get; set; }
        public DbSet<Cash> Cashes { get; set;}
        public DbSet<ContactAddress> ContactAddresses { get; set;}
        public DbSet<Contact> Contacts { get; set;}
    }
}