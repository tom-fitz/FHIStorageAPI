using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FHIStorage.API.Entities;
using FHIStorage.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace FHIStorage.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration _config { get; private set; }
        public IConfiguration AppSetting { get; private set; }

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                // specify cross origin hosts
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:8080", "https://fhistorage.z19.web.core.windows.net")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddMvc()
                .AddControllersAsServices()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));

            //string dbconn = _config["DBConnectionString"];
            string dbconn = "Server=tcp:fhi01dbprod.database.windows.net,1433;Initial Catalog=FHIStorageDB;Persist Security Info=False;User ID=thomas.fitzgerald;Password=Fitz001/;MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

            services.AddDbContext<HouseInfoContext>(x => x.UseSqlServer(dbconn));

            services.AddScoped<IHouseInfoRepository, HouseInfoRepository>();
            services.AddScoped<IFurnitureInfoRepository, FurnitureInfoRepository>();
            services.AddScoped<IImageInfoRepository, ImageInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            AppSetting = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json")
                .Build();

            //houseInfoContext.EnsureSeedDataForContext();

            // Allowing for cross-origin browsers to access the API endpoints.
            app.UseCors(MyAllowSpecificOrigins);

            app.UseStatusCodePages();

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
