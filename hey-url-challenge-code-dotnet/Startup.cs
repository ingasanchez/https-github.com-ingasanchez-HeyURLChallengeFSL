using hey_url_challenge_code_dotnet.Generics;
using hey_url_challenge_code_dotnet.Interfaces;
using hey_url_challenge_code_dotnet.Services;
using hey_url_challenge_code_dotnet.Transaction;
using HeyUrlChallengeCodeDotnet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HeyUrlChallengeCodeDotnet
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
            services.AddBrowserDetection();
            services.AddControllers();
            services.AddControllersWithViews();
            services.AddScoped<IUrlGenerics, UrlGenerics>();
            services.AddScoped<ILogUrlGenerics, LogUrlGenerics>();
            services.AddScoped<ITransactions, Transactions>();
            services.AddScoped<IFixUrlService, FixUrlService>();
           // services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase(databaseName: "HeyUrl"));
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("myConectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationContext>();
            //context.Database.EnsureCreated();
        }
    }
}