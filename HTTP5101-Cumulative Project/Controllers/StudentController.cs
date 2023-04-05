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

        public ActionResult ConfirmDelete(int id)
        {
            Student StudentDetails = Controller.StudentDetails(id);
            ViewBag.Title = "Confirm Delete Page";

            return View(StudentDetails);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Controller.DeleteStudent(id);
            return RedirectToAction("Index");
        }

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(Student StudentObj)
        {
            if (
                    String.IsNullOrEmpty(StudentObj.StudentFName) ||
                    String.IsNullOrEmpty(StudentObj.StudentLName) ||
                    String.IsNullOrEmpty(StudentObj.StudentNumber)
               )
                return RedirectToAction("New");

            int InsertedId = Controller.AddStudent(StudentObj);

            return RedirectToAction(String.Concat("Details/", InsertedId));
        }
    }
}