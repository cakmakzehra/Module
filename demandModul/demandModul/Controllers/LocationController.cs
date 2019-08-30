using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class LocationController : Controller
    {
        // GET: Location
        public ActionResult Locations()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<Location> Location = db.Locations.ToList();
                return View(Location);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        [HttpPost]
        public ActionResult Locations(Location model)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                Location Location = db.Locations.Where(x => x.LocationID == model.LocationID).FirstOrDefault();
                Location.Addresss = model.Addresss;
                Location.Name = model.Name;
                db.SaveChanges();

                return RedirectToAction("Locations", "Location");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        public ActionResult EditRecord(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                Location Location = db.Locations.Where(x => x.LocationID == id).FirstOrDefault();
                return PartialView("LocationPartialView", Location);
            }
            else
            { return RedirectToAction("Login", "Employee"); }

        }
        public ActionResult CreateNew(string name, string address)
        {
            DatabaseContext db = new DatabaseContext();
            Location Location = new Location();
            int Eid = Convert.ToInt32(Session["EmployeeID"]);
            Employee employee = db.Employees.Where(x => x.EmployeeID == Eid).FirstOrDefault();
            if (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(address) == false)
            {
                if (employee != null)
                {
                    Location.Addresss = address;
                    Location.CreateDate = DateTime.Now;
                    Location.CreateEmployee = employee;
                    Location.Status = "Active";
                    Location.Name = name;
                    db.Locations.Add(Location);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Employee");
                }
            }
            return RedirectToAction("Locations", "Location");
        }

        

        public ActionResult Delete(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                Location Location = db.Locations.Where(x => x.LocationID == id).FirstOrDefault();
                Location.Status = "Passive";
                db.SaveChanges();
                return RedirectToAction("Locations", "Location");
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
    }
}