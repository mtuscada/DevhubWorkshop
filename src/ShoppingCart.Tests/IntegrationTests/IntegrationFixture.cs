using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Networks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Testcontainers.MsSql;
using Xunit;

namespace ShoppingCart.Tests.IntegrationTests;

public class IntegrationFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer   _msSqlContainer;
    private readonly INetwork _network;
    private readonly Lazy<HttpClient> _client;

    public IntegrationFixture()
    {
        _network = new NetworkBuilder().Build();
        _msSqlContainer = new MsSqlBuilder().WithPrivileged(true)
            .Build();
        _client = new Lazy<HttpClient>(CreateClient);
    }

    public async Task InitializeAsync()
    {
        await _network.CreateAsync();
        await _msSqlContainer.StartAsync();
    }
    
    protected string ConnectionString => _msSqlContainer.GetConnectionString();
    protected HttpClient Client => _client.Value;

    public async Task DisposeAsync()
    {
        await _msSqlContainer.DisposeAsync();
        await _network.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var connectionString = _msSqlContainer.GetConnectionString();
        builder.UseSetting("ConnectionOptions:DbConnectionString", connectionString);
    }
}
