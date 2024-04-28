using System.Net;
using SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;
using SimpleBanking.Domain.Features.Merchants.Entities;
using SimpleBanking.Tests.Integration.Share.Endpoints;

namespace SimpleBanking.Tests.Integration.Tests.Controllers.Merchants;

public class CreateMerchantEndpointTest(IntegrationTestWebApplicationFactory web) : IntegrationTest(web)
{
    [Fact]
    public async Task CreateMerchnt_ValidInfos_ShouldReturnCreatedMerchant()
    {
        //Given
        var client = web.CreateClient();

        var createInput = new CreateMerchantInput()
        {
            CNPJ = CPFGenerator.Generate(),
            Email = $"test{IdGenerator.Safe()}@test.com",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        var createPersonPayload = createInput.AsPayload();

        //When
        var response = await client.PostAsync(MerchantEndpoints.CREATE, createPersonPayload);
        var content = await response.Content.ReadAsStringAsync();

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = content.FromPayload<Merchant>();

        result.Should().NotBeNull();
        result!.Id.Should().NotBeNullOrEmpty();
        result!.CNPJ.Should().Be(createInput.CNPJ);
        result!.EmailAddress.Should().Be(createInput.Email.ToLower());
    }
}

