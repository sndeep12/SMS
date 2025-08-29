using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Academic
{
    public class SectionMappingDto
    {
        public int SectionMappingId { get; set; }
        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }

        public int ClassId { get; set; }
        public string? ClassName { get; set; }

        public int SectionId { get; set; }
        public string? SectionName { get; set; }

        public int MediumId { get; set; }
        public string? MediumName { get; set; }

        public int UserId { get; set; }
        public string? UserName { get; set; }

        public string? RoomNumber { get; set; }
        public int StatusId { get; set; }
        public int? DisplayOrder { get; set; }
    }

}
