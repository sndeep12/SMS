using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities
{
    [Table("GradeScale")]
    public class GradeScale
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeScaleId { get; set; }

        public int MinMarks { get; set; }
        public int MaxMarks { get; set; }

        [Required]
        [MaxLength(5)]
        public string Grade { get; set; } = null!; // A1, A2, B1, etc.

        public double? GradePoint { get; set; } // 10, 9, 8...

    }
}
