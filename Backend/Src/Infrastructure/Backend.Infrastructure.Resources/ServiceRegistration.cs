using Backend.Application.Interfaces;
using Backend.Infrastructure.Resources.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure.Resources;

public static class ServiceRegistration
{
    public static IServiceCollection AddResourcesInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITranslator, Translator>();

        return services;
    }
}