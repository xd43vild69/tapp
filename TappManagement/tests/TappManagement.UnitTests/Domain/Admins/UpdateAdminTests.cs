namespace TappManagement.UnitTests.Domain.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateAdminTests
{
    private readonly Faker _faker;

    public UpdateAdminTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_admin()
    {
        // Arrange
        var fakeAdmin = new FakeAdminBuilder().Build();
        var updatedAdmin = new FakeAdminForUpdate().Generate();
        
        // Act
        fakeAdmin.Update(updatedAdmin);

        // Assert
        fakeAdmin.Name.Should().Be(updatedAdmin.Name);
        fakeAdmin.Password.Should().Be(updatedAdmin.Password);
        fakeAdmin.IsSync.Should().Be(updatedAdmin.IsSync);
        fakeAdmin.UserId.Should().Be(updatedAdmin.UserId);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeAdmin = new FakeAdminBuilder().Build();
        var updatedAdmin = new FakeAdminForUpdate().Generate();
        fakeAdmin.DomainEvents.Clear();
        
        // Act
        fakeAdmin.Update(updatedAdmin);

        // Assert
        fakeAdmin.DomainEvents.Count.Should().Be(1);
        fakeAdmin.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(AdminUpdated));
    }
}