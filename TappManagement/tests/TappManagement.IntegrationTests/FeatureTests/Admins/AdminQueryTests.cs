namespace TappManagement.IntegrationTests.FeatureTests.Admins;

using TappManagement.SharedTestHelpers.Fakes.Admin;
using TappManagement.Domain.Admins.Features;
using SharedKernel.Exceptions;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class AdminQueryTests : TestBase
{
    [Fact]
    public async Task can_get_existing_admin_with_accurate_props()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne);

        var fakeAdminOne = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        await testingServiceScope.InsertAsync(fakeAdminOne);

        // Act
        var query = new GetAdmin.Query(fakeAdminOne.Id);
        var admin = await testingServiceScope.SendAsync(query);

        // Assert
        admin.Name.Should().Be(fakeAdminOne.Name);
        admin.Password.Should().Be(fakeAdminOne.Password);
        admin.IsSync.Should().Be(fakeAdminOne.IsSync);
        admin.UserId.Should().Be(fakeAdminOne.UserId);
    }

    [Fact]
    public async Task get_admin_throws_notfound_exception_when_record_does_not_exist()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var badId = Guid.NewGuid();

        // Act
        var query = new GetAdmin.Query(badId);
        Func<Task> act = () => testingServiceScope.SendAsync(query);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}