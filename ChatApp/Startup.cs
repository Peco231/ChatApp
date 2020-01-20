using ChatApp.Data;
using ChatApp.Hubs;
using ChatApp.Models;
using ChatApp.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                    .WithOrigins("https://localhost:44349/") // TODO: Move to configurations
                    .AllowCredentials();
            }));

            services.AddSignalR();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddTransient<IUserIdProvider, UserIdProvider>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseCors("PolicyCors");
            app.UseAuthentication();
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chatHub");
            });
            app.UseMvc();
        }
    }
}
