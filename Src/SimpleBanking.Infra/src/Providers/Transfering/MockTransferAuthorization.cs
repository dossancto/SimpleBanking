using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimpleBanking.Adapters.Transfering;
using SimpleBanking.Domain.Features.Balances.Exceptions;

namespace SimpleBanking.Infra.Providers.Transfering;

public class MockTransferAuthorization(
    ILogger<MockTransferAuthorization> logger
    ) : ITransferAuthorizerAdapter
{
    // This Service is not longer avaible. So I will mock always a success message
    public readonly string BASE_URL = "https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc";

    private readonly ILogger<MockTransferAuthorization> logger = logger;

    public Task<bool> Authorize()
    {
        return Task.FromResult(true);
        // return await RealMethod();
    }


    private async Task<bool> RealMethod()
    {
        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync(BASE_URL);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            logger.LogError("Request failed with status code: {StatusCode}, content: {Content}", response.StatusCode, content);

            throw new TransferException("Fail while making request")
            {
                ErrorType = TransferErrorType.NOT_AUTHORIZED,
                Details = $"Request to authorization service failed."
            };
        }

        var result = JsonConvert.DeserializeObject<MockTransferResponse>(content);

        if (result is null)
        {
            throw new TransferException("Fail while deserializing response")
            {
                ErrorType = TransferErrorType.NOT_AUTHORIZED,
                Details = "Unexpected response"
            };
        }

        if (result.Message == "Autorizado")
        {
            return true;
        }

        return false;
    }
}

class MockTransferResponse
{
    [JsonProperty("message")]
    public string Message { get; set; } = default!;
}
