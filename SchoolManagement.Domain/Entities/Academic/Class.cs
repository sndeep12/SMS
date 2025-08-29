using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Academic
{
    [Table("Class")]
    public class Class
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }

        [Required]
        [StringLength(250)]
        public string ClassName { get; set; } = string.Empty;

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? DisplayOrder { get; set; }

        // 🔹 Navigation Properties
        public virtual School School { get; set; } = null!;

        public ICollection<SectionMapping> SectionMappings { get; set; } = new List<SectionMapping>();


    }
}
