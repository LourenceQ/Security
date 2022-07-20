using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Security.Authorization;

namespace Security
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
            services.AddHttpClient("API", client => 
            {
                client.BaseAddress = new Uri("https://localhost:44336/");
            });
            
            services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", opt =>
            {
                opt.Cookie.Name = "MyCookieAuth";
                opt.LoginPath = "/Account/Login";
                opt.AccessDeniedPath = "/Account/AccessDenied";
                opt.ExpireTimeSpan = TimeSpan.FromSeconds(30);
            });
            services.AddRazorPages();

            services.AddAuthorization(opt => 
            {
                opt.AddPolicy("AdminOnly", 
                    policy => policy.RequireClaim("Admin"));
                    
                opt.AddPolicy("MustBelongToHRDepartment", 
                    policy => policy.RequireClaim("Department", "HR"));

                opt.AddPolicy("HRManagerOnly", policy => policy
                    .RequireClaim("Department", "HR")
                    .RequireClaim("Manager")
                    .Requirements.Add(new HrManagerProbationRequirement(3)
                    ));
            });

            services.AddSingleton<IAuthorizationHandler
                , HrManagerProbationRequirementHandler>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
