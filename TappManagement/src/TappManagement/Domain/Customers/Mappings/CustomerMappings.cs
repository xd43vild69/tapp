namespace TappManagement.Domain.Customers.Mappings;

using TappManagement.Domain.Customers.Dtos;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Models;
using Mapster;

public sealed class CustomerMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Customer, CustomerDto>();
        config.NewConfig<CustomerForCreationDto, Customer>()
            .TwoWays();
        config.NewConfig<CustomerForUpdateDto, Customer>()
            .TwoWays();
        config.NewConfig<CustomerForCreation, Customer>()
            .TwoWays();
        config.NewConfig<CustomerForUpdate, Customer>()
            .TwoWays();
    }
}