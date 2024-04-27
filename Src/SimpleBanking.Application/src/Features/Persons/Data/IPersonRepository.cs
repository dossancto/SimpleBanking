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
}
