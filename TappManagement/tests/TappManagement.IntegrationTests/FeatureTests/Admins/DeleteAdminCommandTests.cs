namespace TappManagement.IntegrationTests.FeatureTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.Domain.Admins.Features;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Domain;
using SharedKernel.Exceptions;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class DeleteAdminCommandTests : TestBase
{
    [Fact]
    public async Task can_delete_admin_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAdminOne = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeAdminOne);
        var admin = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins
            .FirstOrDefaultAsync(a => a.Id == fakeAdminOne.Id));

        // Act
        var command = new DeleteAdmin.Command(admin.Id);
        await testingServiceScope.SendAsync(command);
        var adminResponse = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins.CountAsync(a => a.Id == admin.Id));

        // Assert
        adminResponse.Should().Be(0);
    }

    [Fact]
    public async Task delete_admin_throws_notfoundexception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var command = new DeleteAdmin.Command(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(command);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task can_softdelete_admin_from_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAdminOne = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeAdminOne);
        var admin = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins
            .FirstOrDefaultAsync(a => a.Id == fakeAdminOne.Id));

        // Act
        var command = new DeleteAdmin.Command(admin.Id);
        await testingServiceScope.SendAsync(command);
        var deletedAdmin = await testingServiceScope.ExecuteDbContextAsync(db => db.Admins
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == admin.Id));

        // Assert
        deletedAdmin?.IsDeleted.Should().BeTrue();
    }
}