namespace SimpleBanking.Adapters.Hash;

/// <summary>
/// Represents the a Hash Password Request
/// </summary>
public record HashPasswordInput
{
    /// <summary>
    /// The password to be hashed
    /// </summary>
    public required string Password { get; set; }
}


