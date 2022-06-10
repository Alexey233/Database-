using Database_201_721.Models;
using Database_201_721.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Database_201_721.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserManageController : Controller
    {
        UserManager<User> _userManager;
        private readonly ApplicationContext _applicationContext;
        public UserManageController(UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { 
                    Email = model.Email, 
                    UserName = model.Email, 
                    Year = model.Year, 
                    FirstName = model.FirstName, 
                    LastName = model.LastName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var allGroup = _applicationContext.Groups.ToList();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Email = user.Email, Year = user.Year, Groups= allGroup };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.UserName = model.Email;
                    user.Year = model.Year;


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        public async Task<IActionResult> AddOrEditGroup(string id)
        {
            var allGroup = _applicationContext.Groups.ToList();
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            EditUserViewModel model = new EditUserViewModel { Id = user.Id, Groups = allGroup };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditGroup(EditUserViewModel model)
        {
            User user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                var group = _applicationContext.Groups.FirstOrDefault(x => x.Id == model.GroupId);

                user.Group = group;
                if (group.Users == null)
                {
                    List<User> users = new List<User>();
                    users.Add(user);
                    group.Users = users;
                }
                else
                {
                    group.Users.Add(user);
                }

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else 
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> ChangeRequeist(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<ActionResult> ChangeRequeist(User userFromView)
        {
            var user = await _userManager.FindByIdAsync(userFromView.Id);

            if (user.ChangeRequestYear != 0)
            {
                user.Year = (int)user.ChangeRequestYear;
                user.ChangeRequestYear = null;
            }
            if (user != null)
            {
                _applicationContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
