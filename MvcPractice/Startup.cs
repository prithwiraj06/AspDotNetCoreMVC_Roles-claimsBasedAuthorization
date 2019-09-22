using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcPractice.Models;
namespace MvcPractice
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 2;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddAuthorization(options =>
            {
            options.AddPolicy("DeleteRolePolicy", policy =>
                                                  policy.RequireClaim("Delete Role", "true").
                                                  RequireClaim("Create Role", "true"));
                options.AddPolicy("EditRolePolicy", policy =>
                                                    policy.RequireAssertion(context => AuthorizeAccess(context)));
            });
            
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Accounts/Login";  // to redirect user to login page if not authorize
                options.SlidingExpiration = true;
                options.AccessDeniedPath = "/Accounts/AccessDenied";
            });

            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error"); // 500 internal server error handling
                app.UseStatusCodePagesWithReExecute("/Error/{0}"); // 404 error handling for page not found & employee not found
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
        private bool AuthorizeAccess(AuthorizationHandlerContext context)
        {
            return context.User.IsInRole("Admin") && context.User.HasClaim("Edit Role", "true") || context.User.IsInRole("SuperAdmin");
        }
    }
}
