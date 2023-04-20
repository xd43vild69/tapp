namespace TappManagement.SharedTestHelpers.Fakes.User;

using AutoBogus;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Models;

public sealed class FakeUserForCreation : AutoFaker<UserForCreation>
{
    public FakeUserForCreation()
    {
    }
}