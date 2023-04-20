namespace TappManagement.UnitTests.Domain.Appointments;

using TappManagement.SharedTestHelpers.Fakes.Appointment;
using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateAppointmentTests
{
    private readonly Faker _faker;

    public CreateAppointmentTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_appointment()
    {
        // Arrange
        var appointmentToCreate = new FakeAppointmentForCreation().Generate();
        
        // Act
        var fakeAppointment = Appointment.Create(appointmentToCreate);

        // Assert
        fakeAppointment.Size.Should().Be(appointmentToCreate.Size);
        fakeAppointment.Location.Should().Be(appointmentToCreate.Location);
        fakeAppointment.Description.Should().Be(appointmentToCreate.Description);
        fakeAppointment.Reference.Should().Be(appointmentToCreate.Reference);
        fakeAppointment.Payment.Should().Be(appointmentToCreate.Payment);
        fakeAppointment.Approve.Should().Be(appointmentToCreate.Approve);
        fakeAppointment.UserId.Should().Be(appointmentToCreate.UserId);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var appointmentToCreate = new FakeAppointmentForCreation().Generate();
        
        // Act
        var fakeAppointment = Appointment.Create(appointmentToCreate);

        // Assert
        fakeAppointment.DomainEvents.Count.Should().Be(1);
        fakeAppointment.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(AppointmentCreated));
    }
}