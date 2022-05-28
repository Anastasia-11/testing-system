using CourseWork.Models;
using CourseWork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
    
namespace CourseWork.Controllers;

[Authorize(Roles="admin")]
public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;
    
    public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        return View(_roleManager.Roles.ToList());
    }

    public IActionResult Create()
    {
        return View();
    } 
    
    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        if (string.IsNullOrEmpty(name)) 
            return View(name);
        
        var result = await _roleManager.CreateAsync(new IdentityRole(name));
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View(name);
    }
     
    [HttpPost]
    public async Task<IActionResult> Delete(string testId)
    {
        var role = await _roleManager.FindByIdAsync(testId);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction("Index");
    }

    public IActionResult UserList()
    {
        return View(_userManager.Users.Where(u => !u.UserName.Equals("admin")));
    } 

    public async Task<IActionResult> Edit(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) 
            return NotFound();
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var allRoles = _roleManager.Roles.ToList();
        var model = new ChangeRoleViewModel
        {
            UserId = user.Id,
            UserEmail = user.Email,
            UserRoles = userRoles,
            AllRoles = allRoles
        };
        return View(model);

    }
    
    [HttpPost]
    public async Task<IActionResult> Edit(string userId, List<string> roles)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) 
            return NotFound();
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var addedRoles = roles.Except(userRoles);
        var removedRoles = userRoles.Except(roles);

        await _userManager.AddToRolesAsync(user, addedRoles);
        await _userManager.RemoveFromRolesAsync(user, removedRoles);

        return RedirectToAction("UserList");
    }
}