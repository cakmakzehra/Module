using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class SuggestionTypeController : Controller
    {
        // GET: SuggestionType
        public ActionResult SuggestionTypes()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<SuggestionType> SuggestionType = db.SuggestionTypes.ToList();
                return View(SuggestionType);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
        [HttpPost]
        public ActionResult SuggestionTypes(SuggestionType model)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                SuggestionType SuggestionType = db.SuggestionTypes.Where(x => x.SuggestionTypeID == model.SuggestionTypeID).FirstOrDefault();
                SuggestionType.Explanation = model.Explanation;
                SuggestionType.Name = model.Name;
                db.SaveChanges();

                return RedirectToAction("SuggestionTypes", "SuggestionType");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        public ActionResult EditRecord(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                SuggestionType SuggestionType = db.SuggestionTypes.Where(x => x.SuggestionTypeID == id).FirstOrDefault();
                return PartialView("SuggestionTypePartialView", SuggestionType);
            }
            else
            { return RedirectToAction("Login", "Employee"); }

        }

        public ActionResult CreateNew(string name, string explanation)
        {
            DatabaseContext db = new DatabaseContext();
            SuggestionType SuggestionType = new SuggestionType();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();
            if (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(explanation) == false)
            {
                if (employee != null)
                {
                    SuggestionType.Explanation = explanation;
                    SuggestionType.CreateDate = DateTime.Now;
                    SuggestionType.CreateEmployee = employee;
                    SuggestionType.Status = "Active";
                    SuggestionType.Name = name;
                    db.SuggestionTypes.Add(SuggestionType);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Employee");
                }
            }
            return RedirectToAction("SuggestionTypes", "SuggestionType");
        }

        public ActionResult Delete(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                SuggestionType SuggestionType = db.SuggestionTypes.Where(x => x.SuggestionTypeID == id).FirstOrDefault();
                SuggestionType.Status = "Passive";
                db.SaveChanges();
                return RedirectToAction("SuggestionTypes", "SuggestionType");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
    }
}