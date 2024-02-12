using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Toys.Models;

namespace Toys.DAL
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions):base(dbContextOptions)
        {

        }
       public DbSet<HomeHeader>? HomeHeaders { get; set; }
        public DbSet<CreativeApproach> CreativeApproaches { get; set; }
        public DbSet<Toys.Models.ToysModel> Toys { get; set; }
        public DbSet<ToysCategory> ToysCategories { get; set; }
        public DbSet<Contact> ContactUs { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToysCategory>().HasMany(x => x.Toys).WithOne(x => x.Category);
            //builder.en<RoleType>().Hasmany(
            base.OnModelCreating(builder);
        }
    }
}
