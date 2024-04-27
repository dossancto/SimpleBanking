using static SimpleBanking.Infra.Utils.Anvs.Anv;

namespace SimpleBanking.Infra.Utils.Anvs;

public static partial class Anv
{
    public static readonly AnvDatabase Database = new();
}

public class AnvDatabase
{
    public readonly AnvEnv POSTGRES_CONNECTION_STRING = Anv.EnvLoad("POSTGRES_CONNECTION_STRING");
}
