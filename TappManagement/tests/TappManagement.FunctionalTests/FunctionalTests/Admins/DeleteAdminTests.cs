namespace TappManagement.FunctionalTests.FunctionalTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteAdminTests : TestBase
{
    [Fact]
    public async Task delete_admin_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAdmin = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id).Build();
        await InsertAsync(fakeAdmin);

        // Act
        var route = ApiRoutes.Admins.Delete(fakeAdmin.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}