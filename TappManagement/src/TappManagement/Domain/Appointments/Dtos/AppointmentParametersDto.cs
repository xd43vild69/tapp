namespace TappManagement.Domain.Appointments.Dtos;

using SharedKernel.Dtos;

public sealed class AppointmentParametersDto : BasePaginationParameters
{
    public string Filters { get; set; }
    public string SortOrder { get; set; }
}
