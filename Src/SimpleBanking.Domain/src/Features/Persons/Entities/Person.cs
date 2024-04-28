using SimpleBanking.Domain.Features.Accounts.Entities;

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

}
