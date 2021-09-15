using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Mappings;
using Application.Contracts;
using Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Servicio de automapper
            services.AddAutoMapper(typeof(Maps));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Api prueba Angel",
                    Version = "v1",
                    Description = "Api de prueba en la que demuestro las capas y forma de trabajo"
                });
            });
            
            //Agrego servicios del proyecto Application->Contracts
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            
            services.AddControllers();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           // app.UseHttpsRedirection();

           app.UseSwagger();
           app.UseSwaggerUI(c => {
               c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de ejemplo");
               c.RoutePrefix = "";
           });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
