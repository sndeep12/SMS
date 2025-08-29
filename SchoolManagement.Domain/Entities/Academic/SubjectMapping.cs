using SchoolManagement.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Academic
{
    
    [Table("SubjectMapping")]
    public class SubjectMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubjectMappingId { get; set; }
        
        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }

        [ForeignKey(nameof(SectionMapping))]
        public int SectionMappingId { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(Subject))]
        public int SubjectId { get; set; }

        public int? DisplayOrder { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

       

        // Navigation properties
        public School School { get; set; } = null!;
        public SectionMapping SectionMapping { get; set; } = null!;
        public Subject Subject { get; set; } = null!;
        public User User { get; set; } = null!;

        
    }
}
