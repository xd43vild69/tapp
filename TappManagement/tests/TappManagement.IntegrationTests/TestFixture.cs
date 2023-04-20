namespace TappManagement.IntegrationTests;

using TappManagement.Extensions.Services;
using TappManagement.Databases;
using TappManagement.Resources;
using TappManagement.SharedTestHelpers.Utilities;
using Configurations;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

[CollectionDefinition(nameof(TestFixture))]
public class TestFixtureCollection : ICollectionFixture<TestFixture> {}

public class TestFixture : IAsyncLifetime
{
    public static IServiceScopeFactory BaseScopeFactory;
    private readonly TestcontainerDatabase _dbContainer = DbSetup();
    private readonly RmqConfig _rmqContainer = RmqSetup();

    public async Task InitializeAsync()
    {
        var builder = WebApplication.CreateBuilder(new WebApplicationOptions
        {
            EnvironmentName = Consts.Testing.IntegrationTestingEnvName
        });

        await _dbContainer.StartAsync();
        builder.Configuration.GetSection(ConnectionStringOptions.SectionName)[ConnectionStringOptions.TappManagementKey] = $"{_dbContainer.ConnectionString}TrustServerCertificate=true;");
        await RunMigration(_dbContainer.ConnectionString);

        await _rmqContainer.Container.StartAsync();
        builder.Configuration.GetSection(RabbitMqOptions.SectionName)[RabbitMqOptions.HostKey] = "localhost";
        builder.Configuration.GetSection(RabbitMqOptions.SectionName)[RabbitMqOptions.VirtualHostKey] = "/";
        builder.Configuration.GetSection(RabbitMqOptions.SectionName)[RabbitMqOptions.UsernameKey] = "guest";
        builder.Configuration.GetSection(RabbitMqOptions.SectionName)[RabbitMqOptions.PasswordKey] = "guest";
        builder.Configuration.GetSection(RabbitMqOptions.SectionName)[RabbitMqOptions.PortKey] = _rmqContainer.Port.ToString();

        builder.ConfigureServices();
        var services = builder.Services;

        // add any mock services here
        services.ReplaceServiceWithSingletonMock<IHttpContextAccessor>();

        var provider = services.BuildServiceProvider();
        BaseScopeFactory = provider.GetService<IServiceScopeFactory>();
    }

    private static async Task RunMigration(string connectionString)
    {
        var options = new DbContextOptionsBuilder<TappDbContext>()
            .UseSqlServer(connectionString)
            .Options;
        var context = new TappDbContext(options, null, null, null);
        await context?.Database?.MigrateAsync();
    }

    private static TestcontainerDatabase DbSetup()
    {
        var isMacOs = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        var cpuArch = RuntimeInformation.ProcessArchitecture;
        var isRunningOnMacOsArm64 = isMacOs && cpuArch == Architecture.Arm64;
        
        var baseDb = new TestcontainersBuilder<MsSqlTestcontainer>()
            .WithDatabase(new MsSqlTestcontainerConfiguration()
            {
                Password = "#testingDockerPassword#",
            })
            .WithName($"IntegrationTesting_TappManagement_4742cd9c-1085-4c9b-85c0-820526f32381");
            
        if(isRunningOnMacOsArm64)
            baseDb.WithImage("mcr.microsoft.com/azure-sql-edge:latest");

        return baseDb.Build();
    }

    private class RmqConfig
    {
        public IContainer Container { get; set; }
        public int Port { get; set; }
    }

    private static RmqConfig RmqSetup()
    {
        var freePort = DockerUtilities.GetFreePort();
        return new RmqConfig
        {
            Container = new ContainerBuilder()
                .WithImage("masstransit/rabbitmq")
                .WithPortBinding(freePort, 5672)
                .WithName($"IntegrationTesting_RMQ_{Guid.NewGuid()}")
                .Build(),
            Port = freePort
        };
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _rmqContainer.Container.DisposeAsync();
    }
}

public static class ServiceCollectionServiceExtensions
{
    public static IServiceCollection ReplaceServiceWithSingletonMock<TService>(this IServiceCollection services)
        where TService : class
    {
        services.RemoveAll(typeof(TService));
        services.AddSingleton(_ => Mock.Of<TService>());
        return services;
    }
}
