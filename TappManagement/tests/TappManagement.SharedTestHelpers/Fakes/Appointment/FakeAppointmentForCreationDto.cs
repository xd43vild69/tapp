namespace TappManagement.SharedTestHelpers.Fakes.Appointment;

using AutoBogus;
using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.Dtos;

public sealed class FakeAppointmentForCreationDto : AutoFaker<AppointmentForCreationDto>
{
    public FakeAppointmentForCreationDto()
    {
    }
}