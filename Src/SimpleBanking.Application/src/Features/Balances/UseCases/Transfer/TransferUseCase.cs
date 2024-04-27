using SimpleBanking.Application.Features.Accounts.UseCases;
using SimpleBanking.Application.Features.Persons.Data;
using SimpleBanking.Domain.Exceptions;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Balances.Transfer;

public class TransferUseCase(
    UniqueContactUseCase _uniqueContact,
    IPersonRepository _personRepository
    ) : IUseCase
{
    public async Task Execute(TransferInput input)
    {
        var sender = await GetSenderContact(input);
        var receiver = await GetReceiverContact(input);

        await Move(sender, receiver, input.Ammount);
    }

    private async Task Move(UniqueContatOutput sender, UniqueContatOutput receiver, int balance)
    {
        Func<Task> senderRepo = sender.Data switch
        {
            Person p => () => _personRepository.MoveBalance(p.Id, -balance),
            _ => throw new Exception("This sender is not supported")
        };

        Func<Task> receiverRepo = receiver.Data switch
        {
            Person p => () => _personRepository.MoveBalance(p.Id, balance),
            _ => throw new Exception("Receiver not supported")
        };

        await senderRepo();
        await receiverRepo();
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
            // TODO: Change to a better exception
            throw new Exception("Merchant can't transfer");
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
