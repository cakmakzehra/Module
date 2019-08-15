using demandModul.Models;
using demandModul.Models.Database;
using demandModul.ViewModels.IndexViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                IndexViewModel model = new IndexViewModel {
                    Demand = db.Demands.ToList(),
                    Suggestion = db.Suggestions.ToList(),
                    Fault = db.Faults.ToList(),
                };
                return View(model);
            }
            else { return RedirectToAction("Login", "Employee"); }
        }

        

    }
}