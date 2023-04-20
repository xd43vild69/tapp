namespace TappManagement.Domain.Users.Features;

using TappManagement.Domain.Users.Dtos;
using TappManagement.Domain.Users.Services;
using TappManagement.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetUserList
{
    public sealed class Query : IRequest<PagedList<UserDto>>
    {
        public readonly UserParametersDto QueryParameters;

        public Query(UserParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(IUserRepository userRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _userRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<UserDto>();

            return await PagedList<UserDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}