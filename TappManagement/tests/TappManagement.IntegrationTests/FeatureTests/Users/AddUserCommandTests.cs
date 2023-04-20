namespace TappManagement.IntegrationTests.FeatureTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.Domain.Users.Features;
using SharedKernel.Exceptions;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class AddUserCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_user_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = FakeICollection<Appointment>.Generate(new FakeICollection<Appointment>ForCreationDto().Generate());
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeUserOne = new FakeUserForCreationDto()
            .RuleFor(u => u.AppointmentId, _ => fakeICollection<Appointment>One.Id)
            .Generate();

        // Act
        var command = new AddUser.Command(fakeUserOne);
        var userReturned = await testingServiceScope.SendAsync(command);
        var userCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Users
            .FirstOrDefaultAsync(u => u.Id == userReturned.Id));

        // Assert
        userReturned.FirstName.Should().Be(fakeUserOne.FirstName);
        userReturned.LastName.Should().Be(fakeUserOne.LastName);
        userReturned.Username.Should().Be(fakeUserOne.Username);
        userReturned.Identifier.Should().Be(fakeUserOne.Identifier);
        userReturned.Email.Should().Be(fakeUserOne.Email);

        userCreated?.FirstName.Should().Be(fakeUserOne.FirstName);
        userCreated?.LastName.Should().Be(fakeUserOne.LastName);
        userCreated?.Username.Should().Be(fakeUserOne.Username);
        userCreated?.Identifier.Should().Be(fakeUserOne.Identifier);
        userCreated?.Email.Value.Should().Be(fakeUserOne.Email);
    }
}