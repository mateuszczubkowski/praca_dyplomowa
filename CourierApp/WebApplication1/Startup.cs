using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CourierApp.Core.Implementation;
using CourierApp.Core.Implementation.Interfaces;
using CourierApp.Data;
using CourierApp.Data.Models;
using CourierApp.MailService;
using CourierApp.WebApp.Worker;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Spi;

namespace CourierApp.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static QuartzStartup _quartzStartup;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connectionString = Configuration.GetConnectionString("CourierAppContext");
            var identityString = Configuration.GetConnectionString("IdentityAppContext");
            
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<CourierAppDbContext>(options =>
                    options.UseNpgsql(connectionString, dbBuilder => dbBuilder.MigrationsAssembly("CourierApp.WebApp")));

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<CourierAppIdentityDbContext> (options =>
                    options.UseNpgsql(identityString, dbBuilder => dbBuilder.MigrationsAssembly("CourierApp.WebApp")));

            services.AddScoped<ICourierManagementService, CourierManagementService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();
            services.AddScoped<IGeolocationService, GeolocationService>();
            services.AddSingleton<MailQueue>();

            services.Configure<EmailConfig>(Configuration.GetSection("Email"));

            RegisterQuartz(services);

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CourierAppIdentityDbContext>()
                .AddDefaultTokenProviders();

            //Info about Passwords Strength
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            });

            //The Account Login page's settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookies settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Login"; // You can type here you own LoginPath, if you don't set custom path, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // You can type here you own LogoutPath, if you don't set custom path, ASP.NET Core will default to /Account/Logout
                options.SlidingExpiration = true;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider iServiceProvider, IApplicationLifetime lifetime)
        {
            _quartzStartup = app.ApplicationServices.GetService<QuartzStartup>();
            lifetime.ApplicationStarted.Register(_quartzStartup.Start);
            lifetime.ApplicationStopping.Register(_quartzStartup.Stop);
            app.ApplicationServices.GetService<IScheduler>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            Seed.CreateRoles(iServiceProvider, Configuration).Wait();
        }

        private void RegisterQuartz(IServiceCollection services)
        {
            //Quartz services
            services.AddSingleton<IJobFactory, QuartzJobFactory>();
            services.AddSingleton<QuartzStartup>();
            services.AddScoped<MailJob>();
        }
    }
}