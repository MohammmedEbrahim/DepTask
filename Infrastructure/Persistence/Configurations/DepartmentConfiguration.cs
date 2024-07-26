using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(x => x.SubDepartments)
                .WithOne(x => x.ParentDepartment)
                .HasForeignKey(x => x.ParentDepartmentId);
        }
    }
}
