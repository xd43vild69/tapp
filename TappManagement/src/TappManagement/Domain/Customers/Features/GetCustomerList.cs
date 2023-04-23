namespace TappManagement.Domain.Customers.Features;

using TappManagement.Domain.Customers.Dtos;
using TappManagement.Domain.Customers.Services;
using TappManagement.Wrappers;
using SharedKernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using MapsterMapper;
using Mapster;
using MediatR;
using Sieve.Models;
using Sieve.Services;

public static class GetCustomerList
{
    public sealed class Query : IRequest<PagedList<CustomerDto>>
    {
        public readonly CustomerParametersDto QueryParameters;

        public Query(CustomerParametersDto queryParameters)
        {
            QueryParameters = queryParameters;
        }
    }

    public sealed class Handler : IRequestHandler<Query, PagedList<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly SieveProcessor _sieveProcessor;
        private readonly IMapper _mapper;

        public Handler(ICustomerRepository customerRepository, IMapper mapper, SieveProcessor sieveProcessor)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<PagedList<CustomerDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var collection = _customerRepository.Query().AsNoTracking();

            var sieveModel = new SieveModel
            {
                Sorts = request.QueryParameters.SortOrder ?? "-CreatedOn",
                Filters = request.QueryParameters.Filters
            };

            var appliedCollection = _sieveProcessor.Apply(sieveModel, collection);
            var dtoCollection = appliedCollection
                .ProjectToType<CustomerDto>();

            return await PagedList<CustomerDto>.CreateAsync(dtoCollection,
                request.QueryParameters.PageNumber,
                request.QueryParameters.PageSize,
                cancellationToken);
        }
    }
}