namespace TappManagement.FunctionalTests.FunctionalTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateUserRecordTests : TestBase
{
    [Fact]
    public async Task put_user_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeUser = new FakeUserBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Build();
        var updatedUserDto = new FakeUserForUpdateDto()
            .RuleFor(u => u.AppointmentId, _ => fakeICollection<Appointment>One.Id)
            .Generate();
        await InsertAsync(fakeUser);

        // Act
        var route = ApiRoutes.Users.Put(fakeUser.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedUserDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}