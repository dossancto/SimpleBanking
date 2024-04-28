using System.Net;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;
using SimpleBanking.Tests.Integration.Helpers.Creation;
using SimpleBanking.Tests.Integration.Share.Endpoints;

namespace SimpleBanking.Tests.Integration.Tests.Controllers.Transfers;

public class TransferEndpointTest(IntegrationTestWebApplicationFactory web) : IntegrationTest(web)
{
    [Fact]
    public async Task TestNameAsync()
    {
        //Given
        var client = web.CreateClient();

        var resources = web.Resources();

        var p1 = await resources.CreatePerson();
        var p2 = await resources.CreatePerson();

        var createInput = new TransferInput()
        {
            Sender = p1.CPF,
            Receiver = p2.CPF,
            Ammount = 100
        };

        var transferPayload = createInput.AsPayload();

        //When
        var response = await client.PostAsync(TransferEndpoints.TRANSFER, transferPayload);
        var content = await response.Content.ReadAsStringAsync();

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}
