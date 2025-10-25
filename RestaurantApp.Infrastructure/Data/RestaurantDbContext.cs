using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RestaurantApp.Domain;
using RestaurantApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantApp.Infrastructure.Data
{
    public class RestaurantDbContext:IdentityDbContext<AppUser>
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    optionsBuilder.UseSqlServer("Server=.;Database=RestaurantAppDb;Trusted_Connection=true;TrustServerCertificate=True;MultipleActiveResultSets=true");
        // base.OnConfiguring(optionsBuilder);
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(c =>
            {
                //c.HasQueryFilter(c => !c.IsDeleted);
                c.HasKey(c => c.Id);
                c.Property(c => c.Name).IsRequired().HasMaxLength(30);
                c.HasMany(c => c.MenuItems)
                .WithOne(c => c.Category)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                
                
            });
           
            modelBuilder.Entity<MenuItem>(m =>
            {
                m.HasKey(c => c.Id);
                m.Property(c => c.Name).IsRequired().HasMaxLength(50);
                m.Property(c => c.Price).IsRequired().HasColumnType("decimal(18,2)");
                m.Property(c => c.Description).HasMaxLength(500);

            });

            modelBuilder.Entity<Order>(o => {
                o.Property(o => o.OrderStatus)
                .HasConversion(os => os.ToString(), os =>(OrderStatus) Enum.Parse(typeof(OrderStatus), os));
                o.OwnsOne(o => o.Address, a => a.WithOwner());
                o.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)");
            });

            //base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var date=DateTime.UtcNow;

            foreach (var entry  in ChangeTracker.Entries<BaseEntity>())
            {
                if(entry.State== EntityState.Added)
                {
                    entry.Entity.CreatedAt = date;
                    entry.Entity.UpdatedAt = null;

                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = date;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
