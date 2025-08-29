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
    [Table("AcademicYear")]
    public class AcademicYear
    {
        [Key]
        public int AcademicYearId { get; set; }
        public int SchoolId { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StatusId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        // Navigation property
        public School School { get; set; }
        public ICollection<StudentMapping> StudentMappings { get; set; } = new HashSet<StudentMapping>();
    }
}
