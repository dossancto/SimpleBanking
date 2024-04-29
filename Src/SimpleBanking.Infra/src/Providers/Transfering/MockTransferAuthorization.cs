using Newtonsoft.Json;
using SimpleBanking.Adapters.Transfering;

namespace SimpleBanking.Infra.Providers.Transfering;

public class MockTransferAuthorization : ITransferAuthorizerAdapter
{
    public readonly string BASE_URL = "https://run.mocky.io/v3/5794d450-d2e2-4412-8131-73d0293ac1cc";

    public async Task<bool> Authorize()
    {
        var httpClient = new HttpClient();

        var response = await httpClient.GetAsync(BASE_URL);

        var content = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<MockTransferResponse>(content)
          ?? throw new Exception("Fail while deseialing response");

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
