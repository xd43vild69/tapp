namespace TappManagement.SharedTestHelpers.Fakes.Customer;

using AutoBogus;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Dtos;

public sealed class FakeCustomerForCreationDto : AutoFaker<CustomerForCreationDto>
{
    public FakeCustomerForCreationDto()
    {
    }
}