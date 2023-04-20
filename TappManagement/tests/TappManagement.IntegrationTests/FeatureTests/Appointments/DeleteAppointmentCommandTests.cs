namespace TappManagement.IntegrationTests.FeatureTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.Domain.Appointments.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class DeleteAppointmentCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_appointment_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAppointmentOne = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeAppointmentOne);
        var appointment = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments
            .FirstOrDefaultAsync(a => a.Id == fakeAppointmentOne.Id));

        // Act
        var command = new DeleteAppointment.Command(appointment.Id);
        await testingServiceScope.SendAsync(command);
        var appointmentResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments.CountAsync(a => a.Id == appointment.Id));

        // Assert
        appointmentResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_appointment_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteAppointment.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_appointment_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAppointmentOne = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeAppointmentOne);
        var appointment = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments
            .FirstOrDefaultAsync(a => a.Id == fakeAppointmentOne.Id));

        // Act
        var command = new DeleteAppointment.Command(appointment.Id);
        await testingServiceScope.SendAsync(command);
        var deletedAppointment = await testingServiceScope.ExecuteDbContextAsync(db => db.Appointments
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == appointment.Id));

        // Assert
        deletedAppointment?.IsDeleted.Should().BeTrue();
    }
}