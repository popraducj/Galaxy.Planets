using System.Linq;
using System.Reflection;
using Galaxy.Planets.Presentation.Ioc;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.Planets.Presentation.Ioc
{
    public  static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            var dataAssembly = Assembly.Load("Galaxy.Planets.Core");

            dataAssembly.GetTypesForPath("Galaxy.Planets.Core.Services")
                .ForEach(p =>
                {
                    var interfaceValue = p.GetInterfaces().FirstOrDefault();

                    if (interfaceValue != null)
                    {
                        services.AddScoped(interfaceValue.UnderlyingSystemType, p.UnderlyingSystemType);
                    }
                });
            
            Assembly.Load("Galaxy.Planets.Presentation")
                .GetTypesForPath("Galaxy.Planets.Presentation.Services.Teams")
                .ForEach(p =>
                {
                    var interfaceValue = p.GetInterfaces().FirstOrDefault();

                    if (interfaceValue != null)
                    {
                        services.AddScoped(interfaceValue.UnderlyingSystemType, p.UnderlyingSystemType);
                    }
                });
            
            return services;
        }
    }
}