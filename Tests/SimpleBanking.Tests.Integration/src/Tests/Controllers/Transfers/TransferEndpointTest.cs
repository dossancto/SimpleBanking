using System.Net;
using Microsoft.Extensions.DependencyInjection;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Tests.Integration.Helpers.Creation;
using SimpleBanking.Tests.Integration.Share.Endpoints;

namespace SimpleBanking.Tests.Integration.Tests.Controllers.Transfers;

public class TransferEndpointTest(IntegrationTestWebApplicationFactory web) : IntegrationTest(web)
{
    [Fact]
    public async Task TestNameAsync()
    {
        var senderInitialBalance = 200;
        var receiverInitialBalance = 300;
        //Given
        var client = web.CreateClient();
        var resources = web.Resources();

        using var scope = resources.Web.Services.CreateScope();

        var personRepo = scope.ServiceProvider.GetRequiredService<IPersonRepository>();

        var p1 = await resources.CreatePersonWithBalance(senderInitialBalance);
        var p2 = await resources.CreatePersonWithBalance(receiverInitialBalance);

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
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var p1Info = await personRepo.SelectById(new()
        {
            Id = p1.Id
        });

        p1Info!.Balance.Debit.Should().Be(100);

        var p2Info = await personRepo.SelectById(new()
        {
            Id = p2.Id
        });

        p2Info!.Balance.Debit.Should().Be(400);
    }
}
