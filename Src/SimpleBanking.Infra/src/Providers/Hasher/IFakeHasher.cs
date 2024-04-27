using SimpleBanking.Adapters.Hash;

namespace SimpleBanking.Infra.Providers.Hasher;

public class IFakeHasher : IPasswordHasher
{
    public HashPassword Hash(HashPasswordInput input)
    => new()
    {
        HashedPassword = "",
        Salt = ""
    };

    public bool Verify(HashVerifyPasswordInput input)
    => true;
}

