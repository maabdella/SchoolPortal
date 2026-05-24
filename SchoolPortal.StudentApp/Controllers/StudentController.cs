using Microsoft.AspNetCore.Mvc;
using SchoolPortal.StudentApp.Data.Context;
using SchoolPortal.StudentApp.Models;

namespace SchoolPortal.StudentApp.Controllers;

public class StudentController : Controller
{
	private readonly StudentDbContext _studentDbContext;
	public StudentController(StudentDbContext studentDbContext)
	{
		_studentDbContext = studentDbContext;
	}

	public IActionResult Index()
	{
		var students = _studentDbContext.Students.ToList();
		return View(students);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Student student)
    {
        if(!ModelState.IsValid)
            return View(student);
            _studentDbContext.Students.Add(student);
            _studentDbContext.SaveChanges();
        return RedirectToAction("Index");  
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var student = _studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    [HttpPost]
    public IActionResult Edit(Student student)
    {
        if (!ModelState.IsValid)
            return View(student);
        var existingStudent = _studentDbContext.Students.FirstOrDefault(s => s.Id == student.Id);
        if (existingStudent == null)
        {
            return NotFound();
        }
        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Email = student.Email;
        _studentDbContext.SaveChanges();
        return RedirectToAction("Index");
    }


    public 




    public IActionResult Details(int id)
    {
        var student = _studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }


}
