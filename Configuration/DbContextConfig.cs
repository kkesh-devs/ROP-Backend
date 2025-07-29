using KKESH_ROP.Data;
using MongoDB.Driver;

namespace KKESH_ROP.Configuration;

public static class DbContextConfig
{
    public static void AddDbContextServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDB");
        var databaseName = configuration.GetSection("MongoSettings:SecondaryDatabase").Value ?? "KKESH-ROP";

        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);

        // Register MongoDB services
        services.AddSingleton<IMongoClient>(mongoClient);
        services.AddSingleton<IMongoDatabase>(provider =>
            provider.GetService<IMongoClient>().GetDatabase(databaseName));

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMongoDB(mongoClient, databaseName));
    }
}