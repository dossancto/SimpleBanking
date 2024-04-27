using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Infra.Database.EF.Repositories;

namespace SimpleBanking.DependencyInjection.Repositories;

internal static class PersonRepositoryInjection
{
    public static IServiceCollection AddPersonRepositories(this IServiceCollection services)
    => services
            .AddScoped<IPersonRepository, EFPersonRepository>()
    ;
}

