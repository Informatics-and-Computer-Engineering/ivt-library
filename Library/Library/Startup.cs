using Microsoft.AspNetCore.Builder;
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

        private void GenerateDefaultConnectionString()
        {
            if (!File.Exists("./connectionstrings.json"))
            {
                var defaultConnectionString = new ConnectionStrings();
                defaultConnectionString.LibraryConnection = "Host=localhost;Database=library;Username=postgres;Password=your_password";
                File.WriteAllText("./connectionstrings.json", JsonConvert.SerializeObject(defaultConnectionString));
            }
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            GenerateDefaultConnectionString();

            var json = System.IO.File.ReadAllText("./connectionstrings.json");
            var connectionStrings = JsonConvert.DeserializeObject<ConnectionStrings>(json);

            services.AddDbContext<LibraryContext>(options =>
                options.UseNpgsql(connectionStrings.LibraryConnection));

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<LibraryContext>(options => options.UseNpgsql(connectionStrings.LibraryConnection));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<LibraryContext>()
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
