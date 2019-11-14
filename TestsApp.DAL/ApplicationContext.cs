using Microsoft.EntityFrameworkCore;
using TestApp.DAL.Models;

namespace TestApp.DAL
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

		public DbSet<User> Users { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            new UserMap(modelBuilder.Entity<User>());
           
			base.OnModelCreating(modelBuilder);
        }
    }
}
