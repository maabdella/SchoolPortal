namespace SchoolPortal.GradeApp.Models.ViewModels;

public class GradeListItemViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = "—";
    public string CourseName { get; set; } = default!;
    public double Score { get; set; }
    public DateTime GradeDate { get; set; }
    public string? Notes { get; set; }
}
