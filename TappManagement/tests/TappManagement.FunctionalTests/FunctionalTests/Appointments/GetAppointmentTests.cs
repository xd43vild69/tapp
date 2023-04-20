namespace TappManagement.FunctionalTests.FunctionalTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetAppointmentTests : TestBase
{
    [Fact]
    public async Task get_appointment_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAppointment = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id).Build();
        await InsertAsync(fakeAppointment);

        // Act
        var route = ApiRoutes.Appointments.GetRecord(fakeAppointment.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}