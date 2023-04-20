namespace TappManagement.FunctionalTests.FunctionalTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateAdminRecordTests : TestBase
{
    [Fact]
    public async Task put_admin_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAdmin = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id).Build();
        var updatedAdminDto = new FakeAdminForUpdateDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id)
            .Generate();
        await InsertAsync(fakeAdmin);

        // Act
        var route = ApiRoutes.Admins.Put(fakeAdmin.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedAdminDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}