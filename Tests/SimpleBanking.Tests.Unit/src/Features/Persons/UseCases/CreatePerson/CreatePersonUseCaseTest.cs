using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Adapters.Hash;
using SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Application.Features.Accounts.UseCases;
using SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;

namespace SimpleBanking.Tests.Unit.Features.Persons.UseCases.CreatePerson;

public class CreatePersonUseCaseTest
{
    [Fact]
    public async Task CreatePersonUseCase_InvalidEmail_ShoudThrowValidationError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var createPersonInputValidator = new CreatePersonValidator();

        var uniqueContactUseCase = new UniqueContactUseCase(personRepository.Object);

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreatePersonUseCase(
            _personRepository: personRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createPersonInputValidator: createPersonInputValidator,
            _uniqueContact: uniqueContactUseCase
            );

        var createPersonInput = new CreatePersonInput()
        {
            CPF = "92211617069",
            Email = "invalid_email",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        //When
        var task = () => usecase.Execute(createPersonInput);

        var err = await Assert.ThrowsAsync<ValidationFailException>(task);

        //Then
        err.Errors.First().Field.Should().Be("Email");
        err.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task CreatePersonUseCase_NonNumericCPF_ShoudThrowValidationError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var createPersonInputValidator = new CreatePersonValidator();

        var uniqueContactUseCase = new UniqueContactUseCase(personRepository.Object);

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreatePersonUseCase(
            _personRepository: personRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createPersonInputValidator: createPersonInputValidator,
            _uniqueContact: uniqueContactUseCase
            );

        var createPersonInput = new CreatePersonInput()
        {
            CPF = "string",
            Email = "test@test.com",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        //When
        var task = () => usecase.Execute(createPersonInput);

        var err = await Assert.ThrowsAsync<ValidationFailException>(task);

        //Then
        err.Errors.First().Field.Should().Be("CPF");
        err.Errors.Should().HaveCount(2);
    }

    [Fact]
    public async Task CreatePersonUseCase_WeakPassword_ShoudThrowValidationError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var createPersonInputValidator = new CreatePersonValidator();

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreatePersonUseCase(
            _personRepository: personRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createPersonInputValidator: createPersonInputValidator,
            _uniqueContact: default!
            );

        var createPersonInput = new CreatePersonInput()
        {
            CPF = "92211617069",
            Email = "test@test.com",
            FullName = "John Doe",
            Password = "password123"
        };

        //When
        var task = () => usecase.Execute(createPersonInput);

        var err = await Assert.ThrowsAsync<ValidationFailException>(task);

        //Then
        err.Errors.Should().AllSatisfy(x =>
        {
            x.Field.Should().Be("Password");
        });

        err.Errors.Should().HaveCount(2);
    }

    [Fact]
    public async Task CreatePersonUseCase_InvalidCPF_ShoudThrowValidationError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var createPersonInputValidator = new CreatePersonValidator();

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreatePersonUseCase(
            _personRepository: personRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createPersonInputValidator: createPersonInputValidator,
            _uniqueContact: default!
            );

        var createPersonInput = new CreatePersonInput()
        {
            CPF = "11111111111",
            Email = "test@test.com",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        //When
        var task = () => usecase.Execute(createPersonInput);

        var err = await Assert.ThrowsAsync<ValidationFailException>(task);

        //Then
        err.Errors.Should().AllSatisfy(x =>
        {
            x.Field.Should().Be("CPF");
        });

        err.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task CreatePersonUseCase_TooBigValues_ShoudThrowValidationError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var createPersonInputValidator = new CreatePersonValidator();

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreatePersonUseCase(
            _personRepository: personRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createPersonInputValidator: createPersonInputValidator,
            _uniqueContact: default!
            );

        var createPersonInput = new CreatePersonInput()
        {
            CPF = "92211617069",
            Email = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test@test.com",
            FullName = "111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111test@test.com",
            Password = "S#cur3Password"
        };

        //When
        var task = () => usecase.Execute(createPersonInput);

        var err = await Assert.ThrowsAsync<ValidationFailException>(task);

        //Then
        err.Errors.Should().AllSatisfy(x =>
        {
            x.Field.Should().BeOneOf("FullName", "Email");
        });

        err.Errors.Should().HaveCount(2);
    }

    [Fact]
    public async Task CreatePersonUseCase_ExistentInfos_ShoudThrowEntityError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var createPersonInputValidator = new CreatePersonValidator();

        personRepository
          .Setup(x => x.SearchByTerm(It.IsAny<SearchPersonByTermInput>()))
          .ReturnsAsync(new Domain.Features.Persons.Entities.Person());

        var uniqueContactUseCase = new UniqueContactUseCase(personRepository.Object);

        passwordHasher
          .Setup(x => x.Hash(It.IsAny<HashPasswordInput>()))
          .Returns(new HashPassword()
          {
              HashedPassword = "some-thing",
              Salt = "salt"
          });

        var usecase = new CreatePersonUseCase(
            _personRepository: personRepository.Object,
            _passwordHasher: passwordHasher.Object,
            _createPersonInputValidator: createPersonInputValidator,
            _uniqueContact: uniqueContactUseCase
            );

        var createPersonInput = new CreatePersonInput()
        {
            CPF = "92211617069",
            Email = "test@test.com",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        //When
        var task = () => usecase.Execute(createPersonInput);

        var err = await Assert.ThrowsAsync<EntityAlreadyExistsException>(task);

        //Then
        err.Message.Should().Contain("92211617069");
    }

}
