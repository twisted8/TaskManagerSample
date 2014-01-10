using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManager.Helpers;
using TaskManager.Infrastructure;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
       
        /// <summary>
        /// Load the status, categories and profile drop downs
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            EntitiesDb db = new EntitiesDb();

            
            UserInfoHelper ui = new UserInfoHelper();
            Tasks tasks = new Tasks();
            tasks.Priorities = db.ref_Priorities;
            tasks.Status = db.ref_Status;
            tasks.Categories = db.Categories;
            tasks.Profiles = db.UserProfileMs;
            return View(tasks);
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
