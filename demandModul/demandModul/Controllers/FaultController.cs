using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class FaultController : Controller
    {
        // GET: Fault
        public ActionResult Faults()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<Fault> Fault = db.Faults.ToList();
                List<FaultType> FaultTypeList = db.FaultTypes.ToList();
                ViewBag.FaultTypeList = new SelectList(FaultTypeList, "FaultTypeID", "Name");
                List<Location> LocationList = db.Locations.ToList();
                ViewBag.LocationList = new SelectList(LocationList, "LocationID", "Name");
                return View(Fault);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        
        public ActionResult Detail(int? FaultID)
        {
            if (Session["EmployeeID"] != null)
            {
                Fault Fault = null;
                if (FaultID != null)
                {
                    DatabaseContext db = new DatabaseContext();
                    Fault = db.Faults.Where(x => x.FaultID == FaultID).FirstOrDefault();
                    List<FaultType> list = db.FaultTypes.ToList();
                    ViewBag.FaultTypeList = new SelectList(list, "FaultTypeID", "Name");
                    List<Location> Locationlist = db.Locations.ToList();
                    ViewBag.LocationList = new SelectList(Locationlist, "LocationID", "Name");
                }
                return View(Fault);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        [HttpPost]
        public ActionResult Detail(int? FaultID,string IamWorking,string Passive,string Denied,string DifferentCompanyWorking,string SuccessfullyCompleted,string SituationCouldnotBeResolved,Fault model,string edit)
        {
            DatabaseContext db = new DatabaseContext();
            Fault Fault = db.Faults.Where(x => x.FaultID == FaultID).FirstOrDefault();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();

            if ((Fault != null) && (employee != null))
            {
                string controlClicked = string.Empty;
                if (!string.IsNullOrEmpty(Passive))
                {
                    Fault.FaultStatus = "Passive";
                    db.SaveChanges();
                    return RedirectToAction("Faults", "Fault");
                }
                if (!string.IsNullOrEmpty(edit))
                {
                    FaultType FaultType = db.FaultTypes.Where(x => x.FaultTypeID == model.FaultTypeID).FirstOrDefault();
                    Location Location = db.Locations.Where(x => x.LocationID == model.LocationID).FirstOrDefault();
                    Fault.FaultType = FaultType;
                    Fault.Explanation = model.Explanation;
                    Fault.Location = Location;
                    Fault.IntendedMaintenanceTime = model.IntendedMaintenanceTime;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Fault", new { Fault.FaultID });
                }
                if (!string.IsNullOrEmpty(Denied))
                {
                    controlClicked = "Manager is denied.";
                    Fault.AppliedMaintenanceDate = DateTime.Now;
                    Fault.MaintenanceExplanation = model.MaintenanceExplanation;
                    Fault.MaintenanceEmployee = employee.NameSurname;
                    Fault.FaultStatus = "Denied";
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Fault", new { Fault.FaultID });
                }
                if (!string.IsNullOrEmpty(IamWorking))
                {
                    Fault.FaultStatus = "I am working";
                    Fault.MaintenanceExplanation = model.MaintenanceExplanation;
                    Fault.MaintenanceEmployee = employee.NameSurname;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Fault", new { Fault.FaultID });
                }
                if (!string.IsNullOrEmpty(DifferentCompanyWorking))
                {
                    Fault.FaultStatus = "Different company working";
                    Fault.MaintenanceExplanation = model.MaintenanceExplanation+" working in this fault.";
                    Fault.MaintenanceEmployee = employee.NameSurname;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Fault", new { Fault.FaultID });
                }
                if (!string.IsNullOrEmpty(SuccessfullyCompleted))
                {
                    Fault.FaultStatus = "Successfully completed";
                    Fault.MaintenanceEmployee = employee.NameSurname;
                    Fault.MaintenanceExplanation = model.MaintenanceExplanation;
                    Fault.AppliedMaintenanceDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Fault", new { Fault.FaultID });
                }
                if (!string.IsNullOrEmpty(SituationCouldnotBeResolved))
                {
                    Fault.FaultStatus = "Situation could not be resolved";
                    Fault.MaintenanceEmployee = employee.NameSurname;
                    Fault.AppliedMaintenanceDate = DateTime.Now;
                    Fault.MaintenanceExplanation = model.MaintenanceExplanation;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Fault", new { Fault.FaultID });
                }
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
            return View();
        }
        public ActionResult CreateNew(int? location, int? faultType, string IntendedMaintenanceTime, string explanation)
        {
            DatabaseContext db = new DatabaseContext();
            Fault fault = new Fault();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();
            Location Location = db.Locations.Where(x => x.LocationID == location).FirstOrDefault();
            FaultType Faulttype = db.FaultTypes.Where(x => x.FaultTypeID == faultType).FirstOrDefault();

            if (employee != null)
            {
                fault.Location = Location;
                fault.Explanation = explanation;
                fault.FaultType = Faulttype;
                fault.IntendedMaintenanceTime = IntendedMaintenanceTime;
                fault.CreateDate = DateTime.Now;
                fault.Employee = employee;
                fault.FaultStatus = "New Fault";
                fault.RelatedDepartment = fault.FaultType.RelatedDepartment;

                db.Faults.Add(fault);
                db.SaveChanges();
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
            return RedirectToAction("Faults", "Fault");
        }

    }
}