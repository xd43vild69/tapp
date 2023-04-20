namespace TappManagement.IntegrationTests.FeatureTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.Domain.Admins.Features;
using SharedKernel.Exceptions;
using TappManagement.SharedTestHelpers.Fakes.User;

public class AddAdminCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_admin_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAdminOne = new FakeAdminForCreationDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id).Generate();

        // Act
        var command = new AddAdmin.Command(fakeAdminOne);
        var adminReturned = await testingServiceScope.SendAsync(command);
        var adminCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins
            .FirstOrDefaultAsync(a => a.Id == adminReturned.Id));

        // Assert
        adminReturned.Name.Should().Be(fakeAdminOne.Name);
        adminReturned.Password.Should().Be(fakeAdminOne.Password);
        adminReturned.IsSync.Should().Be(fakeAdminOne.IsSync);
        adminReturned.UserId.Should().Be(fakeAdminOne.UserId);

        adminCreated.Name.Should().Be(fakeAdminOne.Name);
        adminCreated.Password.Should().Be(fakeAdminOne.Password);
        adminCreated.IsSync.Should().Be(fakeAdminOne.IsSync);
        adminCreated.UserId.Should().Be(fakeAdminOne.UserId);
    }
}