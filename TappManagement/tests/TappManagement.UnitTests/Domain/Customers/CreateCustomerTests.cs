namespace TappManagement.UnitTests.Domain.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateCustomerTests
{
    private readonly Faker _faker;

    public CreateCustomerTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_customer()
    {
        // Arrange
        var customerToCreate = new FakeCustomerForCreation().Generate();
        
        // Act
        var fakeCustomer = Customer.Create(customerToCreate);

        // Assert
        fakeCustomer.Name.Should().Be(customerToCreate.Name);
        fakeCustomer.Password.Should().Be(customerToCreate.Password);
        fakeCustomer.IsSync.Should().Be(customerToCreate.IsSync);
        fakeCustomer.Cellphone.Should().Be(customerToCreate.Cellphone);
        fakeCustomer.IGUser.Should().Be(customerToCreate.IGUser);
        fakeCustomer.AppointmentId.Should().Be(customerToCreate.AppointmentId);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var customerToCreate = new FakeCustomerForCreation().Generate();
        
        // Act
        var fakeCustomer = Customer.Create(customerToCreate);

        // Assert
        fakeCustomer.DomainEvents.Count.Should().Be(1);
        fakeCustomer.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(CustomerCreated));
    }
}