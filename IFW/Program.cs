using IFW.Pages.Authorisation;
using Microsoft.AspNetCore.Authorization;

namespace IFW
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddAuthentication("RytisCookie").AddCookie("RytisCookie", options =>
            {
                options.Cookie.Name= "RytisCookie";

                // If login page is not /Account/Login specify the path
                options.LoginPath= "/Account/Login";

                // Access denied page
                options.AccessDeniedPath = "/Account/AccessDenied";

                //Cookie lifetime
                options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
            });

            builder.Services.AddAuthorization(options => 
            {
                options.AddPolicy("AdminPolicy",
                    policy => policy.RequireClaim("Admin"));

                options.AddPolicy("HRpolicy",
                    policy => policy.RequireClaim("Department", "HR"));

                options.AddPolicy("HRmanagerPolicy",
                    policy => policy.RequireClaim("Department", "HR") //And manager
                                    .RequireClaim("Manager")
                                    .Requirements.Add(new ProbationRequirement(3)));
            });

            builder.Services.AddSingleton<IAuthorizationHandler, ProbationRequirementHandler>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapRazorPages();

            app.Run();
        }
    }
}