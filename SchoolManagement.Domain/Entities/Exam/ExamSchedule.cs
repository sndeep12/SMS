using SchoolManagement.Domain.Entities.Academic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entities
{
    [Table("ExamSchedule")]
    public class ExamSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamScheduleId { get; set; }

        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }

        public DateTime ExamDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public int MaxMarks { get; set; }
        public int PassingMarks { get; set; }

        // Navigation
        public Exam Exam { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public ICollection<ExamMark> ExamMarks { get; set; } = new List<ExamMark>();
    }
}
