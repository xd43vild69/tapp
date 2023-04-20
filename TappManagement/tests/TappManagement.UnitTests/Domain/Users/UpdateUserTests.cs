namespace TappManagement.UnitTests.Domain.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class UpdateUserTests
{
    private readonly Faker _faker;

    public UpdateUserTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_update_user()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        var updatedUser = new FakeUserForUpdate().Generate();
        
        // Act
        fakeUser.Update(updatedUser);

        // Assert
        fakeUser.Name.Should().Be(updatedUser.Name);
        fakeUser.Email.Should().Be(updatedUser.Email);
        fakeUser.Cellphone.Should().Be(updatedUser.Cellphone);
        fakeUser.IGUser.Should().Be(updatedUser.IGUser);
        fakeUser.AppointmentId.Should().Be(updatedUser.AppointmentId);
    }
    
    [Fact]
    public void queue_domain_event_on_update()
    {
        // Arrange
        var fakeUser = new FakeUserBuilder().Build();
        var updatedUser = new FakeUserForUpdate().Generate();
        fakeUser.DomainEvents.Clear();
        
        // Act
        fakeUser.Update(updatedUser);

        // Assert
        fakeUser.DomainEvents.Count.Should().Be(1);
        fakeUser.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserUpdated));
    }
}