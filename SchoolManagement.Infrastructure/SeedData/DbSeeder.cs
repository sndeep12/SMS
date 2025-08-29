using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Infrastructure.Persistence;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Infrastructure.SeedData
{
    public static class DbSeeder
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                try
                {
                    context.Database.Migrate();


                    // ================= SCHOOL =================
                    if (!context.Schools.Any())
                    {
                        context.Schools.Add(new School
                        {
                            SchoolId = 1,
                            SchoolName = "ST MARY",
                            SchoolUrl = "ST MARY",
                            SchoolDescription = "ST MARY",
                            AddressDetails = "Chennai",
                            ContactNumber = "9876543210",
                            DistrictName = "Chennai",
                            RevenueDistrictName = "Chennai",
                            StateName = "TamilNadu",
                            MailId = "school@gmail.com",
                            SchoolLogo = "string",
                            CreatedBy = 1,
                            CreatedOn = DateTime.Parse("2025-08-15 17:35:41"),
                            StatusId = 1
                        });
                    }

                    // ================= ROLES =================
                    if (!context.Roles.Any())
                    {
                        context.Roles.AddRange(
                            new Role { RoleId = 1, Name = "Admin" },
                            new Role { RoleId = 2, Name = "User" }
                        );
                    }

                    // ================= USERS =================
                    if (!context.Users.Any())
                    {
                        context.Users.AddRange(
                            new User { UserId = 1, Username = "superadmin", Email = "string", PasswordHash = "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", SchoolId = 1, AdmissionNumber = "", FirstName = "super", LastName = "admin", PrimaryNumber = "98765432100", SecondaryNumber = "9876543210", StatusId = 1, UserTypeId = 1 },
                            new User { UserId = 2, Username = "schooladmin", Email = "string", PasswordHash = "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", SchoolId = 1, AdmissionNumber = "", FirstName = "school", LastName = "admin", PrimaryNumber = "98765432100", SecondaryNumber = "9876543210", StatusId = 1, UserTypeId = 2 },
                            new User { UserId = 3, Username = "parent", Email = "string", PasswordHash = "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", SchoolId = 1, AdmissionNumber = "", FirstName = "parent", LastName = "", PrimaryNumber = "98765432100", SecondaryNumber = "9876543210", StatusId = 1, UserTypeId = 4 },
                            new User { UserId = 4, Username = "teacher", Email = "string", PasswordHash = "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", SchoolId = 1, AdmissionNumber = "", FirstName = "teacher", LastName = "", PrimaryNumber = "98765432100", SecondaryNumber = "9876543210", StatusId = 1, UserTypeId = 3 },
                            new User { UserId = 5, Username = "student", Email = "string", PasswordHash = "473287f8298dba7163a897908958f7c0eae733e25d2e027992ea2edc9bed2fa8", SchoolId = 1, AdmissionNumber = "", FirstName = "student", LastName = "", PrimaryNumber = "98765432100", SecondaryNumber = "9876543210", StatusId = 1, UserTypeId = 5 }
                        );
                    }

                    // ================= USER ROLES =================
                    if (!context.UserRoles.Any())
                    {
                        context.UserRoles.Add(new UserRole { UserRoleId = 1, UserId = 1, RoleId = 1 });
                    }

                    // ================= MODULES =================
                    if (!context.Modules.Any())
                    {
                        context.Modules.AddRange(
                            new Module { ModuleId = 1, Name = "School Configuration" },
                            new Module { ModuleId = 2, Name = "Academic Module" }
                        );
                    }

                    // ================= SUBMODULES =================
                    if (!context.SubModules.Any())
                    {
                        context.SubModules.AddRange(
                            new SubModule { SubModuleId = 1, Name = "School", ModuleId = 1 },
                            new SubModule { SubModuleId = 2, Name = "AcademicYear", ModuleId = 2 },
                            new SubModule { SubModuleId = 3, Name = "Class", ModuleId = 2 },
                            new SubModule { SubModuleId = 4, Name = "Section", ModuleId = 2 },
                            new SubModule { SubModuleId = 5, Name = "SectionMapping", ModuleId = 2 },
                            new SubModule { SubModuleId = 6, Name = "Medium", ModuleId = 2 },
                            new SubModule { SubModuleId = 7, Name = "Subject", ModuleId = 2 },
                            new SubModule { SubModuleId = 8, Name = "SubjectMapping", ModuleId = 2 },
                            new SubModule { SubModuleId = 9, Name = "SubjectSectionMapping", ModuleId = 2 }
                        );
                    }

                    // ================= PERMISSIONS =================
                    if (!context.Permissions.Any())
                    {
                        context.Permissions.AddRange(
                            new Permission { PermissionId = 1, Action = "read", SubModuleId = 1 },
                            new Permission { PermissionId = 2, Action = "write", SubModuleId = 1 },
                            new Permission { PermissionId = 3, Action = "delete", SubModuleId = 1 },
                            new Permission { PermissionId = 4, Action = "export", SubModuleId = 1 },
                            new Permission { PermissionId = 5, Action = "read", SubModuleId = 2 },
                            new Permission { PermissionId = 6, Action = "write", SubModuleId = 2 },
                            new Permission { PermissionId = 7, Action = "delete", SubModuleId = 2 },
                            new Permission { PermissionId = 8, Action = "export", SubModuleId = 2 },
                            new Permission { PermissionId = 9, Action = "read", SubModuleId = 3 },
                            new Permission { PermissionId = 10, Action = "write", SubModuleId = 3 },
                            new Permission { PermissionId = 11, Action = "delete", SubModuleId = 3 },
                            new Permission { PermissionId = 12, Action = "export", SubModuleId = 3 },
                            new Permission { PermissionId = 13, Action = "read", SubModuleId = 9 },
                            new Permission { PermissionId = 14, Action = "read", SubModuleId = 8 },
                            new Permission { PermissionId = 15, Action = "read", SubModuleId = 7 },
                            new Permission { PermissionId = 16, Action = "read", SubModuleId = 6 },
                            new Permission { PermissionId = 17, Action = "read", SubModuleId = 5 },
                            new Permission { PermissionId = 18, Action = "read", SubModuleId = 4 },
                            new Permission { PermissionId = 19, Action = "write", SubModuleId = 9 },
                            new Permission { PermissionId = 20, Action = "write", SubModuleId = 8 },
                            new Permission { PermissionId = 21, Action = "write", SubModuleId = 7 },
                            new Permission { PermissionId = 22, Action = "write", SubModuleId = 6 },
                            new Permission { PermissionId = 23, Action = "write", SubModuleId = 5 },
                            new Permission { PermissionId = 24, Action = "write", SubModuleId = 4 },
                            new Permission { PermissionId = 25, Action = "delete", SubModuleId = 9 },
                            new Permission { PermissionId = 26, Action = "delete", SubModuleId = 8 },
                            new Permission { PermissionId = 27, Action = "delete", SubModuleId = 7 },
                            new Permission { PermissionId = 28, Action = "delete", SubModuleId = 6 },
                            new Permission { PermissionId = 29, Action = "delete", SubModuleId = 5 },
                            new Permission { PermissionId = 30, Action = "delete", SubModuleId = 4 },
                            new Permission { PermissionId = 31, Action = "export", SubModuleId = 9 },
                            new Permission { PermissionId = 32, Action = "export", SubModuleId = 8 },
                            new Permission { PermissionId = 33, Action = "export", SubModuleId = 7 },
                            new Permission { PermissionId = 34, Action = "export", SubModuleId = 6 },
                            new Permission { PermissionId = 35, Action = "export", SubModuleId = 5 },
                            new Permission { PermissionId = 36, Action = "export", SubModuleId = 4 }
                        );
                    }

                    // ================= ROLE PERMISSIONS =================
                    if (!context.RolePermissions.Any())
                    {
                        context.RolePermissions.AddRange(
                            new RolePermission { RolePermissionId = 1, PermissionId = 1, RoleId = 1 },
                            new RolePermission { RolePermissionId = 2, PermissionId = 2, RoleId = 1 },
                            new RolePermission { RolePermissionId = 3, PermissionId = 3, RoleId = 1 },
                            new RolePermission { RolePermissionId = 4, PermissionId = 4, RoleId = 1 },
                            new RolePermission { RolePermissionId = 5, PermissionId = 5, RoleId = 1 },
                            new RolePermission { RolePermissionId = 6, PermissionId = 6, RoleId = 1 },
                            new RolePermission { RolePermissionId = 7, PermissionId = 7, RoleId = 1 },
                            new RolePermission { RolePermissionId = 8, PermissionId = 8, RoleId = 1 },
                            new RolePermission { RolePermissionId = 9, PermissionId = 9, RoleId = 1 },
                            new RolePermission { RolePermissionId = 10, PermissionId = 10, RoleId = 1 },
                            new RolePermission { RolePermissionId = 11, PermissionId = 11, RoleId = 1 },
                            new RolePermission { RolePermissionId = 12, PermissionId = 12, RoleId = 1 },
                            new RolePermission { RolePermissionId = 13, PermissionId = 18, RoleId = 1 },
                            new RolePermission { RolePermissionId = 14, PermissionId = 24, RoleId = 1 },
                            new RolePermission { RolePermissionId = 15, PermissionId = 30, RoleId = 1 },
                            new RolePermission { RolePermissionId = 16, PermissionId = 36, RoleId = 1 },
                            new RolePermission { RolePermissionId = 17, PermissionId = 17, RoleId = 1 },
                            new RolePermission { RolePermissionId = 18, PermissionId = 23, RoleId = 1 },
                            new RolePermission { RolePermissionId = 19, PermissionId = 29, RoleId = 1 },
                            new RolePermission { RolePermissionId = 20, PermissionId = 35, RoleId = 1 },
                            new RolePermission { RolePermissionId = 21, PermissionId = 16, RoleId = 1 },
                            new RolePermission { RolePermissionId = 22, PermissionId = 22, RoleId = 1 },
                            new RolePermission { RolePermissionId = 23, PermissionId = 28, RoleId = 1 },
                            new RolePermission { RolePermissionId = 24, PermissionId = 34, RoleId = 1 },
                            new RolePermission { RolePermissionId = 25, PermissionId = 15, RoleId = 1 },
                            new RolePermission { RolePermissionId = 26, PermissionId = 21, RoleId = 1 },
                            new RolePermission { RolePermissionId = 27, PermissionId = 27, RoleId = 1 },
                            new RolePermission { RolePermissionId = 28, PermissionId = 33, RoleId = 1 },
                            new RolePermission { RolePermissionId = 29, PermissionId = 14, RoleId = 1 },
                            new RolePermission { RolePermissionId = 30, PermissionId = 20, RoleId = 1 },
                            new RolePermission { RolePermissionId = 31, PermissionId = 26, RoleId = 1 },
                            new RolePermission { RolePermissionId = 32, PermissionId = 32, RoleId = 1 },
                            new RolePermission { RolePermissionId = 33, PermissionId = 13, RoleId = 1 },
                            new RolePermission { RolePermissionId = 34, PermissionId = 19, RoleId = 1 },
                            new RolePermission { RolePermissionId = 35, PermissionId = 25, RoleId = 1 },
                            new RolePermission { RolePermissionId = 36, PermissionId = 31, RoleId = 1 }
                        );
                    }

                    // ================= ACADEMIC YEAR =================
                    if (!context.AcademicYears.Any())
                    {
                        context.AcademicYears.Add(new AcademicYear
                        {
                            AcademicYearId = 1,
                            SchoolId = 1,
                            AcademicYearName = "string",
                            FromDate = DateTime.Parse("2025-08-16 00:16:24"),
                            ToDate = DateTime.Parse("2025-08-16 00:16:24"),
                            StatusId = 1,
                            CreatedOn = DateTime.Parse("2025-08-16 00:39:01"),
                            CreatedBy = 1
                        });
                    }

                    // ================= CLASS =================
                    if (!context.Classes.Any())
                    {
                        context.Classes.AddRange(
                            new Class { ClassId = 1, SchoolId = 1, ClassName = "10", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 02:08:55"), DisplayOrder = 1 },
                            new Class { ClassId = 2, SchoolId = 1, ClassName = "11", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 02:08:55"), DisplayOrder = 2 },
                            new Class { ClassId = 3, SchoolId = 1, ClassName = "12", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 02:08:55"), DisplayOrder = 3 }
                        );
                    }

                    // ================= MEDIUM =================
                    if (!context.Mediums.Any())
                    {
                        context.Mediums.AddRange(
                            new Medium { MediumId = 1, SchoolId = 1, MediumName = "English", ShortName = "ENG", StatusId = 1, CreatedOn = DateTime.Parse("2025-08-16 02:26:08"), CreatedBy = 1 },
                            new Medium { MediumId = 2, SchoolId = 1, MediumName = "Tamil", ShortName = "TAM", StatusId = 1, CreatedOn = DateTime.Parse("2025-08-16 02:26:08"), CreatedBy = 1 },
                            new Medium { MediumId = 3, SchoolId = 1, MediumName = "Telugu", ShortName = "TEL", StatusId = 1, CreatedOn = DateTime.Parse("2025-08-16 02:26:08"), CreatedBy = 1 }
                        );
                    }

                    // ================= SECTION =================
                    if (!context.Sections.Any())
                    {
                        context.Sections.AddRange(
                            new Section { SectionId = 1, SchoolId = 1, SectionName = "A", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 02:38:08"), DisplayOrder = 1 },
                            new Section { SectionId = 2, SchoolId = 1, SectionName = "B", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 02:38:08"), DisplayOrder = 1 },
                            new Section { SectionId = 3, SchoolId = 1, SectionName = "C", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 02:38:08"), DisplayOrder = 1 }
                        );
                    }

                    // ================= SECTION MAPPING =================
                    if (!context.SectionMappings.Any())
                    {
                        context.SectionMappings.Add(new SectionMapping
                        {
                            SectionMappingId = 1,
                            SchoolId = 1,
                            ClassId = 1,
                            SectionId = 1,
                            MediumId = 1,
                            UserId = 3,
                            RoomNumber = "105",
                            StatusId = 1,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Parse("2012-12-12"),
                            UpdatedBy = 1,
                            UpdatedOn = DateTime.Parse("2012-12-12"),
                            DisplayOrder = 1
                        });
                    }

                    // ================= SUBJECT =================
                    if (!context.Subjects.Any())
                    {
                        context.Subjects.AddRange(
                            new Subject { SubjectId = 1, SchoolId = 1, SubjectName = "Tamil", SubjectCode = "TA001", SubjectType = "Theory", ShortName = "TA", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 03:06:35") },
                            new Subject { SubjectId = 2, SchoolId = 1, SubjectName = "English", SubjectCode = "Eng001", SubjectType = "Theory", ShortName = "EN", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 03:06:35") },
                            new Subject { SubjectId = 3, SchoolId = 1, SubjectName = "Math", SubjectCode = "MT001", SubjectType = "Theory", ShortName = "MT", StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2025-08-16 03:06:35") }
                        );
                    }

                    // ================= SUBJECT MAPPING =================
                    if (!context.SubjectMappings.Any())
                    {
                        context.SubjectMappings.AddRange(
                            new SubjectMapping { SubjectMappingId = 1, SchoolId = 1, SectionMappingId = 1, UserId = 3, SubjectId = 1, DisplayOrder = 1, StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2012-12-12") },
                            new SubjectMapping { SubjectMappingId = 2, SchoolId = 1, SectionMappingId = 1, UserId = 3, SubjectId = 2, DisplayOrder = 1, StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2012-12-12") },
                            new SubjectMapping { SubjectMappingId = 3, SchoolId = 1, SectionMappingId = 1, UserId = 3, SubjectId = 3, DisplayOrder = 1, StatusId = 1, CreatedBy = 1, CreatedOn = DateTime.Parse("2012-12-12") }
                        );
                    }


                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                   Log.Error("Error while seeding database: {Message}", ex.Message);
                }
               
            }
        }
    }
}
