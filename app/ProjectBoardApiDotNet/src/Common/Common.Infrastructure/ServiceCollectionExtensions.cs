using Common.Application.Abstractions;
using Common.Infrastructure.MessageBus;
using Common.Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<DomainEventDispatcherInterceptor>();

        services.AddSingleton<DomainEventDispatcherInterceptor>();

        services.AddScoped<IMessageBus, InMemoryMessageBus>();

        return services;
    }
}
