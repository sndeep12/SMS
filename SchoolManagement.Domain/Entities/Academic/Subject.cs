using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Academic
{
    [Table("Subject")]
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }

        [Required]
        public int SchoolId { get; set; }

        [Required]
        [StringLength(100)]
        public string SubjectName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? SubjectCode { get; set; }

        [StringLength(50)]
        public string? SubjectType { get; set; }

        [StringLength(10)]
        public string? ShortName { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        // 🔹 Navigation Properties
        public School School { get; set; } = null!;
        public ICollection<SubjectMapping> SubjectMappings { get; set; } = new List<SubjectMapping>();

    }
}
