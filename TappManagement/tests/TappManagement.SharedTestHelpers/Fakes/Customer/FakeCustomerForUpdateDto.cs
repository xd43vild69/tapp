namespace TappManagement.SharedTestHelpers.Fakes.Customer;

using AutoBogus;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Dtos;

public sealed class FakeCustomerForUpdateDto : AutoFaker<CustomerForUpdateDto>
{
    public FakeCustomerForUpdateDto()
    {
    }
}