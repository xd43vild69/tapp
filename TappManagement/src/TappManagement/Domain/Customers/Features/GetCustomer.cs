namespace TappManagement.Domain.Customers.Features;

using TappManagement.Domain.Customers.Dtos;
using TappManagement.Domain.Customers.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetCustomer
{
    public sealed class Query : IRequest<CustomerDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public Handler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _customerRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<CustomerDto>(result);
        }
    }
}