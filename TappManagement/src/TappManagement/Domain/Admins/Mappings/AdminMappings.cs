namespace TappManagement.Domain.Admins.Mappings;

using TappManagement.Domain.Admins.Dtos;
using TappManagement.Domain.Admins;
using TappManagement.Domain.Admins.Models;
using Mapster;

public sealed class AdminMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Admin, AdminDto>();
        config.NewConfig<AdminForCreationDto, Admin>()
            .TwoWays();
        config.NewConfig<AdminForUpdateDto, Admin>()
            .TwoWays();
        config.NewConfig<AdminForCreation, Admin>()
            .TwoWays();
        config.NewConfig<AdminForUpdate, Admin>()
            .TwoWays();
    }
}