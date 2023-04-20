namespace TappManagement.UnitTests.Domain.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateAdminTests
{
    private readonly Faker _faker;

    public CreateAdminTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_admin()
    {
        // Arrange
        var adminToCreate = new FakeAdminForCreation().Generate();
        
        // Act
        var fakeAdmin = Admin.Create(adminToCreate);

        // Assert
        fakeAdmin.Name.Should().Be(adminToCreate.Name);
        fakeAdmin.Password.Should().Be(adminToCreate.Password);
        fakeAdmin.IsSync.Should().Be(adminToCreate.IsSync);
        fakeAdmin.UserId.Should().Be(adminToCreate.UserId);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var adminToCreate = new FakeAdminForCreation().Generate();
        
        // Act
        var fakeAdmin = Admin.Create(adminToCreate);

        // Assert
        fakeAdmin.DomainEvents.Count.Should().Be(1);
        fakeAdmin.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(AdminCreated));
    }
}