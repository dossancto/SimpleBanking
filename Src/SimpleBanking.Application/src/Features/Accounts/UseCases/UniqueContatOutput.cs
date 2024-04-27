using SimpleBanking.Domain.Features.Accounts.Entities;

namespace SimpleBanking.Application.Features.Accounts.UseCases;

/// <summary>
/// represents data used to search for unique input
/// </summary>
public class UniqueContatOutput
{
    /// <summary>
    /// THe id of the conflited record
    /// </summary>
    public required string? ConflictId { get; set; }

    /// <summary>
    /// THe id of the conflited record
    /// </summary>
    public required Account? Data { get; set; }

    /// <summary>
    /// THe field in conflict. It can be Email, CPF, CNPJ, ID etc
    /// </summary>
    public required string? ConflictField { get; set; }

    /// <summary>
    /// THe field in conflict. It can be Email, CPF, CNPJ, ID etc
    /// </summary>
    public required ConlitedEnum UserType { get; set; }

    /// <summary>
    /// Indicates if the Has some conflict.
    /// </summary>
    public bool IsUnique => Data is null;

    public static UniqueContatOutput Unique()
      => new()
      {
          ConflictField = null,
          Data = null,
          ConflictId = null,
          UserType = ConlitedEnum.None
      };
}

public enum ConlitedEnum
{
    None,
    Merchant,
    Person
}

