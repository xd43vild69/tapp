namespace TappManagement.Domain.Admins.Features;

using TappManagement.Domain.Admins.Dtos;
using TappManagement.Domain.Admins.Services;
using TappManagement.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetAdminList
{
    public sealed class Query : IRequest<PagedList<AdminDto>>
    {
        public readonly AdminParametersDto QueryParameters;

        public Query(AdminParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<AdminDto>>
    {
        private readonly IAdminRepository _adminRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IAdminRepository adminRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _adminRepository = adminRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<AdminDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _adminRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<AdminDto>();

            return await PagedList<AdminDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}