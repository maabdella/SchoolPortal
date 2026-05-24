using SchoolPortal.StudentApp.Models;

namespace SchoolPortal.GradeApp.Models;

public class Grade : BaseEntity
{
    public int StudentId { get; set; }

    public string CourseName { get; set; }

    public double Score { get; set; }

    public DateTime GradeDate { get; set; }

    public string? Notes { get; set; }
}