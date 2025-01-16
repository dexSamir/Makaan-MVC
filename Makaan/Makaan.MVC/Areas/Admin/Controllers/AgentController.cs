using Makaan.BL.Extension;
using Makaan.BL.VM.Agent;
using Makaan.Core.Entities;
using Makaan.DAL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Makaan.MVC.Areas.Admin.Controllers;
[Area("Admin")]
public class AgentController(AppDbContext _context, IWebHostEnvironment _env) : Controller
{
    public async Task<IActionResult> Index()
    {
        return View(await _context.Agents.Include(x=> x.Department).ToListAsync());
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Departments = await _context.Departments.Where(x=> !x.IsDeleted).ToListAsync();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AgentCreateVM vm)
    {
        if (vm.Image != null)
        {
            if (!vm.Image.IsValidType("image"))
                ModelState.AddModelError("File", "File must be image!");

            if (!vm.Image.IsValidSize(3*1024))
                ModelState.AddModelError("File", "File must be less than 5mb!");
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Department = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
            return View();
        }
        Agent agent = new Agent
        {
            Fullname = vm.Fullname,
            DepartmentId = vm.DepartmentId,
            ProfileImageUrl = await vm.Image.UploadAysnc(_env.WebRootPath, "imgs", "agents"),
        };
        await _context.AddAsync(agent);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Update(int? id)
    {
        ViewBag.Departments = await _context.Departments.Where(x => !x.IsDeleted).ToListAsync();
        if (!id.HasValue) return BadRequest();

        var data = await _context.Agents.FindAsync(id);
        if (data == null) return NotFound();
        AgentUpdateVM vm = new();

        vm.Fullname = data.Fullname;
        vm.ExistingImageUrl = data.ProfileImageUrl;
        vm.DepartmentId = data.DepartmentId;

        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int? id, AgentUpdateVM vm)
    {
        if (!id.HasValue) return BadRequest();
        var data = await _context.Agents.FindAsync(id);
        if (data == null) return NotFound();
        if (!ModelState.IsValid) return View(vm);

        if (vm.Image != null)
        {
            if (!vm.Image.IsValidType("image"))
            {
                ModelState.AddModelError("File", "File type must be an image");
                return View(vm);
            }
            if (!vm.Image.IsValidSize(300))
            {
                ModelState.AddModelError("File", "File must be less than 300kb");
                return View(vm);
            }

            string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), _env.WebRootPath, "imgs", "agents", data.ProfileImageUrl);

            if (System.IO.File.Exists(oldFilePath))
                System.IO.File.Delete(oldFilePath);

            string newFileName = await vm.Image.UploadAysnc(_env.WebRootPath, "imgs", "agents");
            data.ProfileImageUrl = newFileName;
        }
        data.Fullname = vm.Fullname;
        data.DepartmentId = vm.DepartmentId;

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ToggleDepartmentVIsibility(int? id, bool isdeleted)
    {
        if (!id.HasValue) return BadRequest();
        var data = await _context.Agents.FindAsync(id);
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
        var data = await _context.Agents.FindAsync(id);
        if (data == null) return NotFound();

        _context.Agents.Remove(data);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
