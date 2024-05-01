namespace SimpleBanking.Application.Features.Accounts.UseCases.UniqueContact;

/// <summary>
/// represents data used to search for unique input
/// </summary>
public class UniqueContactInput
{
    /// <summary>
    /// Search using this term. It can be Email, CPF, CNPJ, ID etc
    /// </summary>
    public required string SeachTerm { get; set; }
}


/// <summary>
/// represents data used to search for unique input
/// This method serach for many fields.
/// All non null fields will be seached individually
/// </summary>
public class UniqueContactInfosInput
{
    /// <summary>
    /// CPF
    /// </summary>
    public string? CPF { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// CNPJ
    /// </summary>
    public string? CNPJ { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public string? Id { get; set; }

    public IEnumerable<string?> Fields()
      => [
      CPF,
      Email,
      CNPJ,
      Id
    ];
}

