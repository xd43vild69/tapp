namespace TappManagement.IntegrationTests.FeatureTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.Domain.Appointments.Features;
using SharedKernel.Exceptions;
using TappManagement.SharedTestHelpers.Fakes.User;

public class AddAppointmentCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_appointment_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAppointmentOne = new FakeAppointmentForCreationDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id).Generate();

        // Act
        var command = new AddAppointment.Command(fakeAppointmentOne);
        var appointmentReturned = await testingServiceScope.SendAsync(command);
        var appointmentCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments
            .FirstOrDefaultAsync(a => a.Id == appointmentReturned.Id));

        // Assert
        appointmentReturned.Size.Should().Be(fakeAppointmentOne.Size);
        appointmentReturned.Location.Should().Be(fakeAppointmentOne.Location);
        appointmentReturned.Description.Should().Be(fakeAppointmentOne.Description);
        appointmentReturned.Reference.Should().Be(fakeAppointmentOne.Reference);
        appointmentReturned.Payment.Should().Be(fakeAppointmentOne.Payment);
        appointmentReturned.Approve.Should().Be(fakeAppointmentOne.Approve);
        appointmentReturned.UserId.Should().Be(fakeAppointmentOne.UserId);

        appointmentCreated.Size.Should().Be(fakeAppointmentOne.Size);
        appointmentCreated.Location.Should().Be(fakeAppointmentOne.Location);
        appointmentCreated.Description.Should().Be(fakeAppointmentOne.Description);
        appointmentCreated.Reference.Should().Be(fakeAppointmentOne.Reference);
        appointmentCreated.Payment.Should().Be(fakeAppointmentOne.Payment);
        appointmentCreated.Approve.Should().Be(fakeAppointmentOne.Approve);
        appointmentCreated.UserId.Should().Be(fakeAppointmentOne.UserId);
    }
}