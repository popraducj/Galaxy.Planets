using Galaxy.Planets.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Planets.Presentation.Ioc
{
    public static class RegisterDatabase
    {
        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PlanetsDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("PlanetsDb"), x => x.MigrationsAssembly("Galaxy.Planets.Presentation")));
            var build = services.BuildServiceProvider();
            
            var scope = build.GetService<IServiceScopeFactory>().CreateScope();
            scope.ServiceProvider.GetRequiredService<PlanetsDbContext>().Database.Migrate();
            return services;
        }
    }
}