using System.Net;
using Microsoft.Extensions.DependencyInjection;
using SimpleBanking.API.Middlewares.ErrorMiddlewares;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Tests.Integration.Helpers.Creation;
using SimpleBanking.Tests.Integration.Share.Endpoints;

namespace SimpleBanking.Tests.Integration.Tests.Controllers.Transfers;

public class TransferMerchantToUserEndpointTest(IntegrationTestWebApplicationFactory web) : IntegrationTest(web)
{
    [Fact]
    public async Task POSTTransferEndpoint_MerchantToUser_ShouldThrowTransferExceptionUnsupportedSender()
    {
        var receiverInitialBalance = 300;
        //Given
        var client = web.CreateClient();
        var resources = web.Resources();

        var scope = resources.Web.Services.CreateScope();

        var p1 = await resources.CreateMerchant();
        var p2 = await resources.CreatePersonWithBalance(receiverInitialBalance);

        var createInput = new TransferInput()
        {
            Sender = p1.CNPJ,
            Receiver = p2.CPF,
            Ammount = 100
        };

        var transferPayload = createInput.AsPayload();

        //When
        var response = await client.PostAsync(TransferEndpoints.TRANSFER, transferPayload);
        var content = await response.Content.ReadAsStringAsync();

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        var result = content.FromPayload<BaseError>();

        result.Should().NotBeNull();
        result!.Kind.Should().Be(TransferErrorType.UNSUPORTED_SENDER.ToString());
    }
}

