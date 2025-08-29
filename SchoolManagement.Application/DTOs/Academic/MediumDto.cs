using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Academic
{
    public class MediumDto
    {
        public int MediumId { get; set; }
        public int SchoolId { get; set; }
        public string? SchoolName { get; set; }
        public string MediumName { get; set; } = string.Empty;
        public string? ShortName { get; set; }
        public int StatusId { get; set; }
    }

}
