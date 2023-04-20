namespace TappManagement.IntegrationTests.FeatureTests.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.Domain.Appointments.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class AppointmentQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_appointment_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAppointmentOne = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeAppointmentOne);

        // Act
        var query = new GetAppointment.Query(fakeAppointmentOne.Id);
        var appointment = await testingServiceScope.SendAsync(query);

        // Assert
        appointment.Size.Should().Be(fakeAppointmentOne.Size);
        appointment.Location.Should().Be(fakeAppointmentOne.Location);
        appointment.Description.Should().Be(fakeAppointmentOne.Description);
        appointment.Reference.Should().Be(fakeAppointmentOne.Reference);
        appointment.Payment.Should().Be(fakeAppointmentOne.Payment);
        appointment.Approve.Should().Be(fakeAppointmentOne.Approve);
        appointment.UserId.Should().Be(fakeAppointmentOne.UserId);
    }

    [Fact]
    public async Task get_appointment_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetAppointment.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}