using Makaan.BL.VM.Department;
using Makaan.Core.Entities;
using Makaan.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Makaan.MVC.Areas.Admin.Controllers;
[Area("Admin")]
public class DepartmentController(AppDbContext _context) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _context.Departments.ToListAsync());
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DepartmentCreateVM vm)
    {
        if(!ModelState.IsValid) return View();

        Department department = new Department
        {
            Name = vm.Name, 
        };
        _context.Departments.Add(department);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (!id.HasValue) return BadRequest();
        var data = await _context.Departments.FindAsync(id);
        if (data == null) return NotFound();
        DepartmentUpdateVM vm = new DepartmentUpdateVM
        {
            Name = data.Name,
        }; 
        return View(vm);    
    }
    [HttpPost]
    public async Task<IActionResult> Update(int? id, DepartmentUpdateVM vm)
    {
        if (!id.HasValue) return BadRequest();
        var data = await _context.Departments.FindAsync(id);
        if (data == null) return NotFound();

        data.Name = vm.Name;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> ToggleDepartmentVIsibility(int? id, bool isdeleted)
    {
        if (!id.HasValue) return BadRequest(); 
        var data = await _context.Departments.FindAsync(id);
        if (data == null) return NotFound(); 

        data.IsDeleted = isdeleted; 
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index)); 
    }
    public async Task<IActionResult> Hide(int? id)
    {
        return await ToggleDepartmentVIsibility(id, true); 
    }
    public async Task<IActionResult> Show(int? id)
    {
        return await ToggleDepartmentVIsibility(id, false);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (!id.HasValue) return BadRequest();
        var data = await _context.Departments.FindAsync(id);
        if (data == null) return NotFound();
        
        _context.Departments.Remove(data);  
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
