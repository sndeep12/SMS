using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Academic
{
    public class AcademicYearDto
    {
        public string? SchoolName { get; set; }
        
        public int AcademicYearId { get; set; }
        public int SchoolId { get; set; }
        public string AcademicYearName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int StatusId { get; set; }
    }
}
