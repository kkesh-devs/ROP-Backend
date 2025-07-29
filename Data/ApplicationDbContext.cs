using KKESH_ROP.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;

namespace KKESH_ROP.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Consultant> Consultants { get; set; }
    public DbSet<MedicalCenter> MedicalCenters { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Physician> Physicians { get; set; }
    public DbSet<ProductManager> ProductManagers { get; set; }
    public DbSet<Screener> Screeners { get; set; }
    public DbSet<Diagnose> Diagnoses { get; set; }
    public DbSet<DiagnoseRequest> DiagnoseRequests { get; set; }

    public DbSet<JoinPhysicianRequests> PhysicianJoinRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<User>().ToCollection("users");
        builder.Entity<Consultant>().ToCollection("consultants");
        builder.Entity<MedicalCenter>().ToCollection("medicalCenters");
        builder.Entity<Patient>().ToCollection("patients");
        builder.Entity<Physician>().ToCollection("Physicians");
        builder.Entity<ProductManager>().ToCollection("productManagers");
        builder.Entity<Screener>().ToCollection("screeners");
        builder.Entity<Diagnose>().ToCollection("diagnoses");
        builder.Entity<DiagnoseRequest>().ToCollection("diagnoseRequests");
        builder.Entity<JoinPhysicianRequests>().ToCollection("JoinPhysicianRequests");


    }
}