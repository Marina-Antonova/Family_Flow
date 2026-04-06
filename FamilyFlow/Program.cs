using FamilyFlow.Data;
using FamilyFlow.Data.Models;
using FamilyFlow.Data.Seeding;
using FamilyFlow.Data.Seeding.Interfaces;
using FamilyFlow.Services;
using FamilyFlow.Services.Core;
using FamilyFlow.Services.Core.Interfaces;
using FamilyFlow.Services.Interfaces;
using FamilyFlow.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


namespace FamilyFlow
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string?  connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<FamilyFlowDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();
            builder.Services.AddTransient<IEmailSender, NoOpEmailSender>();

            builder.Services
                .AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
            {
                ConfigureIdentity(builder.Configuration, options);

            })
                .AddEntityFrameworkStores<FamilyFlowDbContext>()
                .AddRoles<IdentityRole<Guid>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddScoped<IFamilyMemberService, FamilyMemberService>();
            builder.Services.AddScoped<IHouseTaskService, HouseTaskService>();
            builder.Services.AddScoped<IScheduleEventService, ScheduleEventService>();
            builder.Services.AddScoped<IScheduleService, ScheduleService>();
            builder.Services.AddScoped<IFamilyService, FamilyService>();    

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseRolesSeeder();

            app.MapControllerRoute(
                name: "adminArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }

        private static void ConfigureIdentity(ConfigurationManager configuration, IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = configuration
                .GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedAccount");
            options.SignIn.RequireConfirmedEmail = configuration
                .GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedEmail");
            options.SignIn.RequireConfirmedPhoneNumber = configuration
                .GetValue<bool>("IdentityOptions:SignIn:RequireConfirmedPhoneNumber");

            options.User.RequireUniqueEmail = configuration
                .GetValue<bool>("IdentityOptions:User:RequireUniqueEmail");

            options.Lockout.MaxFailedAccessAttempts = configuration
                .GetValue<int>("IdentityOptions:Lockout:MaxFailedAccessAttempts");
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(configuration
                .GetValue<int>("IdentityOptions:Lockout:DefaultLockoutTimeSpanMin"));

            options.Password.RequireDigit = configuration
                .GetValue<bool>("IdentityOptions:Password:RequireDigit");
            options.Password.RequireLowercase = configuration
                .GetValue<bool>("IdentityOptions:Password:RequireLowercase");
            options.Password.RequireUppercase = configuration
                .GetValue<bool>("IdentityOptions:Password:RequireUppercase");
            options.Password.RequireNonAlphanumeric = configuration
                .GetValue<bool>("IdentityOptions:Password:RequireNonAlphanumeric");
             options .Password.RequiredLength = configuration
                .GetValue<int>("IdentityOptions:Password:RequiredLength");
            options.Password.RequiredUniqueChars = configuration
                .GetValue<int>("IdentityOptions:Password:RequiredUniqueChars");
        }
    }
}
