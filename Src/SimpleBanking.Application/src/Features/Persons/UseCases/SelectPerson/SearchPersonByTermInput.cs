namespace SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;

/// <summary>
/// Represents a search for user using a term
/// </summary>
public class SearchPersonByTermInput
{
    /// <summary>
    /// The term to be used
    /// </summary>
    public required string Term { get; set; }
}


