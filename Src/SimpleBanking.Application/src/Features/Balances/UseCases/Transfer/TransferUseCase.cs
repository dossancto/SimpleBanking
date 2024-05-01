using MediatR;

using SimpleBanking.Adapters.Transfering;
using SimpleBanking.Application.Events.Transfer.MoneyTransfered;
using SimpleBanking.Application.Features.Accounts.UseCases.UniqueContact;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Features.Persons.Data;

using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Domain.Features.Merchants.Entities;
using SimpleBanking.Domain.Features.Persons.Entities;
using SimpleBanking.Domain.Providers.Services;

namespace SimpleBanking.Application.Features.Balances.UseCases.Transfer;

public class TransferUseCase(
    UniqueContactUseCase _uniqueContact,
    IPersonRepository _personRepository,
    IMerchantRepository _merchantRepository,
    IUnitOfWork _uow,
    IMediator _mediator,
    ITransferAuthorizerAdapter _transferAuthorizer
    ) : IUseCase
{
    public async Task Execute(TransferInput input)
    {
        var sender = await GetSenderContact(input);
        var receiver = await GetReceiverContact(input);

        await AssertAuthorized();

        await Move(sender, receiver, input.Ammount);

        await _mediator.Publish(new MoneyTransferedNotification()
        {
            Ammount = input.Ammount,
            Receiver = receiver.Data!,
            Sender = sender.Data!
        });
    }

    private async Task AssertAuthorized()
    {
        var isAuthorized = await _transferAuthorizer.Authorize();

        if (!isAuthorized)
        {
            throw new TransferException("Not Authorized")
            {
                ErrorType = TransferErrorType.NOT_AUTHORIZED,
                Details = "Our authorization server refused the transfer"
            };
        }
    }

    private async Task Move(UniqueContatOutput sender, UniqueContatOutput receiver, int balance)
    {
        Func<Task> senderRepo = sender.Data switch
        {
            Person p => () => _personRepository.MoveBalance(p.Id, -balance),
            _ => throw new TransferException("This sender is not supported")
            {
                ErrorType = TransferErrorType.UNSUPORTED_SENDER
            }
        };

        Func<Task> receiverRepo = receiver.Data switch
        {
            Person p => () => _personRepository.MoveBalance(p.Id, balance),
            Merchant m => () => _merchantRepository.MoveBalance(m.Id, balance),
            _ => throw new TransferException("Receiver not supported")
            {
                ErrorType = TransferErrorType.UNSUPORTED_RECEIVER
            }
        };

        if (sender.Data.Balance.Debit < balance)
        {
            throw new TransferException("Insuficient ammount")
            {
                ErrorType = TransferErrorType.INSUFICIENT_AMMOUNT,
                Details = $"You do not have the required ammount to transfer"
            };
        }

        var work = async () =>
        {
            await senderRepo();
            await receiverRepo();
        };

        await _uow.Sandbox(work);
    }

    private async Task<UniqueContatOutput> GetSenderContact(TransferInput input)
    {
        var senderT = await _uniqueContact.Execute(new UniqueContactInput()
        {
            SeachTerm = input.Sender
        });

        if (senderT.IsUnique)
        {
            throw new NotFoundException($"Sender user with {input.Sender} not found.");
        }

        if (senderT.UserType is ConlitedEnum.Merchant)
        {
            throw new TransferException("Merchant can't transfer")
            {
                ErrorType = TransferErrorType.UNSUPORTED_SENDER
            };
        }

        return senderT;
    }

    private async Task<UniqueContatOutput> GetReceiverContact(TransferInput input)
    {
        var senderT = await _uniqueContact.Execute(new UniqueContactInput()
        {
            SeachTerm = input.Receiver
        });

        if (senderT.IsUnique)
        {
            throw new NotFoundException($"Receiver user with {input.Sender} not found.");
        }

        return senderT;
    }
}
