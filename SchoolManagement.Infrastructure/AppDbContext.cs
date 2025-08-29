
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Domain.Entities.Users;
using System.Reflection.Emit;

namespace SchoolManagement.Infrastructure.Persistence;

public partial class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<School> Schools { get; set; }
   
    public DbSet<User> Users => Set<User>();
    public DbSet<AcademicYear> AcademicYears { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Medium> Mediums { get; set; }
    public DbSet<Section> Sections { get; set; }
    public DbSet<SectionMapping> SectionMappings { get; set; }
    public DbSet<Subject> Subjects { get; set; }  // ✅ Added
    public DbSet<SubjectMapping> SubjectMappings { get; set; }  // ✅ Added

    public DbSet<Student> Students { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<StudentMapping> StudentMappings { get; set; }

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<Module> Modules => Set<Module>();
    public DbSet<SubModule> SubModules => Set<SubModule>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<School>(entity =>
        {
            entity.HasKey(s => s.SchoolId);
            entity.Property(s => s.SchoolName).IsRequired().HasMaxLength(200);
            entity.Property(s => s.SchoolUrl).HasMaxLength(500);
            entity.Property(s => s.MailId).HasMaxLength(200);

        });
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.UserId);
            e.Property(x => x.Username).HasMaxLength(100).IsRequired();
            e.Property(x => x.Email).HasMaxLength(100).IsRequired();
            e.HasOne(u => u.School)         // User has one School
          .WithMany(s => s.Users)        // School has many Users (we need to add Users collection in School)
          .HasForeignKey(u => u.SchoolId) // FK in User
          .OnDelete(DeleteBehavior.SetNull); // optional: set behavior on delete

        });


        // Permission
        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(p => p.PermissionId);

            entity.Property(p => p.Action)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.HasOne(p => p.SubModule)
                  .WithMany(sm => sm.Permissions)
                  .HasForeignKey(p => p.SubModuleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Module
        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(m => m.ModuleId);

            entity.Property(m => m.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // SubModule
        modelBuilder.Entity<SubModule>(entity =>
        {
            entity.HasKey(sm => sm.SubModuleId);

            entity.Property(sm => sm.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasOne(sm => sm.Module)
                  .WithMany(m => m.SubModules)
                  .HasForeignKey(sm => sm.ModuleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Role
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.RoleId);

            entity.Property(r => r.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        });

        // RolePermission (many-to-many between Role and Permission)
        modelBuilder.Entity<RolePermission>(entity =>
        {
            entity.HasKey(rp => new { rp.RolePermissionId });

            entity.HasOne(rp => rp.Role)
                  .WithMany(r => r.RolePermissions)
                  .HasForeignKey(rp => rp.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(rp => rp.Permission)
                  .WithMany(p => p.RolePermissions)
                  .HasForeignKey(rp => rp.PermissionId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // UserRole (many-to-many between User and Role)
        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(ur => ur.UserRoleId);

            entity.HasOne(ur => ur.User)
                  .WithMany(u => u.UserRoles)
                  .HasForeignKey(ur => ur.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(ur => ur.Role)
                  .WithMany(r => r.UserRoles)
                  .HasForeignKey(ur => ur.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);
        });



        modelBuilder.Entity<RefreshToken>()
           .HasOne(rt => rt.User)
           .WithMany(u => u.RefreshTokens)
           .HasForeignKey(rt => rt.UserId)
           .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<AcademicYear>(entity =>
        {
            entity.HasKey(a => a.AcademicYearId);
            entity.Property(a => a.AcademicYearName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasOne(a => a.School)
                  .WithMany(s => s.AcademicYears) // You'll need to add ICollection<AcademicYear> in School model
                  .HasForeignKey(a => a.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(a => a.ClassId);
            entity.Property(a => a.ClassName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.HasOne(a => a.School)
                  .WithMany(s => s.Classes) // You'll need to add ICollection<Class> in School model
                  .HasForeignKey(a => a.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // 🔹 Medium config
        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(m => m.MediumId);

            entity.Property(m => m.MediumName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(m => m.ShortName)
                  .HasMaxLength(50);

            entity.HasOne(m => m.School)
                  .WithMany(s => s.Mediums)
                  .HasForeignKey(m => m.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(s => s.SectionId);

            entity.Property(s => s.SectionName)
                  .IsRequired()
                  .HasMaxLength(250);

            entity.HasOne(s => s.School)
                  .WithMany(sch => sch.Sections)
                  .HasForeignKey(s => s.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Subject config
        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(s => s.SubjectId);
            entity.Property(s => s.SubjectName).HasMaxLength(100).IsRequired();
            entity.Property(s => s.SubjectCode).HasMaxLength(50);
            entity.Property(s => s.SubjectType).HasMaxLength(50);
            entity.Property(s => s.ShortName).HasMaxLength(10);

            entity.HasOne(s => s.School)
                  .WithMany(sch => sch.Subjects)
                  .HasForeignKey(s => s.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SectionMapping>(entity =>
        {
            entity.HasKey(sm => sm.SectionMappingId);

            entity.HasOne(sm => sm.School)
                  .WithMany(s => s.SectionMappings)
                  .HasForeignKey(sm => sm.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.Class)
                  .WithMany(c => c.SectionMappings)
                  .HasForeignKey(sm => sm.ClassId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.Section)
                  .WithMany(s => s.SectionMappings)
                  .HasForeignKey(sm => sm.SectionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.Medium)
                  .WithMany(m => m.SectionMappings)
                  .HasForeignKey(sm => sm.MediumId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.User)
                  .WithMany(u => u.SectionMappings)
                  .HasForeignKey(sm => sm.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(sm => sm.RoomNumber).HasMaxLength(20);
        });
        modelBuilder.Entity<SubjectMapping>(entity =>
        {
            entity.HasKey(sm => sm.SubjectMappingId);

            entity.HasOne(sm => sm.School)
                  .WithMany(s => s.SubjectMappings)
                  .HasForeignKey(sm => sm.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.SectionMapping)
                  .WithMany(sm => sm.SubjectMappings)
                  .HasForeignKey(sm => sm.SectionMappingId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.Subject)
                  .WithMany(s => s.SubjectMappings)
                  .HasForeignKey(sm => sm.SubjectId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(sm => sm.User)
                  .WithMany(u => u.SubjectMappings)
                  .HasForeignKey(sm => sm.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
        // Student configuration
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(s => s.StudentId);

            entity.HasOne(s => s.User)
                  .WithMany()
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(s => s.School)
                 .WithMany()
                 .HasForeignKey(s => s.SchoolId)
                 .OnDelete(DeleteBehavior.Cascade);

            entity.Property(s => s.AdmissionNumber).HasMaxLength(50);
            entity.Property(s => s.Caste).HasMaxLength(50);
            entity.Property(s => s.Religion).HasMaxLength(50);
            entity.Property(s => s.BloodGroup).HasMaxLength(10);
            entity.Property(s => s.SubCaste).HasMaxLength(100);
            entity.Property(s => s.Address).HasMaxLength(250);
            entity.Property(s => s.Community).HasMaxLength(50);
            entity.Property(s => s.AadharNumber).HasMaxLength(12);
            entity.Property(s => s.PAN).HasMaxLength(10);
            entity.Property(s => s.FatherName).HasMaxLength(100);
            entity.Property(s => s.MotherName).HasMaxLength(100);
            entity.Property(s => s.FatherOccupation).HasMaxLength(100);
            entity.Property(s => s.MotherOccupation).HasMaxLength(100);
            entity.Property(s => s.Nationality).HasMaxLength(50);

           
        });

        // Staff configuration
        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(s => s.StaffId);

            entity.HasOne(s => s.User)
                  .WithMany()
                  .HasForeignKey(s => s.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(s => s.School)
                  .WithMany()
                  .HasForeignKey(s => s.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.Property(s => s.StaffNumber).HasMaxLength(50);
            entity.Property(s => s.Designation).HasMaxLength(100);
            entity.Property(s => s.BloodGroup).HasMaxLength(10);
            entity.Property(s => s.Address).HasMaxLength(250);
            entity.Property(s => s.AadharNumber).HasMaxLength(12);
            entity.Property(s => s.PAN).HasMaxLength(10);
            entity.Property(s => s.Qualification).HasMaxLength(100);
            entity.Property(s => s.Experience).HasMaxLength(100);
        });

        modelBuilder.Entity<StudentMapping>(entity =>
        {
            entity.HasKey(e => e.StudentMappingId);

            entity.Property(e => e.RollId).HasMaxLength(50);
            entity.Property(e => e.ExamRollNumber).HasMaxLength(50);

            entity.HasOne(e => e.SectionMapping)
                  .WithMany(s => s.StudentMappings) // 🔹 add collection navigation on SectionMapping
                  .HasForeignKey(e => e.SectionMappingId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.AcademicYear)
                  .WithMany(a => a.StudentMappings) // 🔹 add collection navigation on AcademicYear
                  .HasForeignKey(e => e.AcademicYearId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.School)
                  .WithMany(s => s.StudentMappings) // 🔹 add collection navigation on School
                  .HasForeignKey(e => e.SchoolId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                  .WithMany(u => u.StudentMappings) // 🔹 add collection navigation on User
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
