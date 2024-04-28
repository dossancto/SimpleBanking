using SimpleBanking.Application.Features.Accounts.UseCases;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Tests.Unit.Features.Balances.Transfer;

public class TransferUseCaseTest
{
    [Fact]
    public async Task TransferUseCase_UserCannotBeFount_ShouldThrowNotFoundError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var merchantRepository = new Mock<IMerchantRepository>();

        var uniqueUsecase = new UniqueContactUseCase(
            _personRepository: personRepository.Object,
            _merchantRepository: merchantRepository.Object
            );

        var usecase = new TransferUseCase(
            _uniqueContact: uniqueUsecase,
            _personRepository: personRepository.Object,
            _merchantRepository: merchantRepository.Object
            );

        var input = new TransferInput()
        {
            Ammount = 20,
            Sender = "cpf",
            Receiver = "other-cpf"
        };

        //When
        var task = () => usecase.Execute(input);

        await Assert.ThrowsAsync<NotFoundException>(task);

        //Then
        personRepository.Verify(x => x.MoveBalance(It.IsAny<string>(), It.IsAny<int>()), Times.Never());
    }

    [Fact]
    public async Task TransferUseCase_ValidInfos_ShouldTransfer()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var merchantRepository = new Mock<IMerchantRepository>();

        personRepository
          .Setup(x => x.SearchByTerm(It.Is<SearchPersonByTermInput>(x => x.Term == "cpf")))
          .ReturnsAsync(new Person()
          {
              Id = "1",
              Balance = new()
              {
                  Debit = 200,
              }
          });

        personRepository
          .Setup(x => x.SearchByTerm(It.Is<SearchPersonByTermInput>(x => x.Term == "other-cpf")))
          .ReturnsAsync(new Person()
          {
              Id = "2",
              Balance = new()
              {
                  Debit = 200,
              }
          });

        var uniqueUsecase = new UniqueContactUseCase(
            _personRepository: personRepository.Object,
            _merchantRepository: merchantRepository.Object
            );

        var usecase = new TransferUseCase(
            _uniqueContact: uniqueUsecase,
            _personRepository: personRepository.Object,
            _merchantRepository: merchantRepository.Object
            );

        var input = new TransferInput()
        {
            Ammount = 20,
            Sender = "cpf",
            Receiver = "other-cpf"
        };

        //When
        await usecase.Execute(input); ;

        //Then
        personRepository.Verify(x => x.MoveBalance(It.IsAny<string>(), It.IsAny<int>()), Times.Exactly(2));

        personRepository.Verify(x => x.MoveBalance(It.Is<string>(x => x == "1"), It.Is<int>(x => x == -20)), Times.Once());
        personRepository.Verify(x => x.MoveBalance(It.Is<string>(x => x == "2"), It.Is<int>(x => x == 20)), Times.Once());
    }

    [Fact]
    public async Task TransferUseCase_InsuficientAmmount_ShouldThrowTransferenceError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var merchantRepository = new Mock<IMerchantRepository>();

        personRepository
          .Setup(x => x.SearchByTerm(It.Is<SearchPersonByTermInput>(x => x.Term == "cpf")))
          .ReturnsAsync(new Person()
          {
              Id = "1",
              Balance = new()
              {
                  Debit = 10,
              }
          });

        personRepository
          .Setup(x => x.SearchByTerm(It.Is<SearchPersonByTermInput>(x => x.Term == "other-cpf")))
          .ReturnsAsync(new Person()
          {
              Id = "2",
              Balance = new()
              {
                  Debit = 200,
              }
          });

        var uniqueUsecase = new UniqueContactUseCase(
            _personRepository: personRepository.Object,
            _merchantRepository: merchantRepository.Object
            );

        var usecase = new TransferUseCase(
            _uniqueContact: uniqueUsecase,
            _personRepository: personRepository.Object,
            _merchantRepository: merchantRepository.Object
            );

        var input = new TransferInput()
        {
            Ammount = 20,
            Sender = "cpf",
            Receiver = "other-cpf"
        };

        //When
        var task = () => usecase.Execute(input); ;
        await Assert.ThrowsAsync<TransferException>(task);

        //Then

        personRepository.Verify(x => x.MoveBalance(It.Is<string>(x => x == "1"), It.IsAny<int>()), Times.Never());
        personRepository.Verify(x => x.MoveBalance(It.Is<string>(x => x == "2"), It.IsAny<int>()), Times.Never());
    }
}
