using SchoolManagement.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entities
{
    [Table("ExamMark")]
    public class ExamMark
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamMarkId { get; set; }

        [ForeignKey(nameof(ExamSchedule))]
        public int ExamScheduleId { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        public int MarksObtained { get; set; }
        public string? Grade { get; set; } // A+, A, B, C...

        // Navigation
        public ExamSchedule ExamSchedule { get; set; } = null!;
        public User Student { get; set; } = null!;
    }
}
