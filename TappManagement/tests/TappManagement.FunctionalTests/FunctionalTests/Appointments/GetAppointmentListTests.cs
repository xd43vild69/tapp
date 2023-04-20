namespace TappManagement.FunctionalTests.FunctionalTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetAppointmentListTests : TestBase
{
    [Fact]
    public async Task get_appointment_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Appointments.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}