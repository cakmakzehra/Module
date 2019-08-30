using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class UnitTypeController : Controller
    {
        // GET: UnitType
        public ActionResult UnitTypes()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<UnitType> UnitType = db.UnitTypes.ToList();
                return View(UnitType);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        [HttpPost]
        public ActionResult UnitTypes(UnitType model)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                UnitType UnitType = db.UnitTypes.Where(x => x.UnitTypeID == model.UnitTypeID).FirstOrDefault();
                UnitType.Explanation = model.Explanation;
                UnitType.Name = model.Name;
                db.SaveChanges();

                return RedirectToAction("UnitTypes", "UnitType");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        public ActionResult EditRecord(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                UnitType UnitType = db.UnitTypes.Where(x => x.UnitTypeID == id).FirstOrDefault();
                return PartialView("UnitTypePartialView", UnitType);
            }
            else
            { return RedirectToAction("Login", "Employee"); }

        }

        public ActionResult CreateNew(string name, string explanation)
        {
            DatabaseContext db = new DatabaseContext();
            UnitType UnitType = new UnitType();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();
            if (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(explanation) == false)
            {
                if (employee != null)
                {
                    UnitType.Explanation = explanation;
                    UnitType.CreateDate = DateTime.Now;
                    UnitType.CreateEmployee = employee;
                    UnitType.Status = "Active";
                    UnitType.Name = name;
                    db.UnitTypes.Add(UnitType);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Employee");
                }
            }
            return RedirectToAction("UnitTypes", "UnitType");
        }

        public ActionResult Delete(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                UnitType UnitType = db.UnitTypes.Where(x => x.UnitTypeID == id).FirstOrDefault();
                UnitType.Status = "Passive";
                db.SaveChanges();
                return RedirectToAction("UnitTypes", "UnitType");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
    }
}