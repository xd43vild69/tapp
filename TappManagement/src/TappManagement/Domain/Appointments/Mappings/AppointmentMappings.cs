namespace TappManagement.Domain.Appointments.Mappings;

using TappManagement.Domain.Appointments.Dtos;
using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.Models;
using Mapster;

public sealed class AppointmentMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Appointment, AppointmentDto>();
        config.NewConfig<AppointmentForCreationDto, Appointment>()
            .TwoWays();
        config.NewConfig<AppointmentForUpdateDto, Appointment>()
            .TwoWays();
        config.NewConfig<AppointmentForCreation, Appointment>()
            .TwoWays();
        config.NewConfig<AppointmentForUpdate, Appointment>()
            .TwoWays();
    }
}