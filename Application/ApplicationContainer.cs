using Application.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Application
{
    public static class ApplicationContainer
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services )
		{
			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddMediatR(Assembly.GetExecutingAssembly());
			
			services.TryAddSingleton<IHttpContextAccessor , HttpContextAccessor>();
			services.AddHttpContextAccessor();

		   services.AddTransient<BackgroundJobsService>();
           services.AddTransient<EmailService>();

            return services;
		}
	}
}

