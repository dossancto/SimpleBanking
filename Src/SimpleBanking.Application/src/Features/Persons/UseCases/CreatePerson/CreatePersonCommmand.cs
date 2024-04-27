using MediatR;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

public class CreatePersonCommmand : IRequest<SafePerson>
{
    public required CreatePersonInput Input { get; set; }
}
