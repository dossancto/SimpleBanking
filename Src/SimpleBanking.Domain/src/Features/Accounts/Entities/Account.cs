using SimpleBanking.Domain.DomainTypes;
using SimpleBanking.Domain.Features.Balances.Entities;

namespace SimpleBanking.Domain.Features.Accounts.Entities;

/// <summary>
/// Represents a generic information about an account
/// </summary>
public class Account : BaseRecord
{
    /// <summary>
    /// The Fullname of the account owner
    /// </summary>
    /// <example>test@test.com</example>
    public string ResponsableFullName { get; set; } = string.Empty;

    /// <summary>
    /// A email email address associated with this account
    /// </summary>
    /// <example>test@test.com</example>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// The hashed secure account password
    /// </summary>
    public string HashedPassword { get; set; } = string.Empty;

    /// <summary>
    /// A secure key used as additional security mechanism for the account
    /// </summary>
    public string Salt { get; set; } = string.Empty;

    /// <summary>
    /// Represents information about Balance
    /// </summary>
    public Balance Balance { get; set; } = new();
}

