using SchoolManagement.Domain.Entities.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Domain.Entities
{
    [Table("ExamResult")]
    public class ExamResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamResultId { get; set; }

        [ForeignKey(nameof(Exam))]
        public int ExamId { get; set; }

        [ForeignKey(nameof(Student))]
        public int StudentId { get; set; }

        public int TotalMarks { get; set; }
        public double Percentage { get; set; }
        public string Grade { get; set; } = null!;
        public int Rank { get; set; }

        // Navigation
        public Exam Exam { get; set; } = null!;
        public User Student { get; set; } = null!;
    }
}
