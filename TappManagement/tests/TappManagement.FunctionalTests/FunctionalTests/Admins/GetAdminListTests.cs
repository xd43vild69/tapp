namespace TappManagement.FunctionalTests.FunctionalTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetAdminListTests : TestBase
{
    [Fact]
    public async Task get_admin_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Admins.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}