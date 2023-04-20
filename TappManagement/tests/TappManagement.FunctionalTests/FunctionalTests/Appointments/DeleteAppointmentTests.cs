namespace TappManagement.FunctionalTests.FunctionalTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class DeleteAppointmentTests : TestBase
{
    [Fact]
    public async Task delete_appointment_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAppointment = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id).Build();
        await InsertAsync(fakeAppointment);

        // Act
        var route = ApiRoutes.Appointments.Delete(fakeAppointment.Id);
        var result = await FactoryClient.DeleteRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}