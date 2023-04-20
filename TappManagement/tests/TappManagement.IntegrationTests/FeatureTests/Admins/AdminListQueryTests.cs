namespace TappManagement.IntegrationTests.FeatureTests.Admins;

using TappManagement.Domain.Admins.Dtos;
using TappManagement.SharedTestHelpers.Fakes.Admin;
using SharedKernel.Exceptions;
using TappManagement.Domain.Admins.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class AdminListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_admin_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        var fakeUserTwo = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne, fakeUserTwo);

        var fakeAdminOne = new FakeAdminBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        var fakeAdminTwo = new FakeAdminBuilder()
            .WithUserId(fakeUserTwo.Id)
            .Build();
        var queryParameters = new AdminParametersDto();

        await testingServiceScope.InsertAsync(fakeAdminOne, fakeAdminTwo);

        // Act
        var query = new GetAdminList.Query(queryParameters);
        var admins = await testingServiceScope.SendAsync(query);

        // Assert
        admins.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}