using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    [Table("ExamType")]
    public class ExamType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!; // Unit Test, Half-Yearly, Annual, etc.

        [MaxLength(250)]
        public string? Description { get; set; }

        // Navigation
        public ICollection<Exam> Exams { get; set; } = new List<Exam>();
    }
}
