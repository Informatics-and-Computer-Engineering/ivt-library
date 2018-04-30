﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Library.Data;
using Library.Models;
using Library.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using File = System.IO.File;

namespace Library
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //Configuration = configuration;
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            void GenerateDefaultConnectionString()
            {
                if (!File.Exists("./connectionstrings.json"))
                {
                    var defaultConnectionString = new ConnectionStrings();
                    defaultConnectionString.LibraryContext = "Host=localhost;Database=library;Username=postgres;Password=your_password";
                    File.WriteAllText("./connectionstrings.json", JsonConvert.SerializeObject(defaultConnectionString));
                    
                }
            }

            GenerateDefaultConnectionString();

            //services.AddDbContext<ApplicationDbContext>(options =>
              //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=aspnet-Library-D666C126-6325-4153-9837-F96FEE19D584;Trusted_Connection=True;MultipleActiveResultSets=true"));

            var json = System.IO.File.ReadAllText("./connectionstrings.json");
            var connectionString = JsonConvert.DeserializeObject<ConnectionStrings>(json).LibraryContext;

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LibraryContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
