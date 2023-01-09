using System.Reflection;
using DDDPratica.Core.RegisterServices;

namespace DDDPratica.WebApp.MVC.Extensions.Configuration.Services;

public static class ServicesContainerInstaller
{
    public static IServiceCollection InstallServices
    (
        this IServiceCollection services,
        IConfiguration configuration,
        params Assembly[] assemblies
        )
    {
        IEnumerable<IServicesInstaller> servicesInstallers = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(IsAssignableToType<IServicesInstaller>)
            .Select(Activator.CreateInstance)
            .Cast<IServicesInstaller>();

        foreach (var serviceInstaller in servicesInstallers)
        {
            serviceInstaller.Install(services, configuration);
        }

        return services; 
        
        static bool IsAssignableToType<T>(TypeInfo typeInfo) =>
            !(typeof(T).IsAssignableFrom(typeInfo) || typeInfo.IsAbstract);
    }
}