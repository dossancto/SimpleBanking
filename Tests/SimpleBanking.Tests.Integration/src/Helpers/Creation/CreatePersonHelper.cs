using Microsoft.Extensions.DependencyInjection;
using SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Tests.Integration.Helpers.Creation;

public class CreatePersonResource
{
    public required IntegrationTestWebApplicationFactory Web;
}

public static class CreatePersonHelper
{
    public static CreatePersonResource Resources(this IntegrationTestWebApplicationFactory web)
    => new()
    {
        Web = web
    };

    public static async Task<SafePerson> CreatePerson(this CreatePersonResource resource)
    {
        using var scope = resource.Web.Services.CreateScope();

        var registerPlayer = scope.ServiceProvider.GetRequiredService<CreatePersonUseCase>();

        var randomId = IdGenerator.Safe();

        var email = $"test_{randomId}@test.com";
        var username = $"test{randomId}";

        var payload = new CreatePersonInput()
        {
            Email = email,
            FullName = username,
            CPF = CPFGenerator.Generate(),
            Password = "S#curePa1ssword123",
        };

        return await registerPlayer.Execute(payload);
    }
}
