using Aether.Leagues.Adapters.UnitOfWorks;
using SimpleBanking.Adapters.Transfering;
using SimpleBanking.Application.Features.Accounts.UseCases;
using SimpleBanking.Application.Features.Merchants.Data;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Balances.Exceptions;
using SimpleBanking.Domain.Features.Merchants.Entities;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Balances.UseCases.Transfer;

public class TransferUseCase(
    UniqueContactUseCase _uniqueContact,
    IPersonRepository _personRepository,
    IMerchantRepository _merchantRepository,
    IUnitOfWork _uow,
    ITransferAuthorizerAdapter _transferAuthorizer
    ) : IUseCase
{
    public async Task Execute(TransferInput input)
    {
        var sender = await GetSenderContact(input);
        var receiver = await GetReceiverContact(input);

        await AssertAuthorized();

        await Move(sender, receiver, input.Ammount);
    }

    private async Task AssertAuthorized()
    {
        var isAuthorized = await _transferAuthorizer.Authorize();

        if (!isAuthorized)
        {
            throw new TransferException("Not Authorized");
        }
    }

    private async Task Move(UniqueContatOutput sender, UniqueContatOutput receiver, int balance)
    {
        Func<Task> senderRepo = sender.Data switch
        {
            Person p => () => _personRepository.MoveBalance(p.Id, -balance),
            _ => throw new TransferException("This sender is not supported")
        };

        Func<Task> receiverRepo = receiver.Data switch
        {
            Person p => () => _personRepository.MoveBalance(p.Id, balance),
            Merchant m => () => _merchantRepository.MoveBalance(m.Id, balance),
            _ => throw new TransferException("Receiver not supported")
        };

        if (sender.Data.Balance.Debit < balance)
        {
            throw new TransferException("Insuficient ammount");
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

        if (senderT.UserType == ConlitedEnum.Merchant)
        {
            throw new TransferException("Merchant can't transfer");
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
