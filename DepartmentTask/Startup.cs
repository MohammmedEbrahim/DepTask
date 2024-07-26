using Application;
using Application.Services;
using Hangfire;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace DepartmentTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1" , new OpenApiInfo { Title = "DepartmentTask" , Version = "v1" });
            });

            services.AddPersistenceServices(Configuration);

            services.AddApplicationServices();

            services.AddHangfire(x =>
                             x.UseSqlServerStorage(Configuration.GetConnectionString("DbConnect")));

            services.AddHangfireServer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        [Obsolete]
        public void Configure(IApplicationBuilder app , IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json" , "DepartmentTask v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard("/hangfire_Dashboard");
            app.UseHangfireServer();

            RecurringJob.AddOrUpdate<BackgroundJobsService>(x => x.SendEmailReminders() , "*/1 * * * * *");
        }
    }
}
