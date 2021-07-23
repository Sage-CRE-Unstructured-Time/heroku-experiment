using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Sage.MeteoriteLandingService.ModelMapping;
using Sage.MeteoriteLandingService.Repositories;
using Sage.MeteoriteLandingService.Services;

namespace MeteoriteLandingService
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

            services.AddCors(options => {
                options.AddDefaultPolicy(builder => {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MeteoriteLandingService", Version = "v1" });
            });

            services.AddDbContext<Sage.MeteoriteLandingService.Models.datasetsContext>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("DatasetsDb"), npgsqlOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorCodesToAdd: null);
                });
            });

            services.AddScoped<IMeteoriteLandingsRepository, MeteoriteLandingsRepository>(
                serviceProvider =>
                    new MeteoriteLandingsRepository(
                        serviceProvider.GetRequiredService<Sage.MeteoriteLandingService.Models.datasetsContext>(), 
                        serviceProvider.GetRequiredService<ILogger<MeteoriteLandingsRepository>>()
                    )
            );

            services.AddTransient<IMeteoriteLandingService, Sage.MeteoriteLandingService.Services.MeteoriteLandingService>();

            services.AddAutoMapper(typeof(MappingProfile));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseExceptionHandler("/error");

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MeteoriteLandingService v1"));

            app.UseHttpsRedirection();

            app.UseCors();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
