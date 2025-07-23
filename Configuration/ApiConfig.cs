namespace KKESH_ROP.Configuration;

public static class ApiConfig
{
    public static void AddApiConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TapPaymentSettings>(configuration.GetSection("TapPaymentSettings"));

    }
}
    
public class TapPaymentSettings
{
    public string Url { get; set; }
    public string MerchantId { get; set; }
    public string DevKey { get; set; }
    public string TestKey { get; set; }
    public string ProductionKey { get; set; }
}