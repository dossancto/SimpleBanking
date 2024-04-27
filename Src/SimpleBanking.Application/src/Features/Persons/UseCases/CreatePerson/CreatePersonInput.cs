namespace SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

/// <summary>
/// A model representing infos needed to create a new Person
/// </summary>
public sealed class CreatePersonInput
{
    /// <summary>
    /// The person fullname
    /// </summary>
    public required string FullName { get; set; } 

    /// <summary>
    /// Represents the Person email
    /// </summary>
    public required string Email { get; set; } 

    /// <summary>
    /// Represents the Person password to be hashed so stored
    /// </summary>
    public required string Password { get; set; } 

    /// <summary>
    /// The user CPF. Any non digit character will be removed
    /// </summary>
    /// <example>92211617069</example>
    public required string CPF { get; set; } 
}
