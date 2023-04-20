namespace TappManagement.FunctionalTests.FunctionalTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteUserTests : TestBase
{
    [Fact]
    public async Task delete_user_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeUser = new FakeUserBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Build();
        await InsertAsync(fakeUser);

        // Act
        var route = ApiRoutes.Users.Delete(fakeUser.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}