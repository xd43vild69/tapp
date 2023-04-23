namespace TappManagement.Domain.Customers.Services;

using TappManagement.Domain.Customers;
using TappManagement.Databases;
using TappManagement.Services;

public interface ICustomerRepository : IGenericRepository<Customer>
{
}

public sealed class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
{
    private readonly TappDbContext _dbContext;

    public CustomerRepository(TappDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
