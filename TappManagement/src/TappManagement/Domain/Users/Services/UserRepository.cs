namespace TappManagement.Domain.Users.Services;

using TappManagement.Domain.Users;
using TappManagement.Databases;
using TappManagement.Services;

public interface IUserRepository : IGenericRepository<User>
{
}

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly TappDbContext _dbContext;

    public UserRepository(TappDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
