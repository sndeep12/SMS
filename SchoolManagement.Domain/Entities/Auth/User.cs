
using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Entities.Auth
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int UserTypeId { get; set; }
        public int? ParentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AdmissionNumber { get; set; }

        public string PrimaryNumber { get; set; }

        public string SecondaryNumber { get; set; }

        public string? ProfilePath { get; set; }
        public int? SchoolId { get; set; }
        public School? School { get; set; }
        [Required]
        public int StatusId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        // Navigation property
        public List<RefreshToken> RefreshTokens { get; set; } = new();
        public List<UserRole> UserRoles { get; set; }
        public ICollection<SectionMapping> SectionMappings { get; set; } = new List<SectionMapping>();
        public ICollection<SubjectMapping> SubjectMappings { get; set; } = new List<SubjectMapping>();
        public ICollection<StudentMapping> StudentMappings { get; set; } = new HashSet<StudentMapping>();

    }
}
