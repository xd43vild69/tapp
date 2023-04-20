namespace TappManagement.Domain.Appointments.Features;

using TappManagement.Domain.Appointments.Dtos;
using TappManagement.Domain.Appointments.Services;
using TappManagement.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetAppointmentList
{
    public sealed class Query : IRequest<PagedList<AppointmentDto>>
    {
        public readonly AppointmentParametersDto QueryParameters;

        public Query(AppointmentParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<AppointmentDto>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IAppointmentRepository appointmentRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<AppointmentDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _appointmentRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<AppointmentDto>();

            return await PagedList<AppointmentDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}