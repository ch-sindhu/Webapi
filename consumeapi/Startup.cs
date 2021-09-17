using consumeapi.Models;
//using LIP.service.CustomerService;
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
namespace consumeapi
{
    public class Startup
    {
        //https://www.yogihosting.com/aspnet-core-web-api-javascript/
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ReservationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddScoped<IRepository, Repository>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "consumeapi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "consumeapi v1"));
            }
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("AllReservation.html");

            app.UseHttpsRedirection();
           
            app.UseDefaultFiles();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
