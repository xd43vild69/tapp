namespace TappManagement.IntegrationTests.FeatureTests.Appointments;

using TappManagement.Domain.Appointments.Dtos;
using TappManagement.SharedTestHelpers.Fakes.Appointment;
using SharedKernel.Exceptions;
using TappManagement.Domain.Appointments.Features;
using FluentAssertions;
using Domain;
using Xunit;
using System.Threading.Tasks;
using TappManagement.SharedTestHelpers.Fakes.User;

public class AppointmentListQueryTests : TestBase
{
    
    [Fact]
    public async Task can_get_appointment_list()
    {
        // Arrange
        var testingServiceScope = new TestingServiceScope();
        var fakeUserOne = new FakeUserBuilder().Build();
        var fakeUserTwo = new FakeUserBuilder().Build();
        await testingServiceScope.InsertAsync(fakeUserOne, fakeUserTwo);

        var fakeAppointmentOne = new FakeAppointmentBuilder()
            .WithUserId(fakeUserOne.Id)
            .Build();
        var fakeAppointmentTwo = new FakeAppointmentBuilder()
            .WithUserId(fakeUserTwo.Id)
            .Build();
        var queryParameters = new AppointmentParametersDto();

        await testingServiceScope.InsertAsync(fakeAppointmentOne, fakeAppointmentTwo);

        // Act
        var query = new GetAppointmentList.Query(queryParameters);
        var appointments = await testingServiceScope.SendAsync(query);

        // Assert
        appointments.Count.Should().BeGreaterThanOrEqualTo(2);
    }
}