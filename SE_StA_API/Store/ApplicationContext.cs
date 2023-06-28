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

        public DbSet<CountryTimeZone> CountryTimeZones { get; set; }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<RentalCar> RentalCars { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomType> RoomTypes { get; set;}
        public DbSet<Service> Services { get; set;}
        public DbSet<ServiceType> ServiceTypes { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<DataObject.TimeZone> TimeZones { get; set; }
        public DbSet<Wellness> Wellnesses { get; set; }
    }
}