using SchoolManagement.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Users;

[Table("Staff")]
public class Staff
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StaffId { get; set; }
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User User { get; set; }
    
    [ForeignKey(nameof(School))]
    public int SchoolId { get; set; }

    // Example staff-specific properties
    public string StaffNumber { get; set; }
    public string Designation { get; set; }
    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? UpdatedBy { get; set; }

    // Working fields
    [StringLength(100)]
    public string? Qualification { get; set; }

    [StringLength(100)]
    public string? Experience { get; set; }
    [StringLength(12)]
    public string? AadharNumber { get; set; }

    [StringLength(10)]
    public string? PAN { get; set; }
    [StringLength(10)]
    public string? BloodGroup { get; set; }
    public DateTime? DateOfJoining { get; set; }

    public DateTime? DateOfBirth { get; set; }


    [StringLength(250)]
    public string? Address { get; set; }
    public virtual School School { get; set; } = null!;

}