using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Academic
{
    public class SubjectMappingDto
    {
        public int SubjectMappingId { get; set; }
        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }

        public int SectionMappingId { get; set; }
        public string? SectionName { get; set; } // optional, from SectionMapping

        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public int? DisplayOrder { get; set; }
        public int StatusId { get; set; }
    }

}
