namespace TappManagement.Domain.Customers.Features;

using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Dtos;
using TappManagement.Domain.Customers.Services;
using TappManagement.Services;
using TappManagement.Domain.Customers.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateCustomer
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly CustomerForUpdateDto UpdatedCustomerData;

        public Command(Guid id, CustomerForUpdateDto updatedCustomerData)
        {
            Id = id;
            UpdatedCustomerData = updatedCustomerData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var customerToUpdate = await _customerRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var customerToAdd = _mapper.Map<CustomerForUpdate>(request.UpdatedCustomerData);
            customerToUpdate.Update(customerToAdd);

            _customerRepository.Update(customerToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}