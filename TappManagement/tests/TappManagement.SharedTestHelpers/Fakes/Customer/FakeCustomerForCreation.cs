namespace TappManagement.SharedTestHelpers.Fakes.Customer;

using AutoBogus;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.Models;

public sealed class FakeCustomerForCreation : AutoFaker<CustomerForCreation>
{
    public FakeCustomerForCreation()
    {
    }
}