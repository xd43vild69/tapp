namespace TappManagement.Domain.Appointments.Features;

using TappManagement.Domain.Appointments.Services;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteAppointment
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _appointmentRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _appointmentRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}