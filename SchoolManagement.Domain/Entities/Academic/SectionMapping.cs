using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Academic
{
    [Table("SectionMapping")]
    public class SectionMapping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SectionMappingId { get; set; }

        [ForeignKey(nameof(School))]
        public int SchoolId { get; set; }

        [ForeignKey(nameof(Class))]
        public int ClassId { get; set; }

        [ForeignKey(nameof(Section))]
        public int SectionId { get; set; }

        [ForeignKey(nameof(Medium))]
        public int MediumId { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        [MaxLength(20)]
        public string? RoomNumber { get; set; }

        [Required]
        public int StatusId { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public int? DisplayOrder { get; set; }

        // Navigation properties
        public School School { get; set; } = null!;
        public Class Class { get; set; } = null!;
        public Section Section { get; set; } = null!;
        public Medium Medium { get; set; } = null!;
        public  User User { get; set; } = null!;

        public ICollection<SubjectMapping> SubjectMappings { get; set; } = new HashSet<SubjectMapping>();
        public ICollection<StudentMapping> StudentMappings { get; set; } = new HashSet<StudentMapping>();
    }
}
