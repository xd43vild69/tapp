namespace TappManagement.FunctionalTests.FunctionalTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateAppointmentTests : TestBase
{
    [Fact]
    public async Task create_appointment_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAppointment = new FakeAppointmentForCreationDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id).Generate();

        // Act
        var route = ApiRoutes.Appointments.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeAppointment);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}