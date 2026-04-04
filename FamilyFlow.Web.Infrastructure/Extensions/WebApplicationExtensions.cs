using FamilyFlow.Data.Seeding.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFlow.Web.Infrastructure.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder UseRolesSeeder(this IApplicationBuilder aplicationBuilder)
        {
            using IServiceScope serviceScope = aplicationBuilder
                .ApplicationServices
                .CreateScope();

            IIdentitySeeder identitySeeder = serviceScope
                .ServiceProvider
                .GetRequiredService<IIdentitySeeder>();

            identitySeeder
                .SeedRolesAsync()
                .GetAwaiter()
                .GetResult();

            return aplicationBuilder;
        }
    }
}
