using SimpleBanking.Domain.Features.Accounts.Entities;
using SimpleBanking.Domain.Features.Balances.Entities;

namespace SimpleBanking.Domain.Features.Persons.Entities;

/// <summary>
/// Represents a Person Account
/// </summary>
public class Person : Account
{
    /// <summary>
    /// This person unique identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// This person CPF
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Represents information about Balance
    /// </summary>
    public Balance Balance { get; set; } = new();

}
