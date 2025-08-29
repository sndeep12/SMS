using SchoolManagement.Domain.Entities.Academic;
using SchoolManagement.Domain.Entities.Auth;
using SchoolManagement.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities;

[Table("School")]
public class School
{
    public int SchoolId { get; set; }
    public string SchoolName { get; set; }
    public string SchoolUrl { get; set; }
    public string SchoolDescription { get; set; }
    public string AddressDetails { get; set; }
    public string ContactNumber { get; set; }
    public string DistrictName { get; set; }
    public string RevenueDistrictName { get; set; }
    public string StateName { get; set; }
    public string MailId { get; set; }
    public string SchoolLogo { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public int StatusId { get; set; }
    public long? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<AcademicYear> AcademicYears { get; set; } = new List<AcademicYear>();
    public ICollection<Class> Classes { get; set; } = new HashSet<Class>();
    public ICollection<Section> Sections { get; set; } = new List<Section>();
    public ICollection<Medium> Mediums { get; set; } = new List<Medium>();
    public ICollection<SectionMapping> SectionMappings { get; set; } = new List<SectionMapping>();
    public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
    public ICollection<StudentMapping> StudentMappings { get; set; } = new HashSet<StudentMapping>();
    public ICollection<SubjectMapping> SubjectMappings { get; set; } = new List<SubjectMapping>();


}
