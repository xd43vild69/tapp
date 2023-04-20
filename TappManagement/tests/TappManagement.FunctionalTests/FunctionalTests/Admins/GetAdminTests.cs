namespace TappManagement.FunctionalTests.FunctionalTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetAdminTests : TestBase
{
    [Fact]
    public async Task get_admin_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAdmin = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id).Build();
        await InsertAsync(fakeAdmin);

        // Act
        var route = ApiRoutes.Admins.GetRecord(fakeAdmin.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}