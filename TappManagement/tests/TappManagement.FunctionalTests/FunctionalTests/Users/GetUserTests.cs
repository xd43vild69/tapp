namespace TappManagement.FunctionalTests.FunctionalTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetUserTests : TestBase
{
    [Fact]
    public async Task get_user_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeUser = new FakeUserBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Build();
        await InsertAsync(fakeUser);

        // Act
        var route = ApiRoutes.Users.GetRecord(fakeUser.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}