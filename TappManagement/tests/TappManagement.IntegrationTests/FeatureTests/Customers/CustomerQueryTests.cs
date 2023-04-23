namespace TappManagement.IntegrationTests.FeatureTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.Domain.Customers.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class CustomerQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_customer_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomerOne = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeCustomerOne);

        // Act
        var query = new GetCustomer.Query(fakeCustomerOne.Id);
        var customer = await testingServiceScope.SendAsync(query);

        // Assert
        customer.Name.Should().Be(fakeCustomerOne.Name);
        customer.Password.Should().Be(fakeCustomerOne.Password);
        customer.IsSync.Should().Be(fakeCustomerOne.IsSync);
        customer.Cellphone.Should().Be(fakeCustomerOne.Cellphone);
        customer.IGUser.Should().Be(fakeCustomerOne.IGUser);
        customer.AppointmentId.Should().Be(fakeCustomerOne.AppointmentId);
    }

    [Fact]
    public async Task get_customer_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetCustomer.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}