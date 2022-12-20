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
    /// <param name="configure">конфигурация</param>
    /// <typeparam name="TProcessor">процессор</typeparam>
    public static void AddEntityProcessor<TProcessor>(this IServiceCollection services, Action<EntityProcessorConfiguration> configure)
    {
        services.AddScoped(typeof(TProcessor));
        var configuration = new EntityProcessorConfiguration();
        configure(configuration);
        services.AddSingleton(configuration);
    }

    /// <summary>
    /// Расширение для контейнера <see cref="IServiceCollection"/>, которое регистрирует процессов и его конфигурацию в контейнере.
    /// </summary>
    /// <param name="services">сервисы</param>
    /// <param name="configuration">конфигурация</param>
    /// <typeparam name="TProcessor">процессор</typeparam>
    public static void AddEntityProcessor<TProcessor>(this IServiceCollection services, EntityProcessorConfiguration? configuration)
    {
        services.AddScoped(typeof(TProcessor));
        services.AddSingleton(configuration ??= new EntityProcessorConfiguration());
    }
}