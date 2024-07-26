using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DepartmentConfiguration().Configure(modelBuilder.Entity<Department>());
        }

        public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
