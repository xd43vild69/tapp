namespace TappManagement.SharedTestHelpers.Fakes.Admin;

using AutoBogus;
using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.Models;

public sealed class FakeAdminForCreation : AutoFaker<AdminForCreation>
{
    public FakeAdminForCreation()
    {
    }
}