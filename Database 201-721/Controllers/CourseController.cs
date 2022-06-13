using Database_201_721.Models;
using Database_201_721.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Database_201_721.Controllers
{
    public class CourseController : Controller
    {

        private readonly ApplicationContext _applicationContext;

        public CourseController(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        [Authorize(Roles = "admin")]
        public IActionResult CourseList()
        {
            var courseList = _applicationContext.Courses.ToList();
            return View(courseList);
        }


        [Authorize(Roles = "admin")]
        public IActionResult AddCourse()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AddCourse(CourseViewModel courseViewModel)
        {
            if (courseViewModel != null)
            {
                Course newCourse = new Course();
                newCourse.Id = courseViewModel.Id;
                newCourse.CourseName = courseViewModel.CourseName;

                _applicationContext.Courses.Add(newCourse);
            }
            _applicationContext.SaveChanges();
            return RedirectToAction("CourseList");
        }



        [Authorize(Roles = "admin")]
        public IActionResult Edit() => View();
        [HttpPost]

        [Authorize(Roles = "admin")]
        public IActionResult Edit(Course course)
        {
            _applicationContext.Courses.Update(course);

            _applicationContext.SaveChanges();

            return RedirectToAction("CourseList");
        }


        public IActionResult Delete(int id)
        {
            Course forDelete = _applicationContext.Courses.Find(id);
            
            if (forDelete != null)
            {
                _applicationContext.Courses.Remove(forDelete);
            }
            
            _applicationContext.SaveChanges();

            return RedirectToAction("CourseList");
        }

    }
}
