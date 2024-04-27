using System.Reflection;
using SimpleBanking.Domain.DomainTypes.UseCases;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SimpleBanking.DependencyInjection.UseCases;

public static class UseCasesFromAssembly
{
    public static IServiceCollection AddUseCasesFromAssembly(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped, bool includeInternalTypes = false)
    {
        var usecases = AssemblyScanner.Execute(assembly.GetTypes());

        foreach (var usecase in usecases)
        {
            services.TryAddUseCase(usecase, lifetime);
        }

        return services;
    }

    public static IServiceCollection AddUseCasesFromAssemblyContaining<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Scoped, bool includeInternalTypes = false)
        => services.AddUseCasesFromAssembly(typeof(T).Assembly, lifetime, includeInternalTypes);

    private static IServiceCollection TryAddUseCase(this IServiceCollection services, Type usecase, ServiceLifetime lifetime)
    {
        services.TryAdd(
                new ServiceDescriptor(
                    serviceType: usecase,
                    implementationType: usecase,
                    lifetime: lifetime
                    ));

        return services;
    }
}

/// <summary>
/// Class that can be used to find all the usecases from a type
/// </summary>
public class AssemblyScanner
{
    public static IEnumerable<Type> Execute(IEnumerable<Type> types)
      => from type in types
         let interfaces = type.GetInterfaces()
         where interfaces.Contains(typeof(IUseCase))
         select type;
}
