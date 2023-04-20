namespace TappManagement.IntegrationTests.FeatureTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.Domain.Appointments.Dtos;
using SharedKernel.Exceptions;
using TappManagement.Domain.Appointments.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class UpdateAppointmentCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_appointment_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAppointmentOne = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        var updatedAppointmentDto = new FakeAppointmentForUpdateDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id)
            .Generate();
        await testingServiceScope.InsertAsync(fakeAppointmentOne);

        var appointment = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments
            .FirstOrDefaultAsync(a => a.Id == fakeAppointmentOne.Id));
        var id = appointment.Id;

        // Act
        var command = new UpdateAppointment.Command(id, updatedAppointmentDto);
        await testingServiceScope.SendAsync(command);
        var updatedAppointment = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedAppointment.Size.Should().Be(updatedAppointmentDto.Size);
        updatedAppointment.Location.Should().Be(updatedAppointmentDto.Location);
        updatedAppointment.Description.Should().Be(updatedAppointmentDto.Description);
        updatedAppointment.Reference.Should().Be(updatedAppointmentDto.Reference);
        updatedAppointment.Payment.Should().Be(updatedAppointmentDto.Payment);
        updatedAppointment.Approve.Should().Be(updatedAppointmentDto.Approve);
        updatedAppointment.UserId.Should().Be(updatedAppointmentDto.UserId);
    }
}