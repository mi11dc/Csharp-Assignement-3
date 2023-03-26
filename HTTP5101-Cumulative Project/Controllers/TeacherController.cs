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
    }
}