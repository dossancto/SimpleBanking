namespace SimpleBanking.Tests.Integration.Helpers.Creation;

public class HelperResources
{
    public required IntegrationTestWebApplicationFactory Web;
}

public static class HelperResourcesExtension
{
    public static HelperResources Resources(this IntegrationTestWebApplicationFactory web)
    => new()
    {
        Web = web
    };
}
