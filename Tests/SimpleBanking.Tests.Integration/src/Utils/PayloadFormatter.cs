using System.Text;
using Newtonsoft.Json;

namespace SimpleBanking.Tests.Integration.Utils;

public static class PayloadFormatter
{
    public static StringContent AsPayload<T>(this T obj)
      => new(JsonConvert.SerializeObject(obj), encoding: Encoding.UTF8, "application/json");

    public static T? FromPayload<T>(this string content)
      => JsonConvert.DeserializeObject<T>(content);

    public static async Task<T?> FromPayload<T>(this HttpContent content)
      => JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
}


