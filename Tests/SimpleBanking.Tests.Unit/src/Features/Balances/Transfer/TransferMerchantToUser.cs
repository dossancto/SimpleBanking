using MediatR;
using SimpleBanking.Adapters.Transfering;
using SimpleBanking.Application.Features.Accounts.UseCases.UniqueContact;
using SimpleBanking.Application.Features.Balances.UseCases.Transfer;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Features.Merchants.UseCases.SelectMerchant;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Application.Features.Persons.UseCases.SelectPerson;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Domain.Features.Merchants.Entities;
using SimpleBanking.Domain.Features.Persons.Entities;
using SimpleBanking.Tests.Unit.Mocks;

namespace SimpleBanking.Tests.Unit.Features.Balances.Transfer;

public class TransferMerchantToUser
{
    [Fact]
    public async Task TransferUseCase_UserCannotBeFount_ShouldThrowNotFoundError()
    {
        //Given
        var personRepository = new Mock<IPersonRepository>();
        var merchantRepository = new Mock<IMerchantRepository>();

        var unitOfWorkMock = new Mock<UnitOfWorkMock>();

        var transferAuthorization = new Mock<ITransferAuthorizerAdapter>();

        var mediator = new Mock<IMediator>();

        transferAuthorization
          .Setup(x => x.Authorize())
          .ReturnsAsync(true);

        merchantRepository
          .Setup(x => x.SearchByTerm(It.Is<SelectMerchantByTermInput>(x => x.Term == "cpf")))
          .ReturnsAsync(new Merchant()
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
            _merchantRepository: merchantRepository.Object,
            _uow: unitOfWorkMock.Object,
            _transferAuthorizer: transferAuthorization.Object,
            _mediator: mediator.Object
            );

        var input = new TransferInput()
        {
            Ammount = 20,
            Sender = "cpf",
            Receiver = "other-cpf"
        };

        //When
        var task = () => usecase.Execute(input); ;
        var ex = await Assert.ThrowsAsync<TransferException>(task);

        ex.ErrorType.Should().Be(TransferErrorType.UNSUPORTED_SENDER);

        //Then
        personRepository.Verify(x => x.MoveBalance(It.Is<string>(x => x == "1"), It.IsAny<int>()), Times.Never());
        personRepository.Verify(x => x.MoveBalance(It.Is<string>(x => x == "2"), It.IsAny<int>()), Times.Never());

        unitOfWorkMock.Verify(x => x.Begin(), Times.Never());
        unitOfWorkMock.Verify(x => x.Apply(), Times.Never());
        unitOfWorkMock.Verify(x => x.Rollback(), Times.Never());
    }
}

