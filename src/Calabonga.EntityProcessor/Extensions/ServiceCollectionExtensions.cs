using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.EntityProcessor.Extensions;

/// <summary>
/// Расширение для контейнера <see cref="IServiceCollection"/>, которое регистрирует процессов и его конфигурацию в контейнере.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Расширение для контейнера <see cref="IServiceCollection"/>, которое регистрирует процессов и его конфигурацию в контейнере.
    /// </summary>
    /// <param name="services">сервисы</param>
    /// <param name="configuration">конфигурация</param>
    /// <typeparam name="TProcessor">процессор</typeparam>
    /// <typeparam name="TConfiguration">конфигурация <see cref="EntityProcessor{TEntity}"/></typeparam>
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