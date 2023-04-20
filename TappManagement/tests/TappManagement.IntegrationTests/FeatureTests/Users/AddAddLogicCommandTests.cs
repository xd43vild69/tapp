namespace TappManagement.IntegrationTests.FeatureTests.Users;

using TappManagement.SharedTestHelpers.Fakes.UserExistLogic;
using Domain;
using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Threading.Tasks;
using TappManagement.Domain.Users.Features;
using SharedKernel.Exceptions;

public class AddAddLogicCommandTests : TestBase
{
    [Fact]
    public async Task can_add_new_addlogic_to_db()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeAddLogicOne = new FakeAddLogicForCreationDto().Generate();

        // Act
        var command = new UserExistLogic.Command(fakeAddLogicOne);
        var addLogicReturned = await testingServiceScope.SendAsync(command);
        var addLogicCreated = await testingServiceScope.ExecuteDbContextAsync(db => db.Users
            .FirstOrDefaultAsync(a => a.Id == addLogicReturned.Id));

        // Assert

    }
}