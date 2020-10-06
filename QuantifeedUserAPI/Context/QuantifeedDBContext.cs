using Microsoft.EntityFrameworkCore;
using QuantifeedUserAPI.Models;

namespace QuantifeedUserAPI.Context
{
    public class QuantifeedDBContext : DbContext
    {
        public QuantifeedDBContext()
        {
        }

        public QuantifeedDBContext(DbContextOptions<QuantifeedDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        
        public DbSet<Manager> Managers { get; set; }
        
        public DbSet<Client> Clients { get; set; }
        
        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:quantifeedappdbserver.database.windows.net,1433;Initial Catalog=QuantifeedDB;Persist Security Info=False;User ID=serveradmin;Password=Admin123$;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK_dbo.Users");

                entity.Property(e => e.Alias).HasMaxLength(1000);

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.FirstName).HasMaxLength(1000);

                entity.Property(e => e.LastName).HasMaxLength(1000);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(200);
            });
            
            modelBuilder.Entity<Client>()
                .HasOne(p => p.Manager)
                .WithMany(b => b.Clients);
            
            /*modelBuilder.Entity<Manager>()
                .HasMany(b => b.Clients)
                .WithOne();*/
            
        }
    }
}
