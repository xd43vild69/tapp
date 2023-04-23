namespace TappManagement.FunctionalTests.FunctionalTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.FunctionalTests.TestUtilities;
using FluentAssertions;
using Xunit;
using System.Net;
using System.Threading.Tasks;

public class GetCustomerListTests : TestBase
{
    [Fact]
    public async Task get_customer_list_returns_success()
    {
        // Arrange
        

        // Act
        var result = await FactoryClient.GetRequestAsync(ApiRoutes.Customers.GetList);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}