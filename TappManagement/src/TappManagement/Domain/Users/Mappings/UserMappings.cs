namespace TappManagement.Domain.Users.Mappings;

using TappManagement.Domain.Users.Dtos;
using TappManagement.Domain.Users;
using TappManagement.Domain.Users.Models;
using Mapster;

public sealed class UserMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>();
        config.NewConfig<UserForCreationDto, User>()
            .TwoWays();
        config.NewConfig<UserForUpdateDto, User>()
            .TwoWays();
        config.NewConfig<UserForCreation, User>()
            .TwoWays();
        config.NewConfig<UserForUpdate, User>()
            .TwoWays();
    }
}