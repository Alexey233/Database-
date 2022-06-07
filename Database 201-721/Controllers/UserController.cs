using Database_201_721.Models;
using Database_201_721.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Database_201_721.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationContext _applicationContext;
        private readonly SignInManager<User> _signInManager;
        public UserController(ApplicationContext applicationContext, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _applicationContext = applicationContext;
            _signInManager = signInManager;
        }
        public IActionResult AboutMe(string email)
        {
            var userInfo = _applicationContext.Users.FirstOrDefault(u=> u.Email == email);

            return View(userInfo);
        }


        public IActionResult StudentsInGroup(string email)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Email == email);
            var userGroupId = user.GroupId;

            var allStudentsAndGroup = new GroupAndStudentsViewModel
            {
                Users = _applicationContext.Users.Where(x => x.GroupId == userGroupId).ToList(),
                Group = _applicationContext.Groups.FirstOrDefault(x => x.Id == userGroupId)
            };
            return View(allStudentsAndGroup);
        }

        public IActionResult EmptyBlank()
        {
            var AllBlank = new EmptyBlank();

            return View(AllBlank);
        }

        public IActionResult ChangeInformationAboutMe(string email)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Email == email);

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangeInformationAboutMe(User user)
        {
            var forChange = _applicationContext.Users.FirstOrDefault(u => u.Id == user.Id);

            forChange.ChangeRequestYear = user.ChangeRequestYear;

            _applicationContext.SaveChanges();


            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            return RedirectToAction("AboutMe", "User", new { email = userEmail });
        }

        

    }
}
