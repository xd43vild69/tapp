namespace TappManagement.IntegrationTests;

using System.Threading.Tasks;
using Databases;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using SharedKernel.Exceptions;
using static TestFixture;

public class TestingServiceScope 
{
    private readonly IServiceScope _scope;

    public TestingServiceScope()
    {
        _scope = BaseScopeFactory.CreateScope();
    }

    public TScopedService GetService<TScopedService>()
    {
        var service = _scope.ServiceProvider.GetService<TScopedService>();
        return service;
    }

    public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        var mediator = _scope.ServiceProvider.GetService<ISender>();
        return await mediator.Send(request);
    }

    public async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        var context = _scope.ServiceProvider.GetService<TappDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }

    public async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        var context = _scope.ServiceProvider.GetService<TappDbContext>();
        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        var dbContext = _scope.ServiceProvider.GetRequiredService<TappDbContext>();

        try
        {
            //await dbContext.BeginTransactionAsync();

            var result = await action(_scope.ServiceProvider);

            //await dbContext.CommitTransactionAsync();

            return result;
        }
        catch (Exception)
        {
            //dbContext.RollbackTransaction();
            throw;
        }
    }

    public Task<T> ExecuteDbContextAsync<T>(Func<TappDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>()));
    
    public Task<int> InsertAsync<T>(params T[] entities) where T : class
    {
        return ExecuteDbContextAsync(db =>
        {
            foreach (var entity in entities)
            {
                db.Set<T>().Add(entity);
            }
            return db.SaveChangesAsync();
        });
    }
}