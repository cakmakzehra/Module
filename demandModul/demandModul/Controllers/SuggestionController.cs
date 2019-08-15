using demandModul.Models;
using demandModul.Models.Database;
using demandModul.ViewModels.SuggestionViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demandModul.Controllers
{
    public class SuggestionController : Controller
    {
        // GET: Suggestion
        public ActionResult Suggestions()
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                List<Suggestion> Suggestion = db.Suggestions.ToList();
                return View(Suggestion);
            }
            else
            { return RedirectToAction("Login", "Employee"); }
        }
    

        public ActionResult Detail(int? SuggestionID)
        {
            if (Session["EmployeeID"] != null)
            {
                if (SuggestionID != null)
                {
                    DatabaseContext db = new DatabaseContext();
                    SuggestionLike model = new SuggestionLike();
                    model.Like = db.Likes.Where(x => x.SuggestionID == SuggestionID).ToList();
                    model.Suggestion = db.Suggestions.Where(x => x.SuggestionID == SuggestionID).FirstOrDefault();
                    List<SuggestionType> list = db.SuggestionTypes.ToList();
                    ViewBag.SuggestionTypeList = new SelectList(list, "SuggestionTypeID", "Name");
                    return View(model);
                }
                else { return RedirectToAction("Suggestions", "Suggestion"); }
            }
            else { return RedirectToAction("Login", "Employee"); }
        }

        [HttpPost]
        public ActionResult Detail(int? SuggestionID, Suggestion model, string Approve, string save,string delete,string Denied)
        {
            DatabaseContext db = new DatabaseContext();
            Suggestion Suggestion = db.Suggestions.Where(x => x.SuggestionID == SuggestionID).FirstOrDefault();
            Employee employee = db.Employees.Where(x => x.EmployeeID == Convert.ToInt32(Session["EmployeeID"])).FirstOrDefault();
            if (employee != null && Suggestion != null)
            {
                string controlClicked = string.Empty;
                if (!string.IsNullOrEmpty(save))
                {
                    Suggestion.Explanation = model.Explanation;
                    Suggestion.Name = model.Name;
                    Suggestion.lastUpdateDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Suggestion", new { Suggestion.SuggestionID });
                }
                if (!string.IsNullOrEmpty(delete))
                {
                    Suggestion.ApprovedStatus = "Passive";
                    Suggestion.lastUpdateDate = DateTime.Now;
                    db.SaveChanges();
                    return RedirectToAction("Suggestions", "Suggestion");
                }
                if (!string.IsNullOrEmpty(Denied))
                {
                    Suggestion.ApprovedStatus = "Denied";
                    Suggestion.ApprovedDate = DateTime.Now;
                    Suggestion.AppEmployee = employee.NameSurname;
                    Suggestion.DeniedNote = model.DeniedNoteNote;
                    db.SaveChanges();
                    return RedirectToAction("Suggestions", "Suggestion");
                }
                if (!string.IsNullOrEmpty(Approve))
                {
                    SuggestionType SuggestionType = db.SuggestionTypes.Where(x => x.SuggestionTypeID == model.SuggestionTypeID).FirstOrDefault();
                    Suggestion.SuggestionType = SuggestionType;
                    Suggestion.ApprovedStatus = "Approved";
                    Suggestion.ApprovedDate = DateTime.Now;
                    Suggestion.AppEmployee = employee.NameSurname;
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Suggestion", new { Suggestion.SuggestionID });
                }
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
            return View();
        }

        public PartialViewResult LoadData(int id, string data = "")
        {
            DatabaseContext db = new DatabaseContext();
            Comment comment = new Comment();
            Employee employee = db.Employees.Where(x => x.EmployeeID == Convert.ToInt32(Session["EmployeeID"])).FirstOrDefault();
            Suggestion Suggestion = db.Suggestions.Where(x => x.SuggestionID == id).FirstOrDefault();
            if (string.IsNullOrEmpty(data) == false)
            {
                if (employee != null)
                {
                    comment.CommentText = data;
                    comment.CreateTime = DateTime.Now;
                    comment.Employee = employee;
                    comment.Suggestion = Suggestion;
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("", "Please Login.");
                }
            }
            List<Comment> Comment = db.Comments.Where(x => x.Suggestion.SuggestionID == id).ToList();
            return PartialView("_CommentPartialPage", Comment);
        }

        public ActionResult CreateNew(string name,string explanation)
        {
            DatabaseContext db = new DatabaseContext();
            Suggestion suggestion = new Suggestion();
            Employee employee = db.Employees.Where(x => x.EmployeeID == Convert.ToInt32(Session["EmployeeID"])).FirstOrDefault();
            if (string.IsNullOrEmpty(name) == false && string.IsNullOrEmpty(explanation) == false)
            {
                if (employee != null)
                {
                    suggestion.Name = name;
                    suggestion.ApprovedStatus = "New Suggestion";
                    suggestion.Explanation = explanation;
                    suggestion.CreateDate = DateTime.Now;
                    suggestion.lastUpdateDate = DateTime.Now;
                    suggestion.Employee = employee;
                    db.Suggestions.Add(suggestion);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Login", "Employee");
                }
            }
            return RedirectToAction("Suggestions", "Suggestion");
        }

        public ActionResult Likes(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                Employee employee = db.Employees.Where(x => x.EmployeeID == Convert.ToInt32(Session["EmployeeID"])).FirstOrDefault();
                Suggestion Suggestion = db.Suggestions.Where(x => x.SuggestionID == id).FirstOrDefault();
                Like like = db.Likes.FirstOrDefault(x => x.SuggestionID == id && x.EmployeeID == Convert.ToInt32(Session["EmployeeID"]));
                if (like != null)
                {
                    return RedirectToAction("Detail", "Suggestion", new { Suggestion.SuggestionID });
                }
                else {
                    like = new Like();
                    Suggestion.LikesCount++;
                    like.SuggestionID = Suggestion.SuggestionID;
                    like.EmployeeID = employee.EmployeeID;
                    like.Employee = employee;
                    like.LikedDate = DateTime.Now;
                    like.Liked = true;
                    db.Likes.Add(like);
                    db.SaveChanges();
                    return RedirectToAction("Detail", "Suggestion", new { Suggestion.SuggestionID });
                }
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
        }

        public ActionResult Unlikes(int? id)
        {
            if (Session["EmployeeID"] != null)
            {
                DatabaseContext db = new DatabaseContext();
                Like like = db.Likes.FirstOrDefault(x => x.SuggestionID == id && x.EmployeeID == Convert.ToInt32(Session["EmployeeID"]));
                Suggestion Suggestion = db.Suggestions.Where(x => x.SuggestionID == id).FirstOrDefault();
                Suggestion.LikesCount--;
                db.Likes.Remove(like);
                db.SaveChanges();
                return RedirectToAction("Detail", "Suggestion", new { SuggestionID = id });
            }
            else
            {
                return RedirectToAction("Login", "Employee");
            }
        }
    }
}