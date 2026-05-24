namespace SchoolPortal.StudentApp.Models;

public class Student : BaseEntity
{
    public string FirstName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string Email { get; set; } = default!;

    public DateTime DateOfBirth { get; set; }

    public DateTime EnrollmentDate { get; set; }
}

