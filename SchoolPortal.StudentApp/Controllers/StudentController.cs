using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IActionResult> Create(Student student)
    {
        if(!ModelState.IsValid)
            return View(student);
            _studentDbContext.Students.Add(student);
            await _studentDbContext.SaveChangesAsync();
        return  RedirectToAction("Index");  
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
    public async Task<IActionResult> Edit(Student student)
    {
        if (!ModelState.IsValid)
            return View(student);
        var existingStudent =await _studentDbContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);
        if (existingStudent == null)
        {
            return NotFound();
        }
        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.Email = student.Email;
        await _studentDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var student = _studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }





    [HttpPost]
    [ActionName("Delete")]
    public async Task<IActionResult> DeleteCmfirmed(int id)
    {
        var student = await _studentDbContext.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        student.IsDeleted = true;
        await _studentDbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }




    public IActionResult Details(int id)
    {
        var student = _studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }


    public ActionResult GetAll()
    {
        var students = _studentDbContext.Students.ToList();
        return Json(students);
    }

    public ActionResult GetById(int id)
    {
        var student = _studentDbContext.Students.FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            return NotFound();
        }
        return Json(student);
    }




}
