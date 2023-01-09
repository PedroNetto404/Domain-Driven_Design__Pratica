using System.ComponentModel.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDPratica.Core.RegisterServices;

public interface IServicesInstaller
{
    void Install(IServiceCollection services, IConfiguration configuration); 
}