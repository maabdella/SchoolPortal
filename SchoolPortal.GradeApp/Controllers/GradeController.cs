using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolPortal.GradeApp.Data.Context;
using SchoolPortal.GradeApp.Models;
using SchoolPortal.GradeApp.Models.ViewModels;
using SchoolPortal.GradeApp.Services;

namespace SchoolPortal.GradeApp.Controllers;

public class GradeController : Controller
{
    private readonly GradeAppDbContext _gradeDbContext;
    private readonly StudentsServiceClient _studentsServiceClient;

    public GradeController(GradeAppDbContext gradeDbContext, StudentsServiceClient studentsServiceClient)
    {
        _gradeDbContext = gradeDbContext;
        _studentsServiceClient = studentsServiceClient;
    }

    public async Task<IActionResult> Index()
    {
        var grades = await _gradeDbContext.Grades.ToListAsync();
        var students = await _studentsServiceClient.GetAllStudents();

        if (students == null)
        {
            ViewBag.StudentsServiceError = "The Students service is unavailable. Grade records are shown without student names.";
        }

        var model = new List<GradeListItemViewModel>();

        foreach (var g in grades)
        {
            var student = students?.FirstOrDefault(s => s.Id == g.StudentId);

            model.Add(new GradeListItemViewModel
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = student != null
                    ? $"{student.FirstName} {student.LastName}"
                    : students == null ? "Unavailable" : "Unknown student",
                CourseName = g.CourseName,
                Score = g.Score,
                GradeDate = g.GradeDate,
                Notes = g.Notes
            });
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await LoadStudentDropdown();

        return View(new Grade { GradeDate = DateTime.Today });
    }

    [HttpPost]
    public async Task<IActionResult> Create(Grade grade)
    {
        if (!ModelState.IsValid)
        {
            await LoadStudentDropdown(grade.StudentId);
            return View(grade);
        }

        _gradeDbContext.Grades.Add(grade);
        await _gradeDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var grade = await _gradeDbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
        if (grade == null)
        {
            return NotFound();
        }

        if (!await LoadStudentDropdown(grade.StudentId))
        {
            ViewBag.StudentsServiceError = "The Students service is unavailable. Student list could not be loaded.";
        }

        return View(grade);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Grade grade)
    {
        if (!ModelState.IsValid)
        {
            await LoadStudentDropdown(grade.StudentId);
            return View(grade);
        }

        var existingGrade = await _gradeDbContext.Grades.FirstOrDefaultAsync(g => g.Id == grade.Id);
        if (existingGrade == null)
        {
            return NotFound();
        }

        existingGrade.StudentId = grade.StudentId;
        existingGrade.CourseName = grade.CourseName;
        existingGrade.Score = grade.Score;
        existingGrade.GradeDate = grade.GradeDate;
        existingGrade.Notes = grade.Notes;
        await _gradeDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var grade = await _gradeDbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
        if (grade == null)
        {
            return NotFound();
        }

        return View(await BuildDetailsViewModel(grade));
    }

    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var grade = await _gradeDbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
        if (grade == null)
        {
            return NotFound();
        }

        grade.IsDeleted = true;
        await _gradeDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var grade = await _gradeDbContext.Grades.FirstOrDefaultAsync(g => g.Id == id);
        if (grade == null)
        {
            return NotFound();
        }

        return View(await BuildDetailsViewModel(grade));
    }

    private async Task<GradeDetailsViewModel> BuildDetailsViewModel(Grade grade)
    {
        var student = await _studentsServiceClient.GetStudentById(grade.StudentId);
        if (student == null && grade.StudentId > 0)
        {
            ViewBag.StudentsServiceError = "Could not load student details from the Students service.";
        }

        return new GradeDetailsViewModel
        {
            Grade = grade,
            StudentName = student != null
                ? $"{student.FirstName} {student.LastName}"
                : "Unavailable"
        };
    }

    private async Task<bool> LoadStudentDropdown(int? selectedId = null)
    {
        var students = await _studentsServiceClient.GetAllStudents();

        if (students == null)
        {
            ViewBag.StudentsServiceError =
                "Cannot reach the Students service. Start StudentApp (http://localhost:5166) then refresh this page.";
            return false;
        }

        if (students.Count == 0)
        {
            ViewBag.StudentsServiceError =
                "No students found. Add students at http://localhost:5166/Student first.";
            return false;
        }

        ViewBag.StudentId = new SelectList(
            students.Select(s => new { s.Id, Name = $"{s.FirstName} {s.LastName}" }),
            "Id",
            "Name",
            selectedId);

        return true;
    }
}
