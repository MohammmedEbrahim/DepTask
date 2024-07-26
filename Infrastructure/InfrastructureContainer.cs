using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureContainer
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<ApplicationDbContext>(option =>
				option.UseSqlServer(configuration.GetConnectionString("DbConnect")));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
		}
	}
}

