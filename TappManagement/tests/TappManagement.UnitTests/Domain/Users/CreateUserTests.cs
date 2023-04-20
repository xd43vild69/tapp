namespace TappManagement.UnitTests.Domain.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.DomainEvents;
using Bogus;
using FluentAssertions;
using FluentAssertions.Extensions;
using Xunit;

public class CreateUserTests
{
    private readonly Faker _faker;

    public CreateUserTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void can_create_valid_user()
    {
        // Arrange
        var userToCreate = new FakeUserForCreation().Generate();
        
        // Act
        var fakeUser = User.Create(userToCreate);

        // Assert
        fakeUser.Name.Should().Be(userToCreate.Name);
        fakeUser.Email.Should().Be(userToCreate.Email);
        fakeUser.Cellphone.Should().Be(userToCreate.Cellphone);
        fakeUser.IGUser.Should().Be(userToCreate.IGUser);
        fakeUser.AppointmentId.Should().Be(userToCreate.AppointmentId);
    }

    [Fact]
    public void queue_domain_event_on_create()
    {
        // Arrange
        var userToCreate = new FakeUserForCreation().Generate();
        
        // Act
        var fakeUser = User.Create(userToCreate);

        // Assert
        fakeUser.DomainEvents.Count.Should().Be(1);
        fakeUser.DomainEvents.FirstOrDefault().Should().BeOfType(typeof(UserCreated));
    }
}