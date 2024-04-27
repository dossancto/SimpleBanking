using System.Net;
using SimpleBanking.Application.Features.Persons.UseCases.CreatePerson;
using SimpleBanking.Domain.Features.Persons.Entities;
using SimpleBanking.Tests.Integration.Share.Endpoints;

namespace SimpleBanking.Tests.Integration.Tests.Controllers.Persons;

public class CreatePersonEndpointTest(IntegrationTestWebApplicationFactory web) : IntegrationTest(web)
{
    [Fact]
    public async Task TestNameAsync()
    {
        //Given
        var client = web.CreateClient();

        var createInput = new CreatePersonInput()
        {
            CPF = CPFGenerator.Generate(),
            Email = $"test{IdGenerator.Safe()}@test.com",
            FullName = "John Doe",
            Password = "S#cur3Password"
        };

        var createPersonPayload = createInput.AsPayload();

        //When
        var response = await client.PostAsync(PersonEndpoints.CREATE, createPersonPayload);
        var content = await response.Content.ReadAsStringAsync();

        //Then
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        var result = content.FromPayload<SafePerson>();

        result.Should().NotBeNull();
        result!.CPF.Should().Be(createInput.CPF);
        result!.EmailAddress.Should().Be(createInput.Email.ToLower());
    }

}
