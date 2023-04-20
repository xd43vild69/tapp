namespace TappManagement.SharedTestHelpers.Fakes.User;

using AutoBogus;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Dtos;

public sealed class FakeUserForUpdateDto : AutoFaker<UserForUpdateDto>
{
    public FakeUserForUpdateDto()
    {
    }
}