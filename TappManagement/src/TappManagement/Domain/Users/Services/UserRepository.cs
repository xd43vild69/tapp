namespace TappManagement.Domain.Users.Services;

using TappManagement.Domain.Users;
using TappManagement.Databases;
using TappManagement.Services;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<bool> UserExists(string name, CancellationToken cancellationToken = default);
}

public sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly TappDbContext _dbContext;

    public UserRepository(TappDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> UserExists(string name, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Set<User>()
            .AnyAsync(e => e.Name == name, cancellationToken);
    }
}
        