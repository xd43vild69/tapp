namespace TappManagement.Domain.Appointments.Features;

using TappManagement.Domain.Appointments;
using TappManagement.Domain.Appointments.Dtos;
using TappManagement.Domain.Appointments.Services;
using TappManagement.Services;
using TappManagement.Domain.Appointments.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateAppointment
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly AppointmentForUpdateDto UpdatedAppointmentData;

        public Command(Guid id, AppointmentForUpdateDto updatedAppointmentData)
        {
            Id = id;
            UpdatedAppointmentData = updatedAppointmentData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var appointmentToUpdate = await _appointmentRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var appointmentToAdd = _mapper.Map<AppointmentForUpdate>(request.UpdatedAppointmentData);
            appointmentToUpdate.Update(appointmentToAdd);

            _appointmentRepository.Update(appointmentToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}