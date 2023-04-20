namespace TappManagement.SharedTestHelpers.Fakes.Admin;

using AutoBogus;
using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.Dtos;

public sealed class FakeAdminForCreationDto : AutoFaker<AdminForCreationDto>
{
    public FakeAdminForCreationDto()
    {
    }
}