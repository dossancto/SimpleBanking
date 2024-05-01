using FluentValidation;
using SimpleBanking.Adapters.Hash;
using SimpleBanking.Application.Features.Accounts.UseCases.UniqueContact;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Validations;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Merchants.Entities;

namespace SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;

public class CreateMerchantUseCase(
    IMerchantRepository _merchantRepository,
    IValidator<CreateMerchantInput> _createMerchantValidator,
    IPasswordHasher _passwordHasher,
    UniqueContactUseCase _uniqueContact
    ) : IUseCase
{
    public async Task<Merchant> Execute(CreateMerchantInput input)
    {
        input.Format();

        _createMerchantValidator.Check(input, "Fail while creating a user, invalid merchant");

        var notUniqueField = await _uniqueContact.Execute(new UniqueContactInfosInput()
        {
            Email = input.Email,
            CNPJ = input.CNPJ
        });

        if (!notUniqueField.IsUnique)
        {
            throw new EntityAlreadyExistsException($"This user already exists, {notUniqueField.ConflictField}");
        }

        var hashedPassword = _passwordHasher.Hash(new()
        {
            Password = input.Password
        });

        var model = ToModel(input, hashedPassword);

        var personId = await _merchantRepository.Insert(model);

        model.Id = personId;

        // WARN: Return safe Merchant
        return model;
    }

    public Merchant ToModel(CreateMerchantInput i, HashPassword hash)
    => new()
    {
        Balance = new()
        {
            Debit = 0
        },
        CNPJ = i.CNPJ,
        EmailAddress = i.Email,
        ResponsableFullName = i.FullName,
        HashedPassword = hash.HashedPassword,
        Salt = hash.Salt
    };
}
