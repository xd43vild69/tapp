namespace TappManagement.FunctionalTests.FunctionalTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class UpdateCustomerRecordTests : TestBase
{
    [Fact]
    public async Task put_customer_returns_nocontent_when_entity_exists()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomer = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Build();
        var updatedCustomerDto = new FakeCustomerForUpdateDto()
            .RuleFor(c => c.AppointmentId, _ => fakeICollection<Appointment>One.Id)
            .Generate();
        await InsertAsync(fakeCustomer);

        // Act
        var route = ApiRoutes.Customers.Put(fakeCustomer.Id);
        var result = await FactoryClient.PutJsonRequestAsync(route, updatedCustomerDto);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}