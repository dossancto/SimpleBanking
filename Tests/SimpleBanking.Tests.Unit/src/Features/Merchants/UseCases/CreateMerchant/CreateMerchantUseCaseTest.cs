using SimpleBanking.Adapters.Hash;
using SimpleBanking.Application.Features.Accounts.UseCases.UniqueContact;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Features.Merchants.UseCases.CreateMerchant;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Domain.Exceptions;

namespace SimpleBanking.Tests.Unit.Features.Merchants.UseCases.CreateMerchant;

public class CreateMerchantUseCaseTest
{
    [Fact]
    public async Task CreateMerchantUseCase_ValidInfos_ShouldReturnCreatedMerchant()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var merchantRepository = new Mock<IMerchantRepository>();

        var passwordHasher = new Mock<IPasswordHasher>();

        var createmerchantInputValidator = new CreateMerchantValidator();

        var uniqueContactUseCase = new UniqueContactUseCase(personRepository.Object, merchantRepository.Object);

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreateMerchantUseCase(
            _merchantRepository: merchantRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createMerchantValidator: createmerchantInputValidator,
            _uniqueContact: uniqueContactUseCase
            );

        var createmerchantInput = new CreateMerchantInput()
        {
            CNPJ = "92211617069",
            Email = "invalid_email",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        //When
        var task = () => usecase.Execute(createmerchantInput);

        var err = await Assert.ThrowsAsync<ValidationFailException>(task);

        //Then
        err.Errors.First().Field.Should().Be("Email");
        err.Errors.Should().HaveCount(1);

    }

}
