namespace TappManagement.FunctionalTests.FunctionalTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateUserTests : TestBase
{
    [Fact]
    public async Task create_user_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeUser = new FakeUserForCreationDto()
            .RuleFor(u => u.AppointmentId, _ => fakeICollection<Appointment>One.Id).Generate();

        // Act
        var route = ApiRoutes.Users.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeUser);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}