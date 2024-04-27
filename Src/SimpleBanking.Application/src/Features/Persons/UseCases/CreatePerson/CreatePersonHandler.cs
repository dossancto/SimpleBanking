using MediatR;
using SimpleBanking.Domain.Features.Persons.Entities;

namespace SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;

/// <summary>
/// Handle Person Creation event
/// </summary>
public class CreatePersonHandler(
    CreatePersonUseCase createPerson
    ) : IRequestHandler<CreatePersonCommmand, SafePerson>
{
    public Task<SafePerson> Handle(CreatePersonCommmand request, CancellationToken cancellationToken)
    {
        return createPerson.Execute(request.Input);
    }
}
