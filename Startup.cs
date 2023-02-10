using E_RENTAL_SYSTEM.Model;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_RENTAL_SYSTEM
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
            //adding Entity Framework
            services.AddDbContext<ERSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConStr")));

            //adding swagger middleware
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Ver1", new OpenApiInfo { Title = "E_RENTAL_SYSTEM", Version = "Ver1" });
            });
            var conString = Configuration.GetConnectionString("ConnectionStrings"); //Add Namespace using Microsoft.EntityFrameworkCore for UseSqlServer
            services.AddDbContext<ERSContext>(options => options.UseSqlServer(conString));             //services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ////Resister  swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/Ver1/swagger.json", "ERS_Web");
            });
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
