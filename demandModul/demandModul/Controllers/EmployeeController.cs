using demandModul.Models;
using demandModul.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee employee)
        {
            DatabaseContext db = new DatabaseContext();
            var person = db.Employees.Where(x => x.MailAddress == employee.MailAddress && x.Password == employee.Password).FirstOrDefault();
            if (person != null)
            {
                Session["EmployeeID"] = person.EmployeeID.ToString();
                Session["Name"] = person.NameSurname.ToString();
                Session["Department"] = person.Department.ToString();
                Session["Title"] = person.Title.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Email or Password");
            }
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            DatabaseContext db = new DatabaseContext();
            if (employee.Title == null)
            {
                employee.Title = "Worker";
            }
            db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Login", "Employee");
        }

        public ActionResult MyProfile()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                return View(db.Employees.Where(x => x.EmployeeID == Convert.ToInt32(Session["EmployeeID"])).FirstOrDefault());
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login","Employee");
        }

        public ActionResult List()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
            List<Employee> Employee = db.Employees.ToList();
            return View(Employee);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
    }
}