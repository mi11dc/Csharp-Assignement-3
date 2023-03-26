using HTTP5101_Cumulative_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HTTP5101_Cumulative_Project.Controllers
{
    public class ClassController : Controller
    {
        // Controller which allows us to access methods from ClassDataController
        private ClassDataController Controller = new ClassDataController();

        public ActionResult Index(string Search)
        {
            IEnumerable<Class> ClassesList = Controller.ListClasses(Search);
            ViewBag.Title = "Class List Page";
            ViewData["Search"] = Search;

            return View(ClassesList);
        }

        public ActionResult Details(int id)
        {
            Class ClassDetails = Controller.ClassDetails(id);
            ViewBag.Title = "Class Details Page";

            return View(ClassDetails);
        }
    }
}