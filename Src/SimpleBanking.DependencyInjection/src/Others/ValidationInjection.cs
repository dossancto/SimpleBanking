using FluentValidation;

namespace SimpleBanking.DependencyInjection.Others;

internal static class ValidationInjection
{
    public static IServiceCollection AddFluentValidationConfiguration(this IServiceCollection services)
         => services
         .AddValidatorsFromAssemblyContaining<Application.Application>()
         ;
}
