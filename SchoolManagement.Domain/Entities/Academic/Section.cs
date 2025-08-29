using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Academic
{
    [Table("Section")]
    public class Section
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionId { get; set; }
        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }
        [Required]
        [StringLength(250)]
        public string SectionName { get; set; } = null!;
        [Required]
        public int StatusId { get; set; }
        [Required]
        public int CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? DisplayOrder { get; set; }

        // 🔗 Navigation Properties
        public School School { get; set; } = null!;

        public ICollection<SectionMapping> SectionMappings { get; set; } = new List<SectionMapping>();

    }
}
