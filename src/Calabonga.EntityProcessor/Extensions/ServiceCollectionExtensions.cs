using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.EntityProcessor.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddEntityProcessor<TProcessor>(this IServiceCollection services, Action<EntityProcessorConfiguration> configure)
    {
        services.AddScoped(typeof(TProcessor));
        var configuration = new EntityProcessorConfiguration();
        configure(configuration);
        services.AddSingleton(configuration);
    }

    public static void AddEntityProcessor<TProcessor>(this IServiceCollection services, EntityProcessorConfiguration? configuration)
    {
        services.AddScoped(typeof(TProcessor));
        services.AddSingleton(configuration ??= new EntityProcessorConfiguration());
    }
}