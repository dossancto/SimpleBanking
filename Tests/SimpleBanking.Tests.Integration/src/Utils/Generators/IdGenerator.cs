namespace SimpleBanking.Tests.Integration.Utils.Generators;

public static class IdGenerator
{
    public static string Safe(int size = 10)
    {
        return NanoidDotNet.Nanoid.Generate(alphabet: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890", size: 10);
    }
}
