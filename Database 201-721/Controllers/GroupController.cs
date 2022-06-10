using Database_201_721.Models;
using Database_201_721.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Database_201_721.Controllers
{
    
    public class GroupController : Controller
    {
        private readonly ApplicationContext _applicationContext;

        public GroupController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [Authorize(Roles = "admin")]
        public IActionResult GroupList()
        {

            var allGroups = _applicationContext.Groups.ToList();
            return View(allGroups);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(string groupName)
        {
            var newGroup = new Group { GroupName = groupName };
            _applicationContext.Groups.Add(newGroup);

            _applicationContext.SaveChanges();

            return RedirectToAction("GroupList");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            Group forDelete = _applicationContext.Groups.Find(id);

            _applicationContext.Groups.Remove(forDelete);
            _applicationContext.SaveChanges();


            return RedirectToAction("GroupList");
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit() => View();
        [HttpPost]

        [Authorize(Roles = "admin")]
        public IActionResult Edit(Group group)
        {
            _applicationContext.Groups.Update(group);

            _applicationContext.SaveChanges();

            return RedirectToAction("GroupList");
        }


        public IActionResult StudentsInGroup(int id)
        {
            var allStudentsAndGroup = new GroupAndStudentsViewModel
            {
                Users = _applicationContext.Users.Where(x => x.GroupId == id).ToList(),
                Group = _applicationContext.Groups.FirstOrDefault(x => x.Id == id)
            };
            return View(allStudentsAndGroup);
        }

        public IActionResult ListCoursesInGroupe(int id)
        {
            return View();
        }

        
    }
}
