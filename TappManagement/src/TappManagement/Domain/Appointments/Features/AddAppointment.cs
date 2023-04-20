namespace TappManagement.Domain.Appointments.Features;

using TappManagement.Domain.Appointments.Services;
using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.Dtos;
using TappManagement.Domain.Appointments.Models;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddAppointment
{
    public sealed class Command : IRequest<AppointmentDto>
    {
        public readonly AppointmentForCreationDto AppointmentToAdd;

        public Command(AppointmentForCreationDto appointmentToAdd)
        {
            AppointmentToAdd = appointmentToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, AppointmentDto>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppointmentDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var appointmentToAdd = _mapper.Map<AppointmentForCreation>(request.AppointmentToAdd);
            var appointment = Appointment.Create(appointmentToAdd);

            await _appointmentRepository.Add(appointment, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<AppointmentDto>(appointment);
        }
    }
}