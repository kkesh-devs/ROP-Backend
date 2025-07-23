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

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMongoDB(mongoClient, databaseName));
    }
}