namespace TappManagement.FunctionalTests.FunctionalTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetCustomerTests : TestBase
{
    [Fact]
    public async Task get_customer_returns_success_when_entity_exists()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomer = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Build();
        await InsertAsync(fakeCustomer);

        // Act
        var route = ApiRoutes.Customers.GetRecord(fakeCustomer.Id);
        var result = await FactoryClient.GetRequestAsync(route);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}