namespace TappManagement.IntegrationTests.FeatureTests.Users;

using TappManagement.SharedTestHelpers.Fakes.User;
using TappManagement.Domain.Users.Dtos;
using SharedKernel.Exceptions;
using TappManagement.Domain.Users.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class UpdateUserCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_user_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One);

        var fakeUserOne = new FakeUserBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Build();
        var updatedUserDto = new FakeUserForUpdateDto()
            .WithAppointmentId(fakeICollection<Appointment>One.Id).Generate();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var user = await testingServiceScope.ExecuteDbContextAsync(db => db.Users
            .FirstOrDefaultAsync(u => u.Id == fakeUserOne.Id));
        var id = user.Id;

        // Act
        var command = new UpdateUser.Command(id, updatedUserDto);
        await testingServiceScope.SendAsync(command);
        var updatedUser = await testingServiceScope.ExecuteDbContextAsync(db => db.Users.FirstOrDefaultAsync(u => u.Id == id));

        // Assert
        updatedUser?.FirstName.Should().Be(updatedUserDto.FirstName);
        updatedUser?.LastName.Should().Be(updatedUserDto.LastName);
        updatedUser?.Username.Should().Be(updatedUserDto.Username);
        updatedUser?.Identifier.Should().Be(updatedUserDto.Identifier);
        updatedUser?.Email.Value.Should().Be(updatedUserDto.Email);
    }
}