using HTTP5101_Cumulative_Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class TeacherController : Controller
    {
        // Controller which allows us to access methods from TeacherDataController
        private TeacherDataController Controller = new TeacherDataController();
        public ActionResult Index(string Search)
        {
            IEnumerable<Teacher> TeachersList = Controller.ListTeachers(Search);
            ViewBag.Title = "Teacher List Page";
            ViewData["Search"] = Search;

            return View(TeachersList);
        }

        public ActionResult Details(int id)
        {
            Teacher TeacherDetails = Controller.TeacherDetails(id);
            ViewBag.Title = "Teacher Details Page";
            
            return View(TeacherDetails);
        }

        public ActionResult ConfirmDelete(int id)
        {
            Teacher TeacherDetails = Controller.TeacherDetails(id);
            ViewBag.Title = "Confirm Delete Page";

            return View(TeacherDetails);
        }
        
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Controller.DeleteTeacher(id);
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
        public ActionResult Create(Teacher TeacherObj)
        {
            if (
                    String.IsNullOrEmpty(TeacherObj.TeacherFName) ||
                    String.IsNullOrEmpty(TeacherObj.TeacherLName) ||
                    String.IsNullOrEmpty(TeacherObj.EmployeeNumber) ||
                    String.IsNullOrEmpty(TeacherObj.Salary.ToString())
               )
                return RedirectToAction("New"); 

            int InsertedId = Controller.AddTeacher(TeacherObj);

            return RedirectToAction(String.Concat("Details/", InsertedId));
        }
    }
}