using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CRM.Data;
using CRM.EmailConfirm;
using CRM.Filters;
using CRM.Models.InnerModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<CRMdb>(options =>
            {
                options.UseSqlServer(Configuration["Data:ConnectionString:Default"]);
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");
                      

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSender>(Configuration);
            //----

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>
                 {
                     new CultureInfo("az"),
                     new CultureInfo("en"),
                     new CultureInfo("ru"),                 

                 };

                options.DefaultRequestCulture = new RequestCulture("az");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Home/Account/AccessDenied";
                options.SlidingExpiration = true;
            }


            );
            

            services.AddMvc(options => options.Filters.Add(new GetLanguageActionFilter())
                
                ).AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
          
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddIdentity<User, IdentityRole>().
                AddEntityFrameworkStores<CRMdb>().AddDefaultTokenProviders().
                AddDefaultUI();







            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0);
            });

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CRMdb context,
                                                                               UserManager<User> userManager,
                                                                               RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization();
            app.UseCookiePolicy();
            app.UseAuthentication();

            var supportedCultures = new[]
            {
                new CultureInfo("az"),
                new CultureInfo("en"),
                new CultureInfo("ru"),
         
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("az"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures

            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "MyArea",
                    template: "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            DbInitializer.Seed(context, userManager, roleManager, Configuration).Wait();
        }
    }
}
