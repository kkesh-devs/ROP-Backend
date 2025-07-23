using Serilog;

namespace KKESH_ROP.Configuration;

public static class LoggingConfig
{
    public static void AddLoggingConfiguration(this ConfigureHostBuilder host)
    {
        const string logPath = @"C:\Grafana\Logs\ROP\log-.txt";

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File(
                path: logPath,
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();

        host.UseSerilog();
    }
}