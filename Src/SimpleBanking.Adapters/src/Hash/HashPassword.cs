namespace SimpleBanking.Adapters.Hash;

/// <summary>
/// Represents the HashedPassword
/// </summary>
public record HashPassword
{
    /// <summary>
    /// The Hashed Password
    /// </summary>
    public required string HashedPassword { get; set; }

    /// <summary>
    /// Salt used for more secure
    /// </summary>
    public required string Salt { get; set; }
}

