namespace SchoolPortal.GradeApp.Models;

public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
