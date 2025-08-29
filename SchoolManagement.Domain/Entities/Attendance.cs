using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{

    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AttendanceId { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }

        [ForeignKey(nameof(AcademicYear))]
        public int? AcademicYearId { get; set; }

        [ForeignKey(nameof(SectionMapping))]
        public int? SectionMappingId { get; set; }

        [ForeignKey(nameof(User))] // Student
        public int UserId { get; set; }

        [Required]
        public DateTime AttendanceDate { get; set; }

        [Required]
        [MaxLength(20)]
        public int Status { get; set; } =0; // Present / Absent / Leave / Holiday

        [MaxLength(250)]
        public string? Remarks { get; set; }

        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // 🔹 Navigation Properties
        public virtual School School { get; set; } = null!;
        public virtual AcademicYear? AcademicYear { get; set; } = null!;
        public virtual SectionMapping? SectionMapping { get; set; } = null!;
        public virtual User Users { get; set; } = null!;
    }
}
