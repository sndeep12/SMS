using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entities.Users
{
    [Table("StudentMapping")]
    public class StudentMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentMappingId { get; set; }

        public int SectionMappingId { get; set; }
        public int SchoolId { get; set; }
        public int AcademicYearId { get; set; }
        public int UserId { get; set; }

        public bool? IsPassed { get; set; }
        [MaxLength(50)]
        public string? RollId { get; set; }
        public long StatusId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [MaxLength(50)]
        public string? ExamRollNumber { get; set; }

        // ✅ Navigation properties
        public SectionMapping SectionMapping { get; set; } = null!;
        public AcademicYear AcademicYear { get; set; } = null!;
        public School School { get; set; } = null!;
        public User User { get; set; } = null!;
    }

}
