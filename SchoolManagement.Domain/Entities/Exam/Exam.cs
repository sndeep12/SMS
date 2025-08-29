using SchoolManagement.Domain.Entities.Academic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }

        [ForeignKey(nameof(AcademicYear))]
        public int AcademicYearId { get; set; }

        [ForeignKey(nameof(ExamType))]
        public int ExamTypeId { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = null!; // “Annual Exam 2025”

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation
        public School School { get; set; } = null!;
        public AcademicYear AcademicYear { get; set; } = null!;
        public ExamType ExamType { get; set; } = null!;
        public ICollection<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();
    }
}
