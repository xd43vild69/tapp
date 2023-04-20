namespace TappManagement.FunctionalTests.FunctionalTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateAppointmentRecordTests : TestBase
{
    [Fact]
    public async Task put_appointment_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeUserOne = new FakeUserBuilder().Build();
        await InsertAsync(fakeUserOne);

        var fakeAppointment = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id).Build();
        var updatedAppointmentDto = new FakeAppointmentForUpdateDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id)
            .Generate();
        await InsertAsync(fakeAppointment);

        // Act
        var route = ApiRoutes.Appointments.Put(fakeAppointment.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedAppointmentDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}