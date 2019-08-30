using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class DemandController : Controller
    {
        // GET: Demand
        public ActionResult Demands()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<Demand> Demand = db.Demands.ToList();
                List<UnitType> UnitTypeList = db.UnitTypes.ToList();
                ViewBag.UnitTypeList = new SelectList(UnitTypeList, "UnitTypeID", "Name");
                List<Location> LocationList = db.Locations.ToList();
                ViewBag.LocationList = new SelectList(LocationList, "LocationID", "Name");
                return View(Demand);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
        
        public ActionResult Detail(int? DemandID)
        {
            if (Session["EmployeeID"] != null)
            {
                Demand demand = null;

                if (DemandID != null)
                {
                    DatabaseContext db = new DatabaseContext();
                    demand = db.Demands.Where(x => x.DemandID == DemandID).FirstOrDefault();
                    List<Location> LocationList = db.Locations.ToList();
                    ViewBag.LocationList = new SelectList(LocationList, "LocationID", "Name");
                    List<UnitType> UnitTypeList = db.UnitTypes.ToList();
                    ViewBag.UnitTypeList = new SelectList(UnitTypeList, "UnitTypeID", "Name");
                }
                return View(demand);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        [HttpPost]
        public ActionResult Detail(int? DemandID, string Approve, string Passive, string Denied,string edit,Demand model)
        {
            DatabaseContext db = new DatabaseContext();
            Demand Demand = db.Demands.Where(x => x.DemandID == DemandID).FirstOrDefault();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();

            if (employee != null && Demand != null)
            {
                string controlClicked = string.Empty;
                if (!string.IsNullOrEmpty(Passive))
                {
                    Demand.ApprovedStatus = "Passive";
                    db.SaveChanges();
                    return RedirectToAction("Demands", "Demand");
                }
                if (!string.IsNullOrEmpty(edit))
                {
                    Location Location = db.Locations.Where(x => x.LocationID == model.LocationID).FirstOrDefault();
                    UnitType UnitType = db.UnitTypes.Where(x => x.UnitTypeID == model.UnitTypeID).FirstOrDefault();
                    Demand.UnitType = UnitType;
                    Demand.Location = Location;
                    Demand.Explanation = model.Explanation;
                    Demand.InventoryName = model.InventoryName;
                    Demand.Quantity = model.Quantity;
                    Demand.Urgency = model.Urgency;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Demand", new { Demand.DemandID });
                }
                if (!string.IsNullOrEmpty(Denied))
                {
                    controlClicked = "Manager is denied.";
                    Demand.AppNote = model.AppNote;
                    Demand.AppDate = DateTime.Now;
                    Demand.ApproverEmployee = employee.NameSurname;
                    Demand.ApprovedStatus = "Denied";
                    db.SaveChanges();
                    return RedirectToAction("Demands", "Demand");
                }
                if (!string.IsNullOrEmpty(Approve))
                {
                    controlClicked = "Manager is approved.";
                    Demand.AppNote = model.AppNote;
                    Demand.ApprovedStatus = "Approved";
                    Demand.AppDate = DateTime.Now;
                    Demand.ApproverEmployee = employee.NameSurname;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Demand", new { Demand.DemandID });
                }
                else { ModelState.AddModelError("", "Something went wrong."); }
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
            return View();
        }


        public ActionResult CreateNew(int? unitType, int piece, string urgency, int? address, string name, string explanation)
        {
            DatabaseContext db = new DatabaseContext();
            Demand demand = new Demand();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();
            Location location = db.Locations.Where(x => x.LocationID == address).FirstOrDefault();
            UnitType unittype = db.UnitTypes.Where(x => x.UnitTypeID == unitType).FirstOrDefault();
            if (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(explanation) == false)
            {
                if (employee != null)
                {
                    demand.InventoryName = name;
                    demand.Explanation = explanation;
                    demand.Quantity = piece;
                    demand.Urgency = urgency;
                    demand.ApprovedStatus = "New Demand";
                    demand.UnitType = unittype;
                    demand.Location = location;
                    demand.CreateDate = DateTime.Now;
                    demand.Employee = employee;
                    db.Demands.Add(demand);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Employee");
                }
            }
            return RedirectToAction("Demands", "Demand");
        }

        
    }
}