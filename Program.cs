using KKESH_ROP.Configuration;
using KKESH_ROP.Services;
using MongoDB.Driver;
using Prometheus;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddApiConfigurations(builder.Configuration);
builder.Services.AddControllers();

// Add HttpClient
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<TapPaymentService>();


// Add services to the container
builder.Services.AddCorsServices();
builder.Services.AddServices();
builder.Services.AddDbContextServices(builder.Configuration);
builder.Services.AddSwaggerServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddMappers();



builder.Host.AddLoggingConfiguration();


var app = builder.Build();

app.UseRouting();

app.UseMetricServer();   // Exposes /metrics
app.UseHttpMetrics();    // Measures HTTP metrics

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseSwaggerWithCustomConfig();
app.MapControllers();

app.Run();