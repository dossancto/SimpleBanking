namespace SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;

/// <summary>
/// Represents a search for user using his Id
/// </summary>
public class SelectPersonByIdInput
{
    /// <summary>
    /// User id to be searched for
    /// </summary>
    public required string Id { get; set; }
}

