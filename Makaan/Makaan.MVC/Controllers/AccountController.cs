using Makaan.BL.VM.User;
using Makaan.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Makaan.MVC.Controllers;
public class AccountController(UserManager<User> _userManager, SignInManager<User> _signInManager) : Controller
{
    public IActionResult Register()
    {
        return View(); 
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM vm)
    {
        User user = new User
        {
            Fullname = vm.Username,
            Email = vm.Email,
            UserName = vm.Fullname,
        }; 
        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            foreach (var error  in result.Errors)
                ModelState.AddModelError("", error.Description); 

            return View();
        }
        return RedirectToAction(nameof(Login)); 
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM vm)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        User? user = null;
        if (vm.UsernameOrEmail.Contains("@"))
            user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
        
        else
            user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);
        
        if (user == null)
        {
            ModelState.AddModelError("", "Username or password is wrong!");
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);
        if (!result.Succeeded)
        {
            if (result.IsNotAllowed)
                ModelState.AddModelError("", "Username or password is wrong!");
            return View();
        }
        return RedirectToAction("Index", "Admin");

    }
} 