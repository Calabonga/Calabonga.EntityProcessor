using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.EntityProcessor.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddEntityProcessor<TProcessor, TConfiguration>(this IServiceCollection services, Action<TConfiguration> configuration)
        where TProcessor : IEntityProcessor
        where TConfiguration : IEntityProcessorConfiguration
    {
        services.AddScoped(typeof(TProcessor));
        var entityProcessorConfiguration = Activator.CreateInstance<TConfiguration>();
        configuration(entityProcessorConfiguration);
        services.AddSingleton(typeof(TConfiguration), entityProcessorConfiguration);
    }
}