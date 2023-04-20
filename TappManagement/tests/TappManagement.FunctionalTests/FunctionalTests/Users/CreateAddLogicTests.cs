namespace TappManagement.FunctionalTests.FunctionalTests.Users;

using TappManagement.SharedTestHelpers.Fakes.AddLogic;
using TappManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateAddLogicTests : TestBase
{
    [Fact]
    public async Task create_addlogic_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeAddLogic = new FakeAddLogicForCreationDto().Generate();

        // Act
        var route = ApiRoutes.Users.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeAddLogic);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}