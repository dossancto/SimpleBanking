using SimpleBanking.Domain.Features.Balances.Entities;

namespace SimpleBanking.Domain.Features.Persons.Entities;

/// <summary>
/// Represents a Person Infos without sensive data
/// </summary>
public class SafePerson
{
    /// <summary>
    /// This person unique identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

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
    /// This person CPF
    /// </summary>
    public string CPF { get; set; } = string.Empty;

    /// <summary>
    /// Represents information about Balance
    /// </summary>
    public Balance Balance { get; set; } = new();
}

/// <summary>
/// SafePerson utils for handling data
/// </summary>
public static class SafePersonExtension
{
    /// <summary>
    /// Converts a person to a safe version
    /// </summary>
    public static SafePerson SafeData(this Person p)
      => new()
      {
          Id = p.Id,
          ResponsableFullName = p.ResponsableFullName,
          EmailAddress = p.EmailAddress,
          CPF = p.CPF,
          Balance = p.Balance
      };

    public static async Task<SafePerson> SafeData(this Task<Person> p)
      => SafeData(await p);
}
