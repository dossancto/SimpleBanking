namespace SimpleBanking.Adapters.Hash;

/// <summary>
/// Represents a password Hasher and validator
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashed a password
    /// </summary>
    HashPassword Hash(HashPasswordInput input);

    /// <summary>
    /// Verifies if the provided password is valid
    /// </summary>
    /// <returns>Boolean indicating if the password is valid</returns>
    bool Verify(HashVerifyPasswordInput input);
}
