using static SimpleBanking.Infra.Utils.Anvs.Anv;

namespace SimpleBanking.Infra.Utils.Anvs;

public static partial class Anv
{
    public static readonly TestAnv Test = new();
}

public class TestAnv
{
    public readonly AnvEnv USE_EF_LOG = Anv.EnvLoad("USE_EF_LOG");
}

