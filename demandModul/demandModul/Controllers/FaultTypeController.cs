using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class FaultTypeController : Controller
    {
        // GET: FaultType
        public ActionResult FaultTypes()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<FaultType> FaultType = db.FaultTypes.ToList();
                return View(FaultType);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        public ActionResult CreateNew(string name, string explanation,string releated)
        {
            DatabaseContext db = new DatabaseContext();
            FaultType FaultType = new FaultType();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();
            if (employee != null)
            {
                FaultType.Explanation = explanation;
                FaultType.CreateDate = DateTime.Now;
                FaultType.CreateEmployee = employee;
                FaultType.RelatedDepartment = releated;
                FaultType.Status = "Active";
                FaultType.Name = name;
                db.FaultTypes.Add(FaultType);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
            return RedirectToAction("FaultTypes", "FaultType");
        }

        [HttpPost]
        public ActionResult FaultTypes(FaultType model)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                FaultType FaultType = db.FaultTypes.Where(x => x.FaultTypeID == model.FaultTypeID).FirstOrDefault();
                FaultType.Explanation = model.Explanation;
                FaultType.RelatedDepartment = model.RelatedDepartment;
                FaultType.Name = model.Name;
                db.SaveChanges();
                return RedirectToAction("FaultTypes", "FaultType");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        public ActionResult EditRecord(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                FaultType FaultType = db.FaultTypes.Where(x => x.FaultTypeID == id).FirstOrDefault();
                return PartialView("FaultTypePartialView", FaultType);
            }
            else
            { return RedirectToAction("Login", "Employee"); }

        }

        public ActionResult Delete(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                FaultType FaultType = db.FaultTypes.Where(x => x.FaultTypeID == id).FirstOrDefault();
                FaultType.Status = "Passive";
                db.SaveChanges();
                return RedirectToAction("FaultTypes", "FaultType");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        

    }
}