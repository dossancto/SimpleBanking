using static SimpleBanking.Infra.Utils.Anvs.Anv;

namespace SimpleBanking.Infra.Utils.Anvs;

public static partial class Anv
{
    public static readonly EnvironmentAnv ENVIRONMENT = new();
}

public class EnvironmentAnv
{
    public readonly AnvEnv ASPNETCORE_ENVIRONMENT = Anv.EnvLoad("ASPNETCORE_ENVIRONMENT");
}


