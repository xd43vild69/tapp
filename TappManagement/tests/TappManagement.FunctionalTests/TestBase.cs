namespace TappManagement.FunctionalTests;

using TappManagement.Databases;
using TappManagement;
using AutoBogus;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;
 
[Collection(nameof(TestBase))]
public class TestBase : IDisposable
{
    private static IServiceScopeFactory _scopeFactory;
    protected static HttpClient FactoryClient  { get; private set; }

    public TestBase()
    {
        var factory = new TestingWebApplicationFactory();
        _scopeFactory = factory.Services.GetRequiredService<IServiceScopeFactory>();
        FactoryClient = factory.CreateClient(new WebApplicationFactoryClientOptions());

        AutoFaker.Configure(builder =>
        {
            // configure global autobogus settings here
            builder.WithDateTimeKind(DateTimeKind.Utc)
                .WithRecursiveDepth(3)
                .WithTreeDepth(1)
                .WithRepeatCount(1);
        });
    }
    
    public void Dispose()
    {
        FactoryClient.Dispose();
    }

    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetService<ISender>();
        return await mediator.Send(request);
    }

    public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<TappDbContext>();
        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetService<TappDbContext>();
        context.Add(entity);
        await context.SaveChangesAsync();
    }

    public static async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TappDbContext>();
        await action(scope.ServiceProvider);
    }

    public static async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        using var scope = _scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<TappDbContext>();
        return await action(scope.ServiceProvider);
    }

    public static Task ExecuteDbContextAsync(Func<TappDbContext, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>()));

    public static Task ExecuteDbContextAsync(Func<TappDbContext, ValueTask> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>()).AsTask());

    public static Task ExecuteDbContextAsync(Func<TappDbContext, IMediator, Task> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>(), sp.GetService<IMediator>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<TappDbContext, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>()));

    public static Task<T> ExecuteDbContextAsync<T>(Func<TappDbContext, ValueTask<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>()).AsTask());

    public static Task<T> ExecuteDbContextAsync<T>(Func<TappDbContext, IMediator, Task<T>> action)
        => ExecuteScopeAsync(sp => action(sp.GetService<TappDbContext>(), sp.GetService<IMediator>()));

    public static Task<int> InsertAsync<T>(params T[] entities) where T : class
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