namespace TappManagement.Domain.Appointments.Services;

using TappManagement.Domain.Appointments;
using TappManagement.Databases;
using TappManagement.Services;

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
}

public sealed class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    private readonly TappDbContext _dbContext;

    public AppointmentRepository(TappDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
