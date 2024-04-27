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
        if (sender.Data is Person senderPerson)
        {
            await _personRepository.MoveBalance(senderPerson.Id, balance);
        }
        else
        {
            // TODO: Change to a better exception
            throw new Exception("Merchant can't transfer");
        }

        if (receiver.Data is Person p)
        {
            await _personRepository.MoveBalance(p.Id, balance);
        }
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
