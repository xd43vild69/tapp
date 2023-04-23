namespace TappManagement.IntegrationTests.FeatureTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.Domain.Customers.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class DeleteCustomerCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_customer_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomerOne = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeCustomerOne);
        var customer = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers
            .FirstOrDefaultAsync(c => c.Id == fakeCustomerOne.Id));

        // Act
        var command = new DeleteCustomer.Command(customer.Id);
        await testingServiceScope.SendAsync(command);
        var customerResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers.CountAsync(c => c.Id == customer.Id));

        // Assert
        customerResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_customer_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteCustomer.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_customer_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomerOne = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeCustomerOne);
        var customer = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers
            .FirstOrDefaultAsync(c => c.Id == fakeCustomerOne.Id));

        // Act
        var command = new DeleteCustomer.Command(customer.Id);
        await testingServiceScope.SendAsync(command);
        var deletedCustomer = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == customer.Id));

        // Assert
        deletedCustomer?.IsDeleted.Should().BeTrue();
    }
}