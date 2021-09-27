using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Persistence.DapperConnection;

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
            services.AddCors(o => o.AddPolicy("corsApp", builder => {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddDbContext<ApplicationDbContext>(opt => {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.Configure<ConexionConfiguracion>(Configuration.GetSection("ConnectionStrings"));

            //Servicio de automapper
            services.AddAutoMapper(typeof(Maps));

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API net core Maqueta Lider",
                    Version = "v1",
                    Description = "Se desarrolla API en base a requerimientos. Falta implementar logicas de negocios. Se mantiene en espera el tema."
                });
                var xfile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xpath = Path.Combine(AppContext.BaseDirectory, xfile);
                //la ruta del xml está en el archivo de configuracion del proyecto. (csproj)
                //Se debe setear segun el entorno local correspondiente
                c.IncludeXmlComments(xpath);
            });
            
            //Agrego servicios del proyecto Application->Contracts
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddTransient<IFactoryConnection, FactoryConnection>();
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
            
            services.AddControllers();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("corsApp");
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
