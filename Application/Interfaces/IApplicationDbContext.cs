using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        Task<int> SaveChangesAsync();
    }
}
