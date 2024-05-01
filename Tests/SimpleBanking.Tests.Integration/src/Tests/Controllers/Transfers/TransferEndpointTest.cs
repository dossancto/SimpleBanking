using System.Net;
using Microsoft.Extensions.DependencyInjection;
using SimpleBanking.API.Middlewares.ErrorMiddlewares;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Tests.Integration.Helpers.Creation;
using SimpleBanking.Tests.Integration.Share.Endpoints;

namespace SimpleBanking.Tests.Integration.Tests.Controllers.Transfers;

public class TransferEndpointTest(IntegrationTestWebApplicationFactory web) : IntegrationTest(web)
{
    [Fact]
    public async Task POSTTransferEndpoint_UserToUser_ShouldReturnSuccess()
    {
        var senderInitialBalance = 200;
        var receiverInitialBalance = 300;
        //Given
        var client = web.CreateClient();
        var resources = web.Resources();

        var scope = resources.Web.Services.CreateScope();

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

    [Fact]
    public async Task POSTTransferEndpoint_UserToUserInsuficientAmmount_ShouldReturnInvalidAmmountErrro()
    {
        var senderInitialBalance = 200;
        var receiverInitialBalance = 300;
        //Given
        var client = web.CreateClient();
        var resources = web.Resources();

        var scope = resources.Web.Services.CreateScope();

        var p1 = await resources.CreatePersonWithBalance(senderInitialBalance);
        var p2 = await resources.CreatePersonWithBalance(receiverInitialBalance);

        var createInput = new TransferInput()
        {
            Sender = p1.CPF,
            Receiver = p2.CPF,
            Ammount = 1_000_000
        };

        var transferPayload = createInput.AsPayload();

        //When
        var response = await client.PostAsync(TransferEndpoints.TRANSFER, transferPayload);
        var content = await response.Content.ReadAsStringAsync();

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var result = content.FromPayload<BaseError>();

        result.Should().NotBeNull();
        result!.Kind.Should().Be(TransferErrorType.INSUFICIENT_AMMOUNT.ToString());
    }
}
