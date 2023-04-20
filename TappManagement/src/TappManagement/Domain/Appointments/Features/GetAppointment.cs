namespace TappManagement.Domain.Appointments.Features;

using TappManagement.Domain.Appointments.Dtos;
using TappManagement.Domain.Appointments.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetAppointment
{
    public sealed class Query : IRequest<AppointmentDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, AppointmentDto>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public Handler(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<AppointmentDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _appointmentRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<AppointmentDto>(result);
        }
    }
}