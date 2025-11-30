using DbUp.Engine.Output;

namespace ShoppingCart.Migration;

public interface IDbUpLogger : IUpgradeLog;

public class DbUpLogger : IDbUpLogger
{
    private readonly ILogger<DbUpLogger> _logger;

    /// <summary>
    ///     Initializes the <see cref="DbUpLogger" /> class.
    /// </summary>
    public DbUpLogger(ILogger<DbUpLogger> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    ///     Formats and writes an error log message.
    /// </summary>
    /// <param name="format">
    ///     Format string of the log message in message template format. Example:
    ///     <c>"User {User} logged in from {Address}"</c>
    /// </param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    public void LogError(string format, params object[] args)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogError(format, args);
    }

    public void LogError(Exception ex, string format, params object[] args)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogError(format, args);
    }

    public void LogTrace(string format, params object[] args)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogTrace(format, args);
    }

    public void LogDebug(string format, params object[] args)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogDebug(format, args);
    }

    /// <summary>
    ///     Formats and writes an info log message.
    /// </summary>
    /// <param name="format">
    ///     Format string of the log message in message template format. Example:
    ///     <c>"User {User} logged in from {Address}"</c>
    /// </param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    public void LogInformation(string format, params object[] args)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogInformation(format, args);
    }

    /// <summary>
    ///     Formats and writes an warning log message.
    /// </summary>
    /// <param name="format">
    ///     Format string of the log message in message template format. Example:
    ///     <c>"User {User} logged in from {Address}"</c>
    /// </param>
    /// <param name="args">An object array that contains zero or more objects to format.</param>
    public void LogWarning(string format, params object[] args)
    {
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        _logger.LogWarning(format, args);
    }
}