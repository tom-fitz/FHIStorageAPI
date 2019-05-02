using System;
using System.Collections.Generic;
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
        public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
            //.AddJsonOptions(o => {
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver
            //            as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

            //var connectionString = @"Server=(localdb)\mssqllocaldb;Database=FHIStorageDB;Trusted_Connection=True;";
            var connectionString = @"Server=tcp:fhi01dbprod.database.windows.net,1433;Initial Catalog=FHIStorageDB;Persist Security Info=False;User ID=thomas.fitzgerald;Password=Fitz001/;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            //var connectionString = Startup.Configuration["connectionString:HouseInfoConnectionString"];
            services.AddDbContext<HouseInfoContext>(x => x.UseSqlServer(connectionString));


            services.AddScoped<IHouseInfoRepository, HouseInfoRepository>();
            services.AddScoped<IFurnitureInfoRepository, FurnitureInfoRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, HouseInfoContext houseInfoContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //houseInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            app.UseMvc();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
