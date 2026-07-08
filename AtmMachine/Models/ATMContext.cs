using AtmMachine.Classes;
using AtmMachine.Controllers;
using Microsoft.EntityFrameworkCore;
namespace AtmMachine.Models
{
    public class ATMContext : DbContext
    {

        public ATMContext()
        {
        }

        public ATMContext(DbContextOptions<ATMContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=.;Database=ATMWebAPi;Trusted_Connection=True;TrustServerCertificate=True;").LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging().EnableDetailedErrors();
            }
        }
        // Change the access modifier of the Accounts property from private to public
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Person> AccountHolders { get; set; }
        public DbSet<ATM> ATMs { get; set; }
        public DbSet<UserName> UserNames { get; set; }
        public DbSet<UserResult> UserResults { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserName>()
                .HasNoKey()
                .ToView("PersonView");


        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseModel>().Where(q => q.State == EntityState.Modified || q.State == EntityState.Added);
            foreach(var entry in entries)
            {
                entry.Entity.CreatedAt=DateTime.UtcNow;
                entry.Entity.ModifiedBy = "Ghulam Habib";
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        

    }
}
