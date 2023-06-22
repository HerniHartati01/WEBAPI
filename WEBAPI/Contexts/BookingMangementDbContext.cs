using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;
using WEBAPI.Utility;

namespace WEBAPI.Contexts
{
    public class BookingMangementDbContext : DbContext
    {
        public BookingMangementDbContext(DbContextOptions<BookingMangementDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<University> Universities { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Role>().HasData(new Role
            {
                Guid = Guid.Parse("e68b96e8-2279-4b4d-262e-08db60bf5fd4"),
                Name = nameof(RoleLevel.User),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("e68b96e8-2279-5a5b-262e-08db60bf5fd4"),
                Name = nameof(RoleLevel.Manager),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            },
            new Role
            {
                Guid = Guid.Parse("e68b96e8-2279-6a6c-262e-08db60bf5fd4"),
                Name = nameof(RoleLevel.Admin),
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            });

            builder.Entity<Employee>().HasIndex(e => new
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();


            // Relation between Education and University (1 to Many)
            builder.Entity<Education>()
                .HasOne(u => u.University)
                .WithMany(e => e.Educations)
                .HasForeignKey(e => e.UniversityGuid);

            // Relation between Education and Employee (1 to 1)
            builder.Entity<Education>()
                .HasOne(e => e.Employee)
                .WithOne(e => e.Education)
                .HasForeignKey<Education>(e => e.Guid);

            // Relation between Account and Employee (1 to 1)
            builder.Entity<Account>()
                .HasOne(e => e.Employee)
                .WithOne(a => a.Account)
                .HasForeignKey<Account>(e => e.Guid);

            // Relation between Account Role and Account (1 to Many)
            builder.Entity<AccountRole>()
                .HasOne(a => a.Account)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(ar => ar.AccountGuid);

            // Relation between Account Role and Role (1 to Many)
            builder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRoles)
                .HasForeignKey(e => e.RoleGuid);

            // Relation between Employee and Booking (1 to Many)
            builder.Entity<Booking>()
                .HasOne(e => e.Employee)
                .WithMany(b => b.Bookings)
                .HasForeignKey(b => b.EmployeeGuid);

            // Relation between Room and Booking (1 to Many)
            builder.Entity<Room>()
                .HasMany(r => r.Bookings)
                .WithOne(b => b.Room)
                .HasForeignKey (b => b.RoomGuid);
        }
    }
}
