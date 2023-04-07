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

        public ActionResult ConfirmDelete(int id)
        {
            Class ClassDetails = Controller.ClassDetails(id);
            ViewBag.Title = "Confirm Delete Page";

            return View(ClassDetails);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Controller.DeleteClass(id);
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
        public ActionResult Create(Class ClassObj)
        {
            if (
                    String.IsNullOrEmpty(ClassObj.ClassCode) ||
                    String.IsNullOrEmpty(ClassObj.ClassName) ||
                    String.IsNullOrEmpty(ClassObj.StartDate.ToString()) ||
                    String.IsNullOrEmpty(ClassObj.EndDate.ToString())
               )
                return RedirectToAction("New");

            int InsertedId = Controller.AddClass(ClassObj);

            return RedirectToAction(String.Concat("Details/", InsertedId));
        }
    }
}