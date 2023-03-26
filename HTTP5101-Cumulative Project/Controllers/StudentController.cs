using HTTP5101_Cumulative_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class StudentController : Controller
    {
        // Controller which allows us to access methods from StudentDataController
        private StudentDataController Controller = new StudentDataController();
        
        public ActionResult Index(string Search)
        {
            IEnumerable<Student> StudentsList = Controller.ListStudents(Search);
            ViewBag.Title = "Student List Page";
            ViewData["Search"] = Search;

            return View(StudentsList);
        }

        public ActionResult Details(int id)
        {
            Student StudentDetails = Controller.StudentDetails(id);
            ViewBag.Title = "Studnet Details Page";

            return View(StudentDetails);
        }
    }
}