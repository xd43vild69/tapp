namespace TappManagement.UnitTests.Domain.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.Domain.Customers;
using TappManagement.Domain.Customers.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateCustomerTests
{
    private readonly Faker _faker;

    public UpdateCustomerTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_customer()
    {
        // Arrange
        var fakeCustomer = new FakeCustomerBuilder().Build();
        var updatedCustomer = new FakeCustomerForUpdate().Generate();
        
        // Act
        fakeCustomer.Update(updatedCustomer);

        // Assert
        fakeCustomer.Name.Should().Be(updatedCustomer.Name);
        fakeCustomer.Password.Should().Be(updatedCustomer.Password);
        fakeCustomer.IsSync.Should().Be(updatedCustomer.IsSync);
        fakeCustomer.Cellphone.Should().Be(updatedCustomer.Cellphone);
        fakeCustomer.IGUser.Should().Be(updatedCustomer.IGUser);
        fakeCustomer.AppointmentId.Should().Be(updatedCustomer.AppointmentId);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeCustomer = new FakeCustomerBuilder().Build();
        var updatedCustomer = new FakeCustomerForUpdate().Generate();
        fakeCustomer.DomainEvents.Clear();
        
        // Act
        fakeCustomer.Update(updatedCustomer);

        // Assert
        fakeCustomer.DomainEvents.Count.Should().Be(1);
        fakeCustomer.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(CustomerUpdated));
    }
}