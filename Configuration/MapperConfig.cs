using KKESH_ROP.Mappers;

namespace KKESH_ROP.Configuration;

public static class MapperConfig
{
    public static void AddMappers(this IServiceCollection services)
    {

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<PatientProfile>();
            cfg.AddProfile<ScreenerProfile>();
            cfg.AddProfile<ConsultantProfile>();
            cfg.AddProfile<PhysicianProfile>();
            cfg.AddProfile<ProductManagerProfile>();
            cfg.AddProfile<MedicalCenterProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<DiagnoseProfile>();
            cfg.AddProfile<DiagnoseRequestProfile>();
            cfg.AddProfile<JoinPhysicianProfile>();
                        cfg.AddProfile<JoinPhysicianProfile>();


        });
    }
}