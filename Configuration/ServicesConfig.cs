using KKESH_ROP.Helpers;
using KKESH_ROP.Interfaces.IRepositories;
using KKESH_ROP.Interfaces.IServices;
using KKESH_ROP.Repositories;
using KKESH_ROP.Services;

namespace KKESH_ROP.Configuration;

public static class ServicesConfig
{
    public static void AddServices(this IServiceCollection services)
    {
        
        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IConsultantRepository, ConsultantRepository>();
        services.AddScoped<IScreenerRepository, ScreenerRepository>();
        services.AddScoped<IProductManagerRepository, ProductManagerRepository>();
        services.AddScoped<IMedicalCenterRepository, MedicalCenterRepository>();
        services.AddScoped<IPhysicianRepository, PhysicianRepository>();
        services.AddScoped<ITapPaymentRepository, TapPaymentRepository>();
        services.AddScoped<IDiagnoseRepository, DiagnoseRepository>();
        services.AddScoped<IDiagnoseRequestRepository, DiagnoseRequestRepository>();

        
        //Services
        services.AddScoped<ITapPaymentService, TapPaymentService>();
        
        
        services.AddScoped<TokenHelper>();
    }
}