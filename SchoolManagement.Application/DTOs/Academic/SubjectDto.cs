using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.DTOs.Academic
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }
        public int SchoolId { get; set; }
       
        public string SubjectName { get; set; } = string.Empty;
        public string? SubjectCode { get; set; }
        public string? SubjectType { get; set; }
        public string? ShortName { get; set; }
        public int StatusId { get; set; }
        public string SchoolName { get; set; } = string.Empty;
    }

}
