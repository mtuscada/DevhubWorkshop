using DbUp;
using Microsoft.Extensions.Options;
using ShoppingCart.DataSourcing;
using ShoppingCart.Db;

namespace ShoppingCart.Migration;

public class DbMigration : IStartupFilter
{
    private readonly ILogger<DbMigration>               _logger;
    private readonly IOptions<ConnectionOptions> _options;
    private readonly IDbUpLogger                        _dbLogger;

    public DbMigration(ILogger<DbMigration> logger, IOptions<ConnectionOptions> options, IDbUpLogger dbLogger)
    {
        _logger     = logger;
        _options    = options;
        _dbLogger = dbLogger;
    }
    
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    {
        var connectionString = _options.Value.DbConnectionString;
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            _logger.LogError("Connection string is missing, db migration will be skipped");
            throw new Exception("Connection string is missing, db migration will be skipped");
        }

        EnsureDatabase.For.SqlDatabase(connectionString,_dbLogger);
        
        var upgrader = DeployChanges.To.SqlDatabase(connectionString)
                                    .WithTransaction()
                                    .WithScriptsEmbeddedInAssembly(typeof(MigrationModule).Assembly)
                                    .LogTo(_dbLogger)
                                    .Build();

        if (!upgrader.IsUpgradeRequired())
        {
            _logger.LogInformation("VWF Scenario database update is not required, skipping migration");
            return next;
        }

        var result = upgrader.PerformUpgrade();
        if (!result.Successful)
        {
            _logger.LogError(result.Error,
                             "VWF Scenario database migration failed with message {Message}",
                             result.Error.Message);
            throw new Exception("VWF Scenario database migration failed");
        }

        _logger.LogInformation("VWF Scenario database migrated successfully");
        return next;
    }
}
