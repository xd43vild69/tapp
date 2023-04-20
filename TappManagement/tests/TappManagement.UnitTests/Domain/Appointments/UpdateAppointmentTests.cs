namespace TappManagement.UnitTests.Domain.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateAppointmentTests
{
    private readonly Faker _faker;

    public UpdateAppointmentTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_appointment()
    {
        // Arrange
        var fakeAppointment = new FakeAppointmentBuilder().Build();
        var updatedAppointment = new FakeAppointmentForUpdate().Generate();
        
        // Act
        fakeAppointment.Update(updatedAppointment);

        // Assert
        fakeAppointment.Size.Should().Be(updatedAppointment.Size);
        fakeAppointment.Location.Should().Be(updatedAppointment.Location);
        fakeAppointment.Description.Should().Be(updatedAppointment.Description);
        fakeAppointment.Reference.Should().Be(updatedAppointment.Reference);
        fakeAppointment.Payment.Should().Be(updatedAppointment.Payment);
        fakeAppointment.Approve.Should().Be(updatedAppointment.Approve);
        fakeAppointment.UserId.Should().Be(updatedAppointment.UserId);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeAppointment = new FakeAppointmentBuilder().Build();
        var updatedAppointment = new FakeAppointmentForUpdate().Generate();
        fakeAppointment.DomainEvents.Clear();
        
        // Act
        fakeAppointment.Update(updatedAppointment);

        // Assert
        fakeAppointment.DomainEvents.Count.Should().Be(1);
        fakeAppointment.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(AppointmentUpdated));
    }
}