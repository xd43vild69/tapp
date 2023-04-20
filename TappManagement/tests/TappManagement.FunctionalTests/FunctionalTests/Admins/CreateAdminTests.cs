namespace TappManagement.FunctionalTests.FunctionalTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateAdminTests : TestBase
{
    [Fact]
    public async Task create_admin_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAdmin = new FakeAdminForCreationDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id).Generate();

        // Act
        var route = ApiRoutes.Admins.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeAdmin);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}