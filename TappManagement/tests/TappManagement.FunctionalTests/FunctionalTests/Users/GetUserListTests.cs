namespace TappManagement.FunctionalTests.FunctionalTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetUserListTests : TestBase
{
    [Fact]
    public async Task get_user_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Users.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}