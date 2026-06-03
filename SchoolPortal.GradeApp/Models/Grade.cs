using System.ComponentModel.DataAnnotations;

namespace SchoolPortal.GradeApp.Models;

public class Grade : BaseEntity
{
    [Required(ErrorMessage = "Please select a student.")]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a student.")]
    public int StudentId { get; set; }

    [Required]
    [StringLength(100)]
    public string CourseName { get; set; } = default!;

    [Required]
    [Range(0, 100)]
    public double Score { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime GradeDate { get; set; }

    public string? Notes { get; set; }
}