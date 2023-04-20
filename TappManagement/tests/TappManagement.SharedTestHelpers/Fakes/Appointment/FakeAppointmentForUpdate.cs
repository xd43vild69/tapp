namespace TappManagement.SharedTestHelpers.Fakes.Appointment;

using AutoBogus;
using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.Models;

public sealed class FakeAppointmentForUpdate : AutoFaker<AppointmentForUpdate>
{
    public FakeAppointmentForUpdate()
    {
    }
}