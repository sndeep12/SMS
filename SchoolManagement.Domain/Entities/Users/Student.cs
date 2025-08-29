using SchoolManagement.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Domain.Entities.Users;
[Table("Student")]
public class Student
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudentId { get; set; }
    [Required]
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    [ForeignKey(nameof(School))]
    public int SchoolId { get; set; }
    public User User { get; set; }
   

    public int StatusId { get; set; }
    public DateTime CreatedOn { get; set; }
    public int CreatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public int? UpdatedBy { get; set; }

    // Additional fields
    [StringLength(50)]
    public string? AdmissionNumber { get; set; }

    [StringLength(50)]
    public string? Caste { get; set; }

    [StringLength(50)]
    public string? Religion { get; set; }

    [StringLength(10)]
    public string? BloodGroup { get; set; }

    [StringLength(100)]
    public string? SubCaste { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? Community { get; set; }

    [StringLength(12)]
    public string? AadharNumber { get; set; }

    [StringLength(10)]
    public string? PAN { get; set; }

    public bool IsHosteller { get; set; }
    public bool IsBusFacility { get; set; }

    [StringLength(100)]
    public string? FatherName { get; set; }

    [StringLength(100)]
    public string? MotherName { get; set; }

    [StringLength(100)]
    public string? FatherOccupation { get; set; }

    [StringLength(100)]
    public string? MotherOccupation { get; set; }

    [StringLength(50)]
    public string? Nationality { get; set; }

    public DateTime? DateOfBirth { get; set; }
    public DateTime? DateOfAdmission { get; set; }
    public virtual School School { get; set; } = null!;

}