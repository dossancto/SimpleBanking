using SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Persons.Data;

/// <summary>
/// Represents operating for keeping Persons
/// </summary>
public interface IPersonRepository
{
    /// <summary>
    /// Saves a new Person
    /// </summary>
    /// <returns>A string Id indicating the new Person</returns>
    public Task<string> Insert(Person entity);

    /// <summary>
    /// Searches for a person using his id
    /// </summary>
    /// <returns>The seached person or null, if not found</returns>
    public Task<Person?> SelectById(SelectPersonByIdInput input);

    /// <summary>
    /// Searches a term 
    /// </summary>
    /// <returns>The seached person or null, if not found</returns>
    public Task<Person?> SearchByTerm(SearchPersonByTermInput input);
}
