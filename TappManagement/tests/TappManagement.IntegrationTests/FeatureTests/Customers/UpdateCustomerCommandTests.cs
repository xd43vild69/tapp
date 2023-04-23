namespace TappManagement.IntegrationTests.FeatureTests.Customers;

using TappManagement.SharedTestHelpers.Fakes.Customer;
using TappManagement.Domain.Customers.Dtos;
using SharedKernel.Exceptions;
using TappManagement.Domain.Customers.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class UpdateCustomerCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_customer_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeCustomerOne = new FakeCustomerBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id)
            .Build();
        var updatedCustomerDto = new FakeCustomerForUpdateDto()
            .RuleFor(c => c.AppointmentId, _ => fakeICollection<Appointment>One.Id)
            .Generate();
        await testingServiceScope.InsertAsync(fakeCustomerOne);

        var customer = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers
            .FirstOrDefaultAsync(c => c.Id == fakeCustomerOne.Id));
        var id = customer.Id;

        // Act
        var command = new UpdateCustomer.Command(id, updatedCustomerDto);
        await testingServiceScope.SendAsync(command);
        var updatedCustomer = await testingServiceScope.ExecuteDbContextAsync(db => db.Customers.FirstOrDefaultAsync(c => c.Id == id));

        // Assert
        updatedCustomer.Name.Should().Be(updatedCustomerDto.Name);
        updatedCustomer.Password.Should().Be(updatedCustomerDto.Password);
        updatedCustomer.IsSync.Should().Be(updatedCustomerDto.IsSync);
        updatedCustomer.Cellphone.Should().Be(updatedCustomerDto.Cellphone);
        updatedCustomer.IGUser.Should().Be(updatedCustomerDto.IGUser);
        updatedCustomer.AppointmentId.Should().Be(updatedCustomerDto.AppointmentId);
    }
}