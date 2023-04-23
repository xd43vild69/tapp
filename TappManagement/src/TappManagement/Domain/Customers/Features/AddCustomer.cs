namespace TappManagement.Domain.Customers.Features;

using TappManagement.Domain.Customers.Services;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Dtos;
using TappManagement.Domain.Customers.Models;
using TappManagement.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddCustomer
{
    public sealed class Command : IRequest<CustomerDto>
    {
        public readonly CustomerForCreationDto CustomerToAdd;

        public Command(CustomerForCreationDto customerToAdd)
        {
            CustomerToAdd = customerToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var customerToAdd = _mapper.Map<CustomerForCreation>(request.CustomerToAdd);
            var customer = Customer.Create(customerToAdd);

            await _customerRepository.Add(customer, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<CustomerDto>(customer);
        }
    }
}