using Microsoft.Extensions.DependencyInjection;
using SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;
using SimpleBanking.Domain.Features.Merchants.Entities;

namespace SimpleBanking.Tests.Integration.Helpers.Creation;

public static class CreateMerchantHelper
{
    private static async Task<Merchant> CreateMerchant(this HelperResources resource, IServiceScope scope)
    {
        var registerPlayer = scope.ServiceProvider.GetRequiredService<CreateMerchantUseCase>();

        var randomId = IdGenerator.Safe();

        var email = $"test_{randomId}@test.com";
        var username = $"test{randomId}";

        var payload = new CreateMerchantInput()
        {
            Email = email,
            FullName = username,
            CNPJ = CPFGenerator.Generate(),
            Password = "S#curePa1ssword123",
        };

        return await registerPlayer.Execute(payload);
    }

    public static Task<Merchant> CreateMerchant(this HelperResources resource)
    {
        var scope = resource.Web.Services.CreateScope();

        return resource.CreateMerchant(scope);
    }

}

