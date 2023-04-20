namespace TappManagement.IntegrationTests.FeatureTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.Domain.Admins.Dtos;
using SharedKernel.Exceptions;
using TappManagement.Domain.Admins.Features;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class UpdateAdminCommandTests : TestBase
{
    [Fact]
    public async Task can_update_existing_admin_in_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAdminOne = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        var updatedAdminDto = new FakeAdminForUpdateDto()
            .RuleFor(a => a.UserId, _ => fakeUserOne.Id)
            .Generate();
        await testingServiceScope.InsertAsync(fakeAdminOne);

        var admin = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins
            .FirstOrDefaultAsync(a => a.Id == fakeAdminOne.Id));
        var id = admin.Id;

        // Act
        var command = new UpdateAdmin.Command(id, updatedAdminDto);
        await testingServiceScope.SendAsync(command);
        var updatedAdmin = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins.FirstOrDefaultAsync(a => a.Id == id));

        // Assert
        updatedAdmin.Name.Should().Be(updatedAdminDto.Name);
        updatedAdmin.Password.Should().Be(updatedAdminDto.Password);
        updatedAdmin.IsSync.Should().Be(updatedAdminDto.IsSync);
        updatedAdmin.UserId.Should().Be(updatedAdminDto.UserId);
    }
}