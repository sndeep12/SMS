using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Auth
{
    public class Module
    {
        public int ModuleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<SubModule> SubModules { get; set; } = new List<SubModule>();
    }
}
