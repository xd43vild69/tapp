namespace TappManagement.FunctionalTests.FunctionalTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.FunctionalTests.TestUtilities;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class CreateCustomerTests : TestBase
{
    [Fact]
    public async Task create_customer_returns_created_using_valid_dto()
    {
        // Arrange
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomer = new FakeCustomerForCreationDto()
            .RuleFor(c => c.AppointmentId, _ => fakeICollection<Appointment>One.Id).Generate();

        // Act
        var route = ApiRoutes.Customers.Create;
        var result = await FactoryClient.PostJsonRequestAsync(route, fakeCustomer);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}