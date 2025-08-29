using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Academic
{
    public class SectionDto
    {
        public int SectionId { get; set; }
        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }
        public string SectionName { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public int? DisplayOrder { get; set; }
    }
}
