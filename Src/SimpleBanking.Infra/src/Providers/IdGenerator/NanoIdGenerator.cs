namespace SimpleBanking.Infra.Providers.IdGenerator;

public class NanoIdGenerator : IIdGenerator
{
    public string Generate()
    => NanoidDotNet.Nanoid.Generate();
}

