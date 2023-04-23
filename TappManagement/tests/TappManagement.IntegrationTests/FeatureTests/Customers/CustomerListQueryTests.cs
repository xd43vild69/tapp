namespace TappManagement.IntegrationTests.FeatureTests.Customers;

using TappManagement.Domain.Customers.Dtos;
using TappManagement.SharedTestHelpers.Fakes.Customer;
using SharedKernel.Exceptions;
using TappManagement.Domain.Customers.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class CustomerListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_customer_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        var fakeICollection<Appointment>Two = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One, fakeICollection<Appointment>Two);

        var fakeCustomerOne = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id)
            .Build();
        var fakeCustomerTwo = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>Two.Id)
            .Build();
        var queryParameters = new CustomerParametersDto();

        await testingServiceScope.InsertAsync(fakeCustomerOne, fakeCustomerTwo);

        // Act
        var query = new GetCustomerList.Query(queryParameters);
        var customers = await testingServiceScope.SendAsync(query);

        // Assert
        customers.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}