using FluentValidation;
using SimpleBanking.Adapters.Hash;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Application.Validations;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

/// <summary>
/// Representsa business logic for creating a new person
/// </summary>
public sealed class CreatePersonUseCase
(
   IPersonRepository _personRepository,
   IPasswordHasher _passwordHasher,
   IValidator<CreatePersonInput> _createPersonInputValidator
 ) : IUseCase
{
    public async Task<SafePerson> Execute(CreatePersonInput input)
    {
        _createPersonInputValidator.Check(input, "Fail while creating a user, invalid person");

        var hashedPassword = _passwordHasher.Hash(new()
        {
            Password = input.Password
        });

        var model = ToModel(input, hashedPassword);

        var personId = await _personRepository.Insert(model);

        model.Id = personId;

        return model.SafeData();
    }

    public Person ToModel(CreatePersonInput i, HashPassword hash)
    => new()
    {
        Balance = new()
        {
            Debit = 0
        },
        CPF = i.CPF,
        EmailAddress = i.Email,
        ResponsableFullName = i.FullName,
        HashedPassword = hash.HashedPassword,
        Salt = hash.Salt
    };
}
