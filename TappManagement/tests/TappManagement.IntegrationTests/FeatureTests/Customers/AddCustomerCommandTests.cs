namespace TappManagement.IntegrationTests.FeatureTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.Domain.Customers.Features;
using SharedKernel.Exceptions;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class AddCustomerCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_customer_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomerOne = new FakeCustomerForCreationDto()
            .RuleFor(c => c.AppointmentId, _ => fakeICollection<Appointment>One.Id).Generate();

        // Act
        var command = new AddCustomer.Command(fakeCustomerOne);
        var customerReturned = await testingServiceScope.SendAsync(command);
        var customerCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers
            .FirstOrDefaultAsync(c => c.Id == customerReturned.Id));

        // Assert
        customerReturned.Name.Should().Be(fakeCustomerOne.Name);
        customerReturned.Password.Should().Be(fakeCustomerOne.Password);
        customerReturned.IsSync.Should().Be(fakeCustomerOne.IsSync);
        customerReturned.Cellphone.Should().Be(fakeCustomerOne.Cellphone);
        customerReturned.IGUser.Should().Be(fakeCustomerOne.IGUser);
        customerReturned.AppointmentId.Should().Be(fakeCustomerOne.AppointmentId);

        customerCreated.Name.Should().Be(fakeCustomerOne.Name);
        customerCreated.Password.Should().Be(fakeCustomerOne.Password);
        customerCreated.IsSync.Should().Be(fakeCustomerOne.IsSync);
        customerCreated.Cellphone.Should().Be(fakeCustomerOne.Cellphone);
        customerCreated.IGUser.Should().Be(fakeCustomerOne.IGUser);
        customerCreated.AppointmentId.Should().Be(fakeCustomerOne.AppointmentId);
    }
}