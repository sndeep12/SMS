using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Academic
{
    [Table("Medium")]
    public class Medium
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MediumId { get; set; }

        [Required]
        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }

        [Required]
        [StringLength(50)]
        public string MediumName { get; set; } = string.Empty;

        [StringLength(50)]
        public string? ShortName { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        // 🔹 Navigation Properties
        public virtual School School { get; set; } = null!;
        public ICollection<SectionMapping> SectionMappings { get; set; } = new List<SectionMapping>();


    }
}
