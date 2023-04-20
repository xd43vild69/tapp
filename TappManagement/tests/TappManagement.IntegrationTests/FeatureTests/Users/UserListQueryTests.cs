namespace TappManagement.IntegrationTests.FeatureTests.Users;

using TappManagement.Domain.Users.Dtos;
using TappManagement.SharedTestHelpers.Fakes.User;
using SharedKernel.Exceptions;
using TappManagement.Domain.Users.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.ICollection<Appointment>;

public class UserListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_user_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeICollection<Appointment>One = new FakeICollection<Appointment>Builder().Build();
        var fakeICollection<Appointment>Two = new FakeICollection<Appointment>Builder().Build();
        await testingServiceScope.InsertAsync(fakeICollection<Appointment>One, fakeICollection<Appointment>Two);

        var fakeUserOne = new FakeUserBuilder()
            .WithAppointmentId(fakeICollection<Appointment>One.Id)
            .Build();
        var fakeUserTwo = new FakeUserBuilder()
            .WithAppointmentId(fakeICollection<Appointment>Two.Id)
            .Build();
        var queryParameters = new UserParametersDto();

        await testingServiceScope.InsertAsync(fakeUserOne, fakeUserTwo);

        // Act
        var query = new GetUserList.Query(queryParameters);
        var users = await testingServiceScope.SendAsync(query);

        // Assert
        users.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}