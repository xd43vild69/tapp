namespace TappManagement.Domain.Admins.Services;

using TappManagement.Domain.Admins;
using TappManagement.Databases;
using TappManagement.Services;

public interface IAdminRepository : IGenericRepository<Admin>
{
}

public sealed class AdminRepository : GenericRepository<Admin>, IAdminRepository
{
    private readonly TappDbContext _dbContext;

    public AdminRepository(TappDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
